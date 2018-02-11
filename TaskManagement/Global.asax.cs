using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Mvc;
using TaskMangement;

namespace API {
    public class WebApiApplication: System.Web.HttpApplication {
        protected void Application_Start() {

            RouteTable.Routes.RouteExistingFiles = true;

            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
            
        }
    }
}
