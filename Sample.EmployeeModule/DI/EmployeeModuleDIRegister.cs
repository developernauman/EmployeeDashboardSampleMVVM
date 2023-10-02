using Sample.Common.Ioc;
using System.Collections.Generic;
using System.Reflection;

namespace Sample.EmployeeModule.DI
{
    public class EmployeeModuleDIRegister : BaseResolver
    {
        public override void ConfigureAutoRegister(List<Assembly> assemblies)
        {
            assemblies.Add(typeof(AutoRegister).Assembly);
        }
    }
}
