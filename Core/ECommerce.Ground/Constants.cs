using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Ground
{
    public static class Constants
    {
        public const int DefaultPage = 1;
        public const int DefaultPageSize = 10;
        public static class Media
        {
            public const string UploadsFolderName = "Uploads";
            public const string CategoryFolder = "Category";
        }
        public static class SystemRoleConstants
        {
            public const string Admin = "Admin";
            public const string Vendor = "Vendor";
            public const string Customer = "Customer";

        }
    }
}
