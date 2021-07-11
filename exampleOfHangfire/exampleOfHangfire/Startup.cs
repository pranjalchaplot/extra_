using exampleOfHangfire.Controllers;
using exampleOfHangfire.Models;
using Hangfire;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(exampleOfHangfire.Startup))]
namespace exampleOfHangfire
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            app.UseHangfireDashboard(); // "/hangfire", new DashboardOptions() { Authorization = new[] { new HangfireAthorizationFilter() } }
            //BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));

            HomeController obj = new HomeController();
            RecurringJob.AddOrUpdate(() => obj.SendEmail(), Cron.Minutely);
            app.UseHangfireServer();
        }
    }
}
