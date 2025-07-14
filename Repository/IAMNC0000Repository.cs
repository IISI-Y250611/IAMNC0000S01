using Dapper;
using IAMNC0000S03.Business.Interfaces;
using IAMNC0000S03.Domain.Entity;
using NLog;
using System.Diagnostics;
using static IAMNC0000S03.Domain.Entity.UserEntity;

namespace IAMNC0000S03.Repository
{
    public class IAMNC0000Repository : IIAMNC0000Repository
    {
        private readonly IOracleDbContext _oracleContext;
        protected static readonly Logger _nLogger = LogManager.GetCurrentClassLogger();
        private readonly ActivitySource OracleDBActivitySource = new ActivitySource("OrcaleDbService");

        public IAMNC0000Repository(IOracleDbContext oracleContext)
        {
            _oracleContext = oracleContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="systemCode"></param>
        /// <param name="branchCode"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IEnumerable<RoleEntity>> GetRoleList(string userId, string systemCode, string? branchCode)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-GetRoleList");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    var sql = @"
                         SELECT distinct a.ROLE_ID  
                           FROM IAMNO_REL_USER_ROLE a
                              , IAMNO_ROLE b	
                          WHERE a.BRANCH_CODE = DECODE(:branchCode ,'',a.BRANCH_CODE,:branchCode)
                            AND a.USER_ID = :userId 
                            AND a.SYSTEM_CODE  = :systemCode 
                            AND a.SYSTEM_CODE  = b.SYSTEM_CODE
                            AND a.SYSTEM_CODE != 'IAM' AND b.SYSTEM_CODE != 'IAM' ";

                    return await connection.QueryAsync<RoleEntity>(sql, new { branchCode, systemCode, userId });
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB GetRoleList 讀取失敗");
                throw;
            }

        }

        public async Task<IEnumerable<OrgEntity>> GetOrganizationList(string? branchCode)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-GetOrganizationList");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    var sql = @"
                         SELECT DISTINCT
                                BRANCH_CODE
                              , a.OU_ID
                              , a.OU_NAME
                           FROM IAMNO_UNIT a	
                          WHERE a.BRANCH_CODE = DECODE(:branchCode ,'', a.BRANCH_CODE, :branchCode)      
                       ORDER BY BRANCH_CODE
                              , a.OU_ID ";

