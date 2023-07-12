using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.WEB.Model.Estoque;
using UI.WEB.WorkFlow.Estoque;

namespace WEBApp.Controllers
{
    public class ProdutoController : Controller
    {

        ProdutoWorkFlow wf = new ProdutoWorkFlow();
        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult RetornaObjInclusao()
        {
            EntityProduto ObjInclusao = new EntityProduto();
            ObjInclusao = wf.RetornaObjInclusao();

            return Json(new
            {
                ObjInclusao = ObjInclusao
            }, JsonRequestBehavior.AllowGet);
        }
    }
}