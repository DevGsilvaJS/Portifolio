using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.WEB.WorkFlow.Vendas.TabelasAuxiliares;

namespace WEBApp.Controllers
{
    public class ConvenioController : Controller
    {
        ConvenioWorkFlow wf = new ConvenioWorkFlow();
        // GET: Convenio
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult RetornaObjInclusao()
        {

            EntityConvenio ObjInclusao = new EntityConvenio();

            ObjInclusao = wf.RetornaObjInclusao();

            return Json(new
            {
                retorno = ObjInclusao
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GravarConvenio(EntityConvenio _Convenio)
        {
            string sRetorno = "NOTOK";

            sRetorno = wf.GravarConvenio(_Convenio);

            return Json(new
            {
                retorno = sRetorno
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListaDados()
        {
            List<EntityConvenio> lsConvenio = new List<EntityConvenio>();

            lsConvenio = wf.ListaDados();

            return Json(new
            {
                lsConvenio = lsConvenio
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetConvenioByID(int cvnid)
        {
            EntityConvenio _Convenio = new EntityConvenio();
            _Convenio = wf.GetConvenioByID(cvnid);

            return Json(new
            {
                retorno = _Convenio
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ExcluirConvenio(int cvnid)
        {
            string sRetorno = wf.ExcluirConvenio(cvnid);

            return Json(new
            {
                retorno = sRetorno
            }, JsonRequestBehavior.AllowGet);
        }
    }
}