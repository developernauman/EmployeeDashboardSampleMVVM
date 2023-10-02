using Sample.Common.Configuration;

namespace Sample.Common
{
    public static class APIDBConfig
    {
        public static string DBName => BaseConfiguration.Instance.ReadConfigureStringValue("DBName", "DBName");
        public static string ConnStr => BaseConfiguration.Instance.ReadConnectionStrValue(DBName);
    }
}
