namespace SDPJob.Configuration
{
    public static class Appsetting
    {
        public static IConfiguration AppSetting { get; }
        static Appsetting()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
        public static string GetStringConnection() {
            return AppSetting.GetConnectionString("SoudaphoneContext");
        }

    }
}
