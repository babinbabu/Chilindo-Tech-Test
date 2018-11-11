using ChilindoTechTest.Api.Controllers.Api;
using ChilindoTechTest.Core;
using ChilindoTechTest.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ChilindoTechTest.Api
{
    
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbDataInitialization();

        }

        private void DbDataInitialization()
        {

            using (ChilindoTechTestEntities db = new ChilindoTechTestEntities())
            {
                if (db.AccountDetails.Count() == 0)
                {
                    string Sample = "Demo";
                    foreach (Common.Enums.DefaultAccount accountNo in (Common.Enums.DefaultAccount[])Enum.GetValues(typeof(Common.Enums.DefaultAccount)))
                    {
                        var entityAccountDetail = new ChilindoTechTest.Core.Entity.AccountDetail();
                        entityAccountDetail.HolderName = Sample + " " + accountNo;
                        entityAccountDetail.Balance = 0;
                        entityAccountDetail.CurrencyType = Common.Enums.ChilindoCurrencyType.USD;
                        db.AccountDetails.Add(entityAccountDetail);

                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
