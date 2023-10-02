using Sample.Common;
using Sample.EmployeeModule.DI;

namespace SampleAPI.DataProvider.Base
{
    public abstract class EmployeeModuleBaseDIDataProvider<T, M> : CommonBaseDataProvider<T, M> where T : class where M : class
    {
        protected new T Service { get; }

        protected EmployeeModuleBaseDIDataProvider(string authHeader) : this(authHeader, true)
        {

        }

        protected EmployeeModuleBaseDIDataProvider(string authHeader, bool processHeader) : base(authHeader, processHeader)
        {
            Service = EmployeeModuleDIResolver.Resolve<T>();
        }
    }
}