                    return await connection.QueryAsync<OrgEntity>(sql, new { branchCode });
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB GetOrganizationList 讀取失敗");
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchCode"></param>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IEnumerable<UserEntity>> GetUserBaseInfo(string? branchCode = null, string? userId = null, string? orgId = null)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-GetUserBaseInfo");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    var sql = @"
                        SELECT a.BRANCH_CODE
                             , a.USER_ID
                             , a.USER_NAME
                             , a.OU_ID
                             , DECODE(b.OU_ID, 'Y', '離職', b.OU_NAME) AS OU_NAME
                             , a.USER_EMAIL
                          FROM IAMNO_USER a
                     LEFT JOIN IAMNO_UNIT b
                            ON a.OU_ID = b.OU_ID
                         WHERE (:userId IS NULL OR a.USER_ID = :userId)
                           AND (:branchCode IS NULL OR a.BRANCH_CODE = :branchCode)
                           AND (:orgId IS NULL OR b.OU_ID = :orgId)";

                    return await connection.QueryAsync<UserEntity>(sql, new { userId, branchCode, orgId });
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB GetUserBaseInfo 讀取失敗");
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IEnumerable<SupportBranchEnity>> GetSuppourtCode(string? orgId, string? branchCode)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-GetSuppourtCode");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    var sql = @"
                         SELECT a.USER_ID
                              , a.SUP_BRANCH_CODE
                           FROM IAMNO_SUPPORT_USER a
                      LEFT JOIN IAMNO_USER b
                             ON a.USER_ID = b.USER_ID
                          WHERE (b.OU_ID = :orgId or :orgId is null)
                            AND SUP_BRANCH_CODE != NVL(:branchCode, -1)";

                    return await connection.QueryAsync<SupportBranchEnity>(sql, new { orgId, branchCode });
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB GetSuppourtCode 讀取失敗");
                throw;
            }
        }

        /// <summary>
        /// 讀取使用者資料
        /// </summary>
        /// <param name="branchCode">業務組別代碼</param>
        /// <param name="userId">使用者代號</param>
        /// <returns></returns>
        public async Task<UserEntity> GetDeatilUserInfo(string branchCode, string userId)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-GetUserInfo");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    var sql = @"
                       SELECT a.USER_ID
                            , a.USER_NAME
                            , a.BRANCH_CODE
                            , a.PASSWORD
                            , a.USER_TYPE
                            , a.USER_DESC
                            , a.ID_ENABLE
                            , a.ID_LOCK_STATE
                            , a.NEED_CHANPSWD
                            , a.VALID_S_DATE
                            , a.VALID_E_DATE
                            , a.LAST_CHANPSWD
                            , a.LAST_SUCCLOGIN
                            , a.LAST_FAILLOGIN
                            , a.LAST_LOCK_DATE
                            , a.CNT_FAILLOGIN
                            , a.CON_FAULT
                            , a.NONCON_FAULT
                            , a.COUNTER
                            , a.ID_AES 
                            , a.PSWD_ALLRIGHT
                            , a.EMP_ID
                            , a.USER_NICKNAME
                            , a.USER_EMAIL
                            , a.MGR_USER_ID
                            , a.USER_KIND
                            , a.WLIST_MARK
                            , a.WLIST_S_DATE
                            , a.WLIST_E_DATE
                            , a.AUTH_EMAIL_TIMES
                            , a.OU_ID
                            , DECODE(a.OU_ID, 'Y', '離職', b.OU_NAME) AS OU_NAME 
                         FROM IAMNO_USER a
                            , IAMNO_UNIT b  
                        WHERE a.BRANCH_CODE = :branchCode   
                          AND a.USER_ID = :userId 
                          AND a.OU_ID = b.OU_ID(+) ";

                    return await connection.QueryFirstOrDefaultAsync<UserEntity>(sql, new { branchCode, userId });
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB GetUserInfo 讀取失敗");
                throw;
            }
        }

        /// <summary>
        /// 查詢使用者已授予的角色
        /// </summary>
        /// <param name="branchCode">業務組別代碼</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RoleEntity>> GetUserAssignedRoles(string systemCode, string branchCode, string userId)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-GetUserAssignedRoles");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    var sql = @"
                         SELECT b.ROLE_ID
                              , b.ROLE_NAME
                              , b.ROLE_TYPE
                              , b.ROLE_KIND
                              , b.ROLE_DESC
                              , b.SORT_ORDER
                              , b.MGR_BRANCH_CODE
                              , b.MGR_OU_ID
                              , ( SELECT CASE WHEN count(*) > 0 THEN 'Y' ELSE 'N' END AS Flag
                                    FROM IAMNO_REL_ROLE_FUN x
                                       , IAMNO_FUNCTION y
                                   WHERE x.SYSTEM_CODE = a.SYSTEM_CODE
                                     AND x.ROLE_ID     = a.ROLE_ID
                                     AND x.SYSTEM_CODE = y.SYSTEM_CODE
                                     AND x.FUN_ID      = y.FUN_ID
                                     AND y.SENSITIVE_FUN_FLAG = 'Y'
                                 ) AS HAS_SENSITIVE_FUN_FLAG   
                           FROM IAMNO_REL_USER_ROLE a
                              , IAMNO_ROLE b
                          WHERE a.SYSTEM_CODE = :systemCode
                            AND a.BRANCH_CODE = :branchCode
                            AND a.USER_ID = :userId
                            AND a.SYSTEM_CODE = b.SYSTEM_CODE 
                            AND a.ROLE_ID = b.ROLE_ID
                       ORDER BY a.ROLE_ID ";

                    var result = await connection.QueryAsync<RoleEntity>(sql, new { systemCode, branchCode, userId });
                    return result ?? Enumerable.Empty<RoleEntity>();
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB GetUserAssignedRoles 讀取失敗");
                throw;
            }
        }

        /// <summary>
        /// 使用者已授予功能權利查詢
        /// </summary>
        /// <param name="systemCode">系統代碼</param>
        /// <param name="branchCode">業務組別代碼</param>
        /// <param name="userId">使用者代碼</param>
        /// <returns></returns>
        public async Task<IEnumerable<FunctionEntity>> GetUserAssignedFunctions(string systemCode, string branchCode, string userId)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-GetUserAssignedFunctions");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    var sql = @"
                         SELECT y.*
                           FROM ( 
                                SELECT a.SYSTEM_CODE
                                     , a.FUN_ID 
                                  FROM IAMNO_REL_USER_FUN a
                                 WHERE a.SYSTEM_CODE = :systemCode
                                   AND a.BRANCH_CODE = :branchCode
                                   AND a.USER_ID     = :userId
                                 UNION
                                SELECT a.SYSTEM_CODE
                                     , b.FUN_ID
                                  FROM IAMNO_REL_USER_ROLE a
                                     , IAMNO_REL_ROLE_FUN b
                                 WHERE a.SYSTEM_CODE   = :systemCode
                                   AND a.BRANCH_CODE   = :branchCode
                                   AND a.USER_ID       = :userId
                                   AND a.SYSTEM_CODE   = b.SYSTEM_CODE
                                   AND a.ROLE_ID       = b.ROLE_ID
                                ) x
                                , IAMNO_FUNCTION y	  
                          WHERE x.SYSTEM_CODE = y.SYSTEM_CODE(+)
                            AND x.FUN_ID      = y.FUN_ID(+)
                       ORDER BY x.SYSTEM_CODE
                              , x.FUN_ID ";

                    var result = await connection.QueryAsync<FunctionEntity>(sql, new { systemCode, branchCode, userId });
                    return result ?? Enumerable.Empty<FunctionEntity>();
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB GetUserAssignedFunctions 讀取失敗");
                throw;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchCode"></param>
        /// <param name="systemCode"></param>
        /// <param name="userId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<bool> CheckFunction(string? branchCode, string systemCode, string userId, string functionId)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-CheckFunction");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    var param = new
                    {
                        systemCode,
                        branchCode,
                        userId,
                        functionId
                    };

                    var sql = @"
                       SELECT 1
                         FROM (
                              SELECT b.FUN_ID
                                FROM IAMNO_REL_USER_ROLE a
                                   , IAMNO_REL_ROLE_FUN b 
                               WHERE a.SYSTEM_CODE = :systemCode 
                                 AND a.BRANCH_CODE = DECODE(:branchCode, '', a.BRANCH_CODE, :branchCode)
                                 AND a.USER_ID     = :userId 
                                 AND a.ROLE_ID     = b.ROLE_ID 
                                 AND b.FUN_ID      = :functionId     
                               UNION 
                              SELECT a.FUN_ID 
                                FROM IAMNO_REL_USER_FUN a 
                               WHERE a.SYSTEM_CODE = :systemCode  
                                 AND a.BRANCH_CODE =  DECODE(:branchCode, '', a.BRANCH_CODE, :branchCode)
                                 AND a.USER_ID     = :userId  
                                 AND a.FUN_ID      = :functionId 
                                 )
                        WHERE rownum = 1 ";

                    var exists = await connection.ExecuteScalarAsync<int?>(sql, param);
                    return exists.HasValue;
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB CheckFunction 讀取失敗");
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<bool> CheckRole(string? branchCode, string systemCode, string userId, string roleId)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-CheckRole");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    var sql = @"
                         SELECT 1
                           FROM IAMNO_REL_USER_ROLE a
                              , IAMNO_REL_ROLE_FUN b 
                          WHERE a.SYSTEM_CODE = :systemCode 
                            AND (:branchCode IS NULL OR a.BRANCH_CODE = :branchCode)
                            AND a.USER_ID     = :userId 
                            AND a.ROLE_ID     = :roleId   
                            AND rownum = 1 ";


                    var exists = await connection.ExecuteScalarAsync<int?>(sql, new
                    {
                        systemCode,
                        branchCode,
                        userId,
                        roleId
                    });

                    return exists.HasValue;
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB CheckRole 讀取失敗");
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="branchCode"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<bool> CheckUserExists(string userId, string? branchCode)
        {
            using var activity = OracleDBActivitySource.StartActivity("OracleDB-CheckUserExists");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    var sql = @"
                         SELECT 1
                           FROM IAMNO_USER 
                          WHERE BRANCH_CODE = DECODE(:branchCode ,'', BRANCH_CODE, :branchCode)
                            AND USER_ID     = :userId ";

                    var result = await connection.ExecuteScalarAsync<int?>(sql, new { userId = userId, branchCode = branchCode });
                    return result.HasValue;
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB CheckUserExists 讀取失敗");
                throw;
            }
        }

        /// <summary>
        /// 確認組室別代碼是否存在
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<bool> CheckOrgExists(string orgId)
        {

            using var activity = OracleDBActivitySource.StartActivity("OracleDB-CheckOrgExists");
            try
            {
                using (var connection = _oracleContext.CreateConnection())
                {
                    if (connection == null)
                    {
                        throw new NullReferenceException("Database connection is null");
                    }

                    var sql = @"
                         SELECT 1
                           FROM IAMNO_USER a
                              , IAMNO_UNIT b
                          WHERE a.OU_ID = b.OU_ID(+)
                            AND b.OU_ID = :OrgID ";

                    var result = await connection.ExecuteScalarAsync<int?>(sql, new { orgId });
                    return result.HasValue;
                }
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _nLogger.Error(ex, "OracleDB CheckOrgExists 讀取失敗");
                throw;
            }
        }

    }
}