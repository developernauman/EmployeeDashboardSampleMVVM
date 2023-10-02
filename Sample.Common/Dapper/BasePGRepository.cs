using Dapper;
using Microsoft.Data.SqlClient;
using Sample.Common.Log4Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Dapper
{
    public abstract class BasePGRepository : LoggingService
    {
        #region Constructor

        protected BasePGRepository(Type concreteType) : base(concreteType)
        {
        }

        #endregion


        #region Protected Methods

        protected IDbConnection GetDb(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        protected IEnumerable<T> QueryExecute<T>(IDbConnection con, string sql, object param = null, IDbTransaction transaction = null, int? timeout = null)
        {
            return con.Query<T>(sql, AppendParamOnRequestObject(param), transaction, commandTimeout: timeout);
        }

        protected async Task<IEnumerable<T>> QueryExecuteAsync<T>(IDbConnection con, string sql, object param = null, IDbTransaction transaction = null, int? timeout = null)
        {
            return await con.QueryAsync<T>(sql, AppendParamOnRequestObject(param), transaction, timeout);
        }

        protected IEnumerable<T> ExecuteStoredProc<T>(IDbConnection con, string spName, object param = null, IDbTransaction transaction = null, int? timeout = null)
        {
            return ExecuteStoredProc<T>(con, string.Empty, spName, param, transaction, timeout);
        }

        protected IEnumerable<T> ExecuteStoredProc<T>(IDbConnection con, string schema, string spName, object param = null, IDbTransaction transaction = null, int? timeout = null)
        {
            return con.Query<T>(BuildSpName(schema, spName), AppendParamOnRequestObject(param), transaction, commandType: CommandType.StoredProcedure, commandTimeout: timeout);
        }

        protected async Task<IEnumerable<T>> ExecuteStoredProcAsync<T>(IDbConnection con, string spName, object param = null, IDbTransaction transaction = null, int? timeout = null)
        {
            return await ExecuteStoredProcAsync<T>(con, string.Empty, spName, param, transaction, timeout);
        }

        protected async Task<IEnumerable<T>> ExecuteStoredProcAsync<T>(IDbConnection con, string schema, string spName, object param = null, IDbTransaction transaction = null, int? timeout = null)
        {
            return await con.QueryAsync<T>(BuildSpName(schema, spName), AppendParamOnRequestObject(param), transaction, commandType: CommandType.StoredProcedure, commandTimeout: timeout);
        }

        protected async Task ExecuteStoredProcAsync(IDbConnection con, string spName, object param = null, IDbTransaction transaction = null, int? timeout = null)
        {
            await ExecuteStoredProcAsync(con, string.Empty, spName, param, transaction, timeout);
        }

        protected async Task ExecuteStoredProcAsync(IDbConnection con, string schema, string spName, object param = null, IDbTransaction transaction = null, int? timeout = null)
        {
            await con.ExecuteAsync(BuildSpName(schema, spName), AppendParamOnRequestObject(param), transaction, timeout, CommandType.StoredProcedure);
        }

        #endregion


        #region Private Methods

        private object AppendParamOnRequestObject(object param)
        {
            if (param == null)
                return null;

            dynamic resultRequest = new ExpandoObject();
            var requestDict = resultRequest as IDictionary<string, object>;

            foreach (var propertyInfo in param.GetType().GetProperties())
            {
                requestDict.Add("p_" + propertyInfo.Name.ToLower(), propertyInfo.GetValue(param));
            }

            return resultRequest;
        }

        private string BuildSpName(string schema, string spName)
        {
            if (!string.IsNullOrWhiteSpace(schema))
            {
                return $"{schema}.\"{spName}\"";
            }

            return $"public.\"{spName}\"";
        }

        #endregion
    }
}
