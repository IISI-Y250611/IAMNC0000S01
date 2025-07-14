using Dapper;
using DotNetEnv;
using IAMNC0000S03.Business.Interfaces;
using IAMNC0000S03.Business.Models;
using IAMNC0000S03.Business.Queries;
using IAMNC0000S03.Domain.Entity;
using NLog;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using System.Text;
using static IAMNC0000S03.Domain.Entity.UserEntity;

namespace IAMNC0000S03.Repository
{
    public class IAMNC0000N02Repository : IIAMNC0000N02Repository
    {
        private readonly IOracleDbContext _oracleContext;
        protected static readonly Logger _nLogger = LogManager.GetCurrentClassLogger();
        private readonly ActivitySource OracleDBActivitySource = new ActivitySource("OrcaleDbService");

        public IAMNC0000N02Repository(IOracleDbContext oracleContext)
        {
            _oracleContext = oracleContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BullientDataQueryDto"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IEnumerable<BullientDataDto>> GetBullientData(BullientDataQueryDto query)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-GetBullientData");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    StringBuilder sql = new();
                    var parameters = new DynamicParameters();

                    sql.Append(@"
                         SELECT a.MSG_SEQ	as	MsgSeq,
                                a.SYSTEM_CODE	as	SystemCode,
                                a.SUBJECT	as	Subject,
                                a.CONTENT	as	EncodeContent,
                                a.VALID_S_DATE	as	ValidStrartDate,
                                a.VALID_E_DATE	as	ValidEndDate,
                                a.DISPLAY_TYPE	as	DisplayType,
                                a.IS_TOP	as	IsTop,
                                a.TOP_ORDER	as	TopOrder,
                                a.TXT_DATE	as	TxtDate,
                                a.TXT_USER_ID	as	TxtUserId,
                                a.PROC_URL	as	ProcessUrl,
                                a.URL_BUTTON_NAME	as	UrlButtonName,
                                a.LIMIT_READ_MARK	as	LimitReadMark,
                                a.CLOSE_COUNTDOWN	as	CloseCountdown,
   
                                  decode(a.system_code,'PUBLIC','通用公告',b.system_name) System_name
                            FROM IAMNO_BULLETIN a,IAMNO_APPLICATION b 
                            WHERE a.system_code = b.system_code 
                             AND  (a.system_code='PUBLIC' OR a.system_code=:SystemCode)     
                             AND  a.display_type=DECODE('1','',a.display_type,'1')     --:displayType呈現方式，1:公告列表（預設）、2:POPUP視窗   ");

                    parameters.Add(":SystemCode", query.SystemCode);

                    if (query.IsCurrentActive)
                    {
                        sql.Append(@"AND a.valid_s_date <= sysdate
                                    AND a.valid_e_date >= TRUNC(sysdate)");
                    }
                    else
                    {
                        sql.Append(@"AND  a.valid_s_date >= TO_DATE(:StartBullientDate, 'yyyy-mm-dd')--:公告有效起日");

                        parameters.Add(":StartBullientDate", query.StartBullientDate);
                    }

                    //排序
                    if(query.OrderType == "1")
                    {
                        sql.Append(@" ORDER BY A.IS_TOP,A.TOP_ORDER ,A.TXT_DATE DESC ");
                    }
                    else
                    {
                        sql.Append(@" ORDER BY A.TXT_DATE  ");
                    }

                    var result = await connection.QueryAsync<BullientDataDto>(sql.ToString(), parameters);


                    return result;
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB GetBullientData 讀取失敗");

            throw;
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="MsgSeq"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AttachmentFile>> GetAttachmentFile(int MsgSeq)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-GetAttachmentFile");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    StringBuilder sql = new();
                    var parameters = new DynamicParameters();

                    sql.Append(@"
                         SELECT b.MSG_SEQ as MsgSeq,
                                b.ATT_SEQ as AttSeq,
                                b.FILE_NAME as FileName,
                                b.DISPLAY_ORDER,  
                                b.service_type  as ServiceType
                            FROM  IAMNO_ATTACHMENT b    
                            WHERE b.service_type='1'
                              AND b.msg_seq = :MsgSeq  ");

                    parameters.Add(":MsgSeq", MsgSeq);

                    var result = await connection.QueryAsync<AttachmentFile>(sql.ToString(), parameters);


                    return result;
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB GetAttachmentFile 讀取失敗");

                throw;
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="MsgSeq"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PopNotificationsDataDto>> GetPopNotificationsData(PopNotificationsQueryDto query)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-GetPopNotificationsData");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    StringBuilder sql = new();
                    var parameters = new DynamicParameters();

                    sql.Append(@"
                         Select * FROM ( 
                             SELECT MSG_SEQ as MsgSeq,
                            system_code as SystemCode, 
                            SUBJECT as Subject,
                            CONTENT as EncodeContent,
                            TOP_ORDER as TopOrder, 
                            LIMIT_READ_MARK as LimitReadMark,
                            PROC_URL as ProcessUrl, 
                            URL_BUTTON_NAME as UrlButtonName, 
                            decode(URL_BUTTON_NAME,NULL,'N','Y') BottonMark 
   FROM IAMNO_BULLETIN 
   WHERE (SYSTEM_CODE='PUBLIC' OR SYSTEM_CODE='MED3')   --:systemCode
   AND DISPLAY_TYPE IN ('2', 'X')
   AND VALID_S_DATE <= sysdate
   AND VALID_E_DATE >= TRUNC(sysdate)
   AND (CASE WHEN LIMIT_READ_MARK IS NULL THEN 1
       WHEN LIMIT_READ_MARK='U' AND
           ( Select count(*) From IAMNO_BULLETIN_USER_CTRL
             WHERE USER_ID = :UserId
		   --AND BRANCH_CODE = :BRANCH_CODE : paras.Add(IACGVarLib.gLoginBranchCode)            
             AND DISPLAY_OFF_MARK = 'N'
             AND MSG_SEQ = IAMNO_BULLETIN.MSG_SEQ
             AND rownum = 1) > 0 THEN 1
       ELSE 0
	   END ) =1   
   ORDER BY VALID_S_DATE DESC 
   )
");

                    parameters.Add(":UserId", query.UserId);

                    var result = await connection.QueryAsync<PopNotificationsDataDto>(sql.ToString(), parameters);


                    return result;
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB GetPopNotificationsData 讀取失敗");

                throw;
            }

        }
    }
}