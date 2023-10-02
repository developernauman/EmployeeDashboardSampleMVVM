using Sample.Common.Configuration;

namespace Sample.EmployeeModule
{
    public static class EmployeeConfig
    {
        public static string EmployeeDBName => BaseConfiguration.Instance.ReadConfigureStringValue("EmployeeDBName", "EmployeeConn");
        public static string EmployeeConnStr => BaseConfiguration.Instance.ReadConnectionStrValue(EmployeeDBName);
    }
}
