using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.WEB.Model.Utilitarios;
using WorkFlow.Utilitarios;

namespace WEBApp.Controllers
{
    public class LoginController : Controller
    {
        UsuarioWorkFlow wf = new UsuarioWorkFlow();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidaLogin(string Usuario, string Senha)
        {
            EntityUsuario usu = new EntityUsuario();
            EntityUsuario _Usuario = new EntityUsuario();

            _Usuario = wf.ValidaUsuario(Usuario, Senha);

            if (_Usuario.TbEmail.EMLEMAIL != null && _Usuario.TbEmail.EMLEMAIL != "")
            {
                Session.Add("_userLogado", true);
            }


            try
            {
                var resultado = new
                {
                    isLogado = usu
                };

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public JsonResult NotLogado()
        {
            bool logado = false;
            
                if (Session["_userLogado"] != null)
                {
                    logado = true;                    
                }
                else
                {
                    logado = false;                 
                }                 
                var resultado = new
                {
                    isLogado = logado
                };

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }  
        }
    }
