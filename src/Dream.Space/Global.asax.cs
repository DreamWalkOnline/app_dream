﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using Dream.Space.Infrastructure.IoC;
using NLog;
using GlobalConfiguration = System.Web.Http.GlobalConfiguration;

namespace Dream.Space
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IoCContainer.Instance.Register();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.ConfigureBundle();

            CertificateConfig.IgnoreCertificateErrors();
            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            var logger = LogManager.GetCurrentClassLogger();
            logger.Error($"Message:{error.Message}, Stack trace:{error.StackTrace}");
        }
    }
}
