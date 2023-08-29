using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.WEB.Model.Utilitarios;
using WorkFlow.Utilitarios;

namespace WEBApp.Controllers
{
    public class UsuarioController : Controller
    {

        UsuarioWorkFlow wf = new UsuarioWorkFlow();
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult RetornaSequencial()
        {

            string retorno = "";

            retorno = wf.RetornaSequencial();

            return Json(new
            {
                retorno = retorno
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RetornaObjInclusao()
        {
            return Json(new
            {
                ObjInclusao = wf.RetornaObjInclusao()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GravarUsuario(EntityUsuario _Usuario)
        {
            string sRetorno = wf.GravarUsuario(_Usuario);

            return Json(new
            {
                retorno = sRetorno
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListaDados()
        {
            List<EntityUsuario> lsUsuario = new List<EntityUsuario>();

            lsUsuario = wf.ListaDados();

            return Json(new
            {
                lsUsuario = lsUsuario
            }, JsonRequestBehavior.AllowGet);
        }
    }
}