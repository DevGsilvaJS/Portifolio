using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBApp.Controllers
{
    public class ResetTablesController : Controller
    {
        public ActionResult ResetarTabelas()
        {
            string resetTabelas = ConfigurationManager.AppSettings["ResetTabelas"];

            if (!string.IsNullOrEmpty(resetTabelas) && resetTabelas.ToLower() == "true")
            {
                // Lógica para resetar as tabelas
                ResetarTabelasSQL();
            }

            // Redirecionar para uma página ou retornar um resultado específico
            return RedirectToAction("Index", "Home");
        }

        private void ResetarTabelasSQL()
        {
            // Lógica para resetar as tabelas do SQL Server
        }
    }
}