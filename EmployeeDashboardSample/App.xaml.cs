﻿using System.Windows;

namespace EmployeeDashboardSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new BootStrapper();
            bootstrapper.Run();
        }
    }
}
