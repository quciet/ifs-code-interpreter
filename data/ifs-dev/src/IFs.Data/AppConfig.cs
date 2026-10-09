using System;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Data.Odbc;

namespace IFs.Data 
{
    internal class AppConfig
    {
        public static ConnectionStringSettingsCollection ConnectionStrings {
            get {
                var assembly = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
                var config = ConfigurationManager.OpenExeConfiguration(assembly.LocalPath);
                var section = config.GetSection("connectionStrings") as ConnectionStringsSection;
                return section.ConnectionStrings;
            }
        }

        public static string GetFilePathFromConnectionString(string connectionString) {
            var builder = new OdbcConnectionStringBuilder();
            builder.ConnectionString = connectionString;
            var dataSource = builder["Data Source"] as string;
            var dataDir = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            if (dataDir != null) {
                dataSource = dataSource.Replace("|DataDirectory|", dataDir);
            }
            return dataSource;
        }
    }
}
