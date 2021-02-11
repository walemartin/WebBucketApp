using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using WebBucketApp.Models;

namespace WebBucketApp
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return ConfigOutlookAsync(message);
            //return Task.FromResult(0);
        }
        private Task ConfigOutlookAsync(IdentityMessage message)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.live.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("olasupoolawale@outlook.com", "dogPOUND"),

                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            }; //587

            MailMessage mail = new MailMessage
            {
                From = new MailAddress("olasupoolawale@outlook.com", "no-reply")
            };
            mail.To.Add(new MailAddress(message.Destination));
            //mail.CC.Add(new MailAddress("jcborlagdan@ymail.com"));
            mail.Subject = message.Subject;
            mail.IsBodyHtml = true;

            mail.Body = message.Body;
            //smtpClient.Send(mail);
            // Send:
            return smtpClient.SendMailAsync(mail);


        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
        { 
        }
        static ApplicationDbContext()
        {
            // Set the database initializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WebBucketApp.Models.CompanyToken> CompanyTokens { get; set; }

        public System.Data.Entity.DbSet<WebBucketApp.Models.RegisterViewModel> RegisterViewModels { get; set; }

        public System.Data.Entity.DbSet<WebBucketApp.ViewModels.EditUserViewModel> EditUserViewModels { get; set; }

        public System.Data.Entity.DbSet<WebBucketApp.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<WebBucketApp.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<WebBucketApp.Models.ClientDetail> ClientDetails { get; set; }

        public System.Data.Entity.DbSet<WebBucketApp.Models.LaundryManager> LaundryManagers { get; set; }

        public System.Data.Entity.DbSet<WebBucketApp.Models.ImageDir> ImageDirs { get; set; }

        public System.Data.Entity.DbSet<WebBucketApp.Models.LaundryPayment> LaundryPayments { get; set; }

        public System.Data.Entity.DbSet<WebBucketApp.Models.WashWorkFlow> WashWorkFlows { get; set; }

        public System.Data.Entity.DbSet<WebBucketApp.Models.Sales> Sales { get; set; }
    }
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
        : base(roleStore)
        {
        }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager>
        options, IOwinContext context)
        {
            return new ApplicationRoleManager(new
            RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }
        //Create User=olasupoolawale@outlook.com with Admin@Sys24net in the Admin role
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager =
            HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().
            Get<ApplicationRoleManager>();
            const string name = "olasupoolawale@outlook.com";
            const string password = "Admin@Sys24net";
            const string roleName = "Admin";
            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }
            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = name,
                    Email = name,
                    FirstName="Olawale",
                    LastName="Olasupo",
                    TrxnDate=DateTime.Now,
                    CompToken= "A49349",
                    CompanyTokenId=1,
                    PhoneNo="08062667809",
                    Address="1, Salawe Avenue, Off Love Street, Ketu, Lagos",
                };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }
            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
            //Create users role
            const string userRoleName = "Users";
            role = roleManager.FindByName(userRoleName);
            if (role == null)
            {
                role = new IdentityRole(userRoleName);
                var roleresult = roleManager.Create(role);
            }
        }
    }
}
