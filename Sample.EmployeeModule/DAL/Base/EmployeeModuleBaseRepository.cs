using Sample.Common.DAL;
using Sample.Common.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.EmployeeModule.DAL.Base
{
    public class EmployeeModuleBaseRepository : DALBaseRepository
    {
        public EmployeeModuleBaseRepository(Type concreteType) : base(concreteType)
        {
        }


        #region Protected Methods

        protected async Task<Response<T>> ExecuteStoredProcSingleAsync<T>(SP name, object request, int? timeout = null)
        {
            return await ExecuteStoredProcSingleAsync<T>(name.ToString(), request, timeout);
        }

        protected async Task<Response<IEnumerable<T>>> ExecuteStoredProcListAsync<T>(SP name, object request, int? timeout = null)
        {
            return await ExecuteStoredProcListAsync<T>(name.ToString(), request, timeout);
        }

        protected async Task<Response<T>> ExecuteStoredProcSingleJsonAsync<T>(SP name, object request, int? timeout = null)
        {
            return await ExecuteStoredProcSingleJsonAsync<T>(name.ToString(), request, timeout);
        }

        protected async Task<Response<IEnumerable<T>>> ExecuteStoredProcJsonListAsync<T>(SP name, object request, int? timeout = null)
        {
            return await ExecuteStoredProcJsonListAsync<T>(name.ToString(), request, timeout);
        }


        protected override string DBConnStr()
        {
            return EmployeeConfig.EmployeeConnStr.Replace("AppPath", string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "DataBase"));
        }

        protected override string DefaultSchema => "dbo";

        #endregion


        #region Enum

        protected enum SP
        {
            GetEmployeesDetails,
            GetEmployeeDetailsById,
            AddOrUpdateEmployee,
            DeleteEmployeeById
        }

        #endregion
    }
}
