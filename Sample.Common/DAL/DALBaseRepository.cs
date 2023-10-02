using Newtonsoft.Json;
using Sample.Common.Dapper;
using Sample.Common.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.DAL
{
    public abstract class DALBaseRepository : BasePGRepository
    {
        #region Protected Members

        protected readonly IDbTransaction _transaction;
        protected virtual string DefaultSchema => "public";

        #endregion


        #region Constructor

        protected DALBaseRepository(Type concreteType) : this(null, concreteType)
        {
        }

        protected DALBaseRepository(IDbTransaction transaction, Type concreteType) : base(concreteType)
        {
            _transaction = transaction;
        }

        #endregion


        #region Protected Methods

        protected async Task<Response<T>> ExecuteStoredProcSingleAsync<T>(string spName, object request, int? timeout = null)
        {
            return await ExecuteStoredProcSingleAsync<T>(DefaultSchema, spName, request, timeout);
        }

        protected async Task<Response<T>> ExecuteStoredProcSingleAsync<T>(string schema, string spName, object request, int? timeout = null)
        {
            var con = GetDbCon();
            try
            {
                var result = await ExecuteStoredProcAsync<T>(con, schema, spName, request, _transaction, timeout);
                return new Response<T>() { Payload = result.FirstOrDefault() };
            }
            catch (Exception ex)
            {
                return new Response<T>() { Success = false, Error = new ErrorItem(ex.Message, ex) };
            }
            finally
            {
                if (_transaction == null)
                    con.Dispose();
            }
        }

        protected async Task<Response<IEnumerable<T>>> ExecuteStoredProcListAsync<T>(string spName, object request, int? timeout = null)
        {
            return await ExecuteStoredProcListAsync<T>(DefaultSchema, spName, request, timeout);

        }

        protected async Task<Response<IEnumerable<T>>> ExecuteStoredProcListAsync<T>(string schema, string spName, object request, int? timeout = null)
        {
            var con = GetDbCon();
            try
            {
                var result = await ExecuteStoredProcAsync<T>(con, schema, spName, request, _transaction, timeout);
                return new Response<IEnumerable<T>>() { Payload = result };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<T>>() { Success = false, Error = new ErrorItem(ex.Message, ex) };
            }
            finally
            {
                if (_transaction == null)
                    con.Dispose();
            }
        }

        protected async Task<Response<T>> ExecuteStoredProcSingleJsonAsync<T>(string spName, object request, int? timeout = null, params JsonConverter[] converters)
        {
            return await ExecuteStoredProcSingleJsonAsync<T>(DefaultSchema, spName, request, timeout, converters);
        }

        protected async Task<Response<T>> ExecuteStoredProcSingleJsonAsync<T>(string schema, string spName, object request, int? timeout = null, params JsonConverter[] converters)
        {
            var result = await ExecuteStoredProcJsonListAsync<T>(schema, spName, request, timeout, converters);

            return new Response<T>()
            {
                Success = result.Success,
                Error = result.Error,
                Payload = (
                    (result.Success && result.Payload != null && result.Payload.Any())
                        ? result.Payload.First() : default(T)),
                ResponseCode = result.ResponseCode
            };
        }

        protected async Task<Response<IEnumerable<T>>> ExecuteStoredProcJsonListAsync<T>(string spName, object request, int? timeout = null, params JsonConverter[] converters)
        {
            return await ExecuteStoredProcJsonListAsync<T>(DefaultSchema, spName, request, timeout, converters);
        }

        protected async Task<Response<IEnumerable<T>>> ExecuteStoredProcJsonListAsync<T>(string schema, string spName, object request, int? timeout = null, params JsonConverter[] converters)
        {
            var result = await ExecuteStoredProcListAsync<string>(schema, spName, request, timeout);

            if (result.Success && result.Payload != null && result.Payload.Any() && result.Payload.First() != null)
            {
                try
                {
                    return new Response<IEnumerable<T>>()
                    {
                        Payload = result.Payload.Select(r => ConvertJsonData<T>(r, converters)).ToList(),
                        Error = null,
                        ResponseCode = result.ResponseCode,
                        Success = true
                    };

                }
                catch (Exception ex)
                {
                    return new Response<IEnumerable<T>>()
                    {
                        Payload = null,
                        Error = new ErrorItem("Fail on conversion", ex),
                        ResponseCode = result.ResponseCode,
                        Success = false
                    };
                }
            }

            return new Response<IEnumerable<T>>()
            {
                Payload = null,
                Error = result.Error,
                ResponseCode = result.ResponseCode,
                Success = result.Success
            };
        }

        protected virtual IDbConnection GetDbCon()
        {
            if (_transaction != null && !string.IsNullOrWhiteSpace(_transaction.Connection.ConnectionString))
                return _transaction.Connection;

            return GetDb(DBConnStr());
        }

        protected abstract string DBConnStr();

        #endregion


        #region Private Methods

        private T ConvertJsonData<T>(string rawData, params JsonConverter[] converters)
        {
            try
            {
                if (converters != null && converters.Any())
                {
                    return JsonConvert.DeserializeObject<T>(rawData, converters);
                }

                return JsonConvert.DeserializeObject<T>(rawData);
            }
            catch (Exception ex)
            {
                GetLogger.Error("Conversion error, data: " + rawData, ex);
                return default(T);
            }
        }

        #endregion
    }
}
