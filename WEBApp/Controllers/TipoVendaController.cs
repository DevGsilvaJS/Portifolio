using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.WEB.Model.Vendas.TabelasAuxiliares;
using UI.WEB.WorkFlow.Vendas.TabelasAuxiliares;

namespace WEBApp.Controllers
{
    public class TipoVendaController : Controller
    {
        TipoVendaWorkFlow wf = new TipoVendaWorkFlow();
        // GET: TipoVenda
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult RetornaObjInclusao()
        {
            return Json(new
            {
                ObjInclusao = wf.RetornaObjInclusao()
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListaDados()
        {
            List<EntityTipoVenda> lsTipoVenda = new List<EntityTipoVenda>();

            lsTipoVenda = wf.ListaDados();

            return Json(new
            {
                lsTipoVenda = lsTipoVenda
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ExcluirTipoVenda(int tpvid)
        {
            string sRetorno = wf.ExcluirTipoVenda(tpvid);

            return Json(new
            {
                retorno = sRetorno
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTipoVendaByID(int tpvid)
        {
            EntityTipoVenda _TipoVenda = new EntityTipoVenda();
            _TipoVenda = wf.GetTipoVendaByID(tpvid);

            return Json(new
            {
                retorno = _TipoVenda
            }, JsonRequestBehavior.AllowGet);
        }        
        public JsonResult GravarTipoVenda(EntityTipoVenda _TipoVenda)
        {
            string sRetorno = wf.GravarTipoVenda(_TipoVenda);

            return Json(new
            {
                retorno = sRetorno
            }, JsonRequestBehavior.AllowGet);
        }
    }
}