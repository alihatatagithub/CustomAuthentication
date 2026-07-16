using Microsoft.Extensions.Configuration;

namespace ECommerce.Ground
{
    public static class Configurations
    {
        public static IConfiguration ConfigurationManager { get; set; }
        private static T GetAppSettingValue<T>(string key, T defaultValue = default)
        {
            var value = ConfigurationManager[key];

            if (string.IsNullOrEmpty(value))
                return defaultValue;

            return (T)Convert.ChangeType(value, typeof(T));
        }
        private static string GetConnectionString(string appSettingsKey)
        {
            return ConfigurationManager.GetConnectionString(appSettingsKey);
        }
        public static string JWTKey => GetAppSettingValue<string>("JWT:Key");
        public static int JWTDurationMinuates => GetAppSettingValue<int>("JWT:DurationMinuates");
        public static int RefreshTokenDurationDays => GetAppSettingValue<int>("JWT:RefreshTokenDurationDays");
        public static string ConnectionString => GetConnectionString("DefaultConnection");
    }
}
