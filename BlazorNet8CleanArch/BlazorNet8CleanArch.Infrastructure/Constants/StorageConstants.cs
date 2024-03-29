﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorNet8CleanArch.Infrastructure.Constants
{
    public static class StorageConstants
    {
        public static class Local
        {
            public static string Preference = "clientPreference";

            public static string JWTTokenStorageKeyName = "jwtToken";

            public static string HttpClientAuthBaseAddress = "https://api45gabs.azurewebsites.net/";

            public static string UserImageURL = "userImageURL";

            public static string BingPhoto = "";
        }

        public static class Server
        {
            public static string Preference = "serverPreference";

            //TODO - add
        }
    }
}
