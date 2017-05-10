using ISP.DAL;
using ISP.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ISP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitializeDB();
            SetCultureSettings();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void InitializeDB()
        {
            if (Database.Exists("ISPConnectionString") == false)
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<ISPContext, Configuration>());
                using (ISPContext db = new ISPContext())
                {
                    db.Database.Initialize(true);
                }
            }
        }

        public void SetCultureSettings()
        {
            CultureInfo culture_settings = new CultureInfo("ru-RU", true);
            culture_settings.DateTimeFormat.DateSeparator = ".";
            culture_settings.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            culture_settings.DateTimeFormat.LongDatePattern = "d MMMM yyyy 'г.'";

            culture_settings.DateTimeFormat.TimeSeparator = ":";
            culture_settings.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            culture_settings.DateTimeFormat.ShortTimePattern = "HH:mm";

            culture_settings.NumberFormat.NumberDecimalSeparator = ".";
            culture_settings.NumberFormat.NumberDecimalDigits = 2;
            culture_settings.NumberFormat.NumberGroupSeparator = "";

            culture_settings.NumberFormat.PercentDecimalSeparator = ".";
            culture_settings.NumberFormat.PercentDecimalDigits = 2;
            culture_settings.NumberFormat.PercentGroupSeparator = "";
            culture_settings.NumberFormat.PercentSymbol = "%";
            culture_settings.NumberFormat.PercentPositivePattern = 1;
            culture_settings.NumberFormat.PercentNegativePattern = 1;

            Thread.CurrentThread.CurrentCulture = culture_settings;
            Thread.CurrentThread.CurrentUICulture = culture_settings;
            CultureInfo.DefaultThreadCurrentCulture = culture_settings;
            CultureInfo.DefaultThreadCurrentUICulture = culture_settings;
        }
    }
}