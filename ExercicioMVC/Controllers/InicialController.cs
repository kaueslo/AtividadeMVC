using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace ExercicioMVC.NET_MVC.Controllers
{
    public class InicialController : Controller
    {

        public String strConexaoBanco = "data source="+ WebConfigurationManager.AppSettings["Servidor"] +"; initial catalog=" + WebConfigurationManager.AppSettings["Banco"] +";user id="+WebConfigurationManager.AppSettings["Usuario"]+";password="+WebConfigurationManager.AppSettings["Senha"]+";";

       

        // GET: Inicial
        public ActionResult Index()
        {
            Session["strConexaoBanco"] = strConexaoBanco;

            return View();
        }
    }
}