namespace Sample.EmployeeModule.DI
{
    public static class EmployeeModuleDIResolver
    {
        private static readonly object WorkjetFlowDiCoreLock = new object();
        private static readonly EmployeeModuleDIRegister EMRegister = new EmployeeModuleDIRegister();


        public static bool HasConfigured => EMRegister.Initialized;


        public static void ConfigureDI()
        {
            lock (WorkjetFlowDiCoreLock)
            {
                if (!HasConfigured)
                    EMRegister.Initialize();
            }
        }


        public static T Resolve<T>() where T : class
        {
            return EMRegister.Resolve<T>();
        }

        public static void Register<TInt, TImpl>() where TInt : class where TImpl : class, TInt
        {
            EMRegister.Register<TInt, TImpl>();
        }
    }
}
