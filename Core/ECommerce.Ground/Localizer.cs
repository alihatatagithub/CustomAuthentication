using System.Globalization;
using System.Resources;

namespace ECommerce.Ground
{
    public static class Localizer
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("ECommerce.Ground.Resources.SharedResources", typeof(Localizer).Assembly);

        public static string GetString(string key)
        {
            try
            {
                return _resourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? key;
            }
            catch
            {
                return key;
            }
        }

        public static string GetString(string key, params object[] args)
        {
            try
            {
                var value = _resourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? key;
                return string.Format(value, args);
            }
            catch
            {
                return key;
            }
        }
    }
}
