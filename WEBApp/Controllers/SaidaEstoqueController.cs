using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.WEB.Model.Estoque;
using UI.WEB.Model.Fiscal.Tabelas_Auxiliares;
using UI.WEB.WorkFlow.Estoque;

namespace WEBApp.Controllers
{
    public class SaidaEstoqueController : Controller
    {
        SaidaEstoqueWorkFlow wf = new SaidaEstoqueWorkFlow();
        // GET: SaidaEstoque
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult RetornaObjInclusao()
        {
            EntityNotaFiscal obj = new EntityNotaFiscal();
            obj = wf.RetornaObjInclusao();

            return Json(new
            {
                ObjInclusao = obj
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RetornaComboCfops()
        {
            List<EntityCFOP> listaCfops = new List<EntityCFOP>();
            listaCfops = wf.RetornaComboCfops();

            return Json(new
            {
                listaCfops = listaCfops
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RetornaEntityProduto(string produto)
        {
            List<EntityProduto> Produto = new List<EntityProduto>();
            Produto = wf.RetornaListaSaida(produto);

            return Json(new
            {
                Produto = Produto
            }, JsonRequestBehavior.AllowGet);
        }
    }
}