#6 startup giriş güvenliği 
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using JobSearch.Models;

namespace JobSearch
{
    public partial class Startup
    {
        
        public void ConfigureAuth(IAppBuilder app)
        {
        //  kullanıcı yöneticisini ve oturum açma yöneticisini yapılandırın
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            
            
            //üçüncü taraf oturum açma sağlayıcısı ile oturum açan bir kullanıcı hakkındaki bilgileri geçici olarak depolamak için bir tanımlama bilgisi kullanmak
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    //Kullanıcı oturum açtığında uygulamanın güvenlik damgasını doğrulamasını sağlar.
                    // Bu, bir şifre değiştirdiğinizde veya hesabınıza harici bir giriş eklediğinizde kullanılan bir güvenlik özelliğidir.
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

          
        }
    }
}