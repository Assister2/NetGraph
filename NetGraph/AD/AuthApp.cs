using System;
using System.IO;
using Microsoft.Identity.Client;

namespace CyConex
{
    public partial class AuthApp
    {
        private static readonly string TenantName = "bluesquaresoftware";// ConfigurationManager.AppSettings["TenantName"];
        private static readonly string Tenant = $"{TenantName}.onmicrosoft.com";
        private static readonly string AzureAdB2CHostname = $"{TenantName}.b2clogin.com";

        /// <summary>
        /// ClientId for the application which initiates the login functionality (this app)  
        /// </summary>
        private static readonly string ClientId = "0e4738a1-e093-4fb1-9b40-78e3251f0b26";// ConfigurationManager.AppSettings["AADAppClientId"];

        /// <summary>
        /// Should be one of the choices on the Azure AD B2c / [This App] / Authentication blade
        /// </summary>
        private static readonly string RedirectUri = $"https://{TenantName}.b2clogin.com/oauth2/nativeclient";

        /// <summary>
        /// From Azure AD B2C / UserFlows blade
        /// </summary>        
        public static string PolicySignUpSignIn = "B2C_1_NetGraphUserFlow";// ConfigurationManager.AppSettings["PolicySignUpSignIn"];
        /// <summary>
        /// Note: AcquireTokenInteractive will fail to get the AccessToken if "Admin Consent" has not been granted to this scope.  To achieve this:
        /// 
        /// 1st: Azure AD B2C / App registrations / [API App] / Expose an API / Add a scope
        /// 2nd: Azure AD B2C / App registrations / [This App] / API Permissions / Add a permission / My APIs / [API App] / Select & Add Permissions
        /// 3rd: Azure AD B2C / App registrations / [This App] / API Permissions / ... (next to add a permission) / Grant Admin Consent for [tenant]
        /// </summary>
        public static string[] ApiScopes = { "https://bluesquaresoftware.onmicrosoft.com/f6b4f665-8f16-4578-b852-b413d3345a5a/NetGraph.Read" };// { ConfigurationManager.AppSettings["ApiScopes"] };
        public static string AzureFunctionUrl = "https://netgraphfunctionapp.azurewebsites.net/api/v1/useridentities?code=p5mA4aZMQNotHX_8-eZiiYSJLk859TaCIcThL3RJGeEoAzFuaO2Rjw==";// ConfigurationManager.AppSettings["AzureFunctionUrl"];

        private static string AuthorityBase = $"https://{AzureAdB2CHostname}/tfp/{Tenant}/";
        public static string AuthoritySignUpSignIn = $"{AuthorityBase}{PolicySignUpSignIn}";
        //public static string AuthoritySignIn = $"{AuthorityBase}{PolicySignUpSignIn}";
        public static IPublicClientApplication PublicClientApp { get; private set; }

        static AuthApp()
        {
            PublicClientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithB2CAuthority(AuthoritySignUpSignIn)
                .WithRedirectUri(RedirectUri)
                .WithLogging(Log, LogLevel.Info, false)
                .Build();

            TokenCacheHelper.Bind(PublicClientApp.UserTokenCache);
        }

        private static void Log(LogLevel level, string message, bool containsPii)
        {
            string logs = $"{level} {message}{Environment.NewLine}";
            File.AppendAllText(System.Reflection.Assembly.GetExecutingAssembly().Location + ".msalLogs.txt", logs);
        }
    }
}
