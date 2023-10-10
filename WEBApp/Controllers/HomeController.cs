using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session.Count == 0 && Session["_AutenticacaoRetornoModel"] == null)
            {
                return Redirect("/Login");
            }

            return View();
        }

        [HttpPost]
        public JsonResult ValidaSessao()
        {
            bool isLogado = false;

            if (Session["_userLogado"] != null)
            {
                isLogado = true;
            }
            

            try
            {
                var resultado = new
                {
                    isLogado = isLogado
                };

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public JsonResult SetUsuarioLogado()
        {
            bool isLogado = false;
            string emailUsuario = "";


            if (Session["_isLogado"] != null)
            {
                 emailUsuario = Session["_userEmail"].ToString();
                 isLogado = (bool)Session["_isLogado"];

            }


            try
            {
                var resultado = new
                {
                    isLogado = isLogado,
                    emailUsuario = emailUsuario
                };

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        [HttpPost]
        public JsonResult Logoff()
        {
            bool isLogado = false;

            if (Session["_userLogado"] != null)
            {
                Session.Remove("_userLogado");
            }
            try
            {
                var resultado = new
                {
                    isLogado = isLogado
                };

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}