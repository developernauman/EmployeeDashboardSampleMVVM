using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace Sample.Common.Configuration
{
    public sealed class BaseConfiguration
    {
        #region Private Static Members

        private static BaseConfiguration _instance;
        private static readonly object ConfigLock = new object();

        #endregion


        #region Private Members

        private IConfigurationRoot _configRoot;
        private const string AppSettingJson = "appsettings.json";
        private const string AppSettingNode = "ApplicationSettings";
        private readonly ConcurrentDictionary<string, string> StrConfig = new ConcurrentDictionary<string, string>();

        #endregion


        #region Constructor

        private BaseConfiguration()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), AppSettingJson);
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile(path, true).AddEnvironmentVariables();

            _configRoot = configurationBuilder.Build();
        }

        #endregion


        #region Public Static Methods

        public static BaseConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (ConfigLock)
                    {
                        _instance = new BaseConfiguration();
                    }
                }

                return _instance;
            }
        }

        #endregion


        #region Public Members

        public string ReadConnectionStrValue(string config)
        {
            return _configRoot.GetConnectionString(config);
        }

        public string ReadConfigureStringValue(string config, string defaultValue = "")
        {
            if (StrConfig.ContainsKey(config))
            {
                return StrConfig[config];
            }

            string strVal;

            if (!string.IsNullOrEmpty(_configRoot.GetSection(AppSettingNode)[config]))
            {
                strVal = _configRoot.GetSection(AppSettingNode)[config];
            }
            else
            {
                return defaultValue;
            }

            lock (ConfigLock)
            {
                if (!StrConfig.ContainsKey(config))
                {
                    StrConfig.TryAdd(config, strVal);
                }
            }


            return StrConfig[config];
        }

        #endregion
    }
}
