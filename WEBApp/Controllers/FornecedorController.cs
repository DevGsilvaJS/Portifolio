using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.WEB.Model.Financeiro.Tabelas_Auxiliares;
using UI.WEB.WorkFlow.Outros;

namespace WEBApp.Controllers
{
    public class FornecedorController : Controller
    {

        ListasGenericasWorkFlow wfListas = new ListasGenericasWorkFlow();
        // GET: Fornecedor
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ComboCentroCusto()
        {

            List<EntityCentroCusto> lista = new List<EntityCentroCusto>();

            lista = wfListas.listaCentroCusto();

            return Json(new
            {
                retorno = lista
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ComboPlanoContas()
        {

            List<EntityPlanoContas> lista = new List<EntityPlanoContas>();

            lista = wfListas.listaPlanoContas();

            return Json(new
            {
                retorno = lista
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
