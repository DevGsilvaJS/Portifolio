using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.WEB.WorkFlow.Outros;

namespace WEBApp.Controllers
{
    public class ResetTablesController : Controller
    {
        ResetTablesWorkFlow wf = new ResetTablesWorkFlow();
        public JsonResult ResetTables()
        {

            string sRetorno = wf.CreateTables();
    
            return Json(new
            {
                retorno = sRetorno
            }, JsonRequestBehavior.AllowGet);
        }
    }
}