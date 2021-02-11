namespace WebBucketApp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebBucketApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebBucketApp.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WebBucketApp.ApplicationDbContext";
        }

        protected override void Seed(WebBucketApp.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var comp = new CompanyToken()
            {
                Branch = "Admin",
                CompToken = "A49349",
                ExpirationDate = DateTime.Today,
                TrxnDate = new DateTime(2040, 01, 01),
                Email = "olasupoolawale@outlook.com"

            };
            context.CompanyTokens.AddOrUpdate();
            context.SaveChanges();
        }
    }
}
