﻿using System;

namespace Totvs.Sample.Shop.Domain
{
    public class Constants
    {
        public const string LocalizationSourceName = "Totvs.Sample.Shop";

        private const string ENVIRONMENT_VARIABLE = "ASPNETCORE_ENVIRONMENT";
        private const string DEV_ENVIRONMENT_VARIABLE = "DEVELOPMENT";

        public static bool IsDevelopment()
        {
            var environmentName = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE);
            if (environmentName.ToUpperInvariant() == DEV_ENVIRONMENT_VARIABLE)
                return true;

            return false;
        }
    }
}
