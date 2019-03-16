using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //inicializa a viewBag a '0'
            ViewBag.Ecra = "0";
            Session["primeiraVezOperador"] = true;
            Session["teste1"] = false;
            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string visor)
        {
            //var, auxiliar
            string ecra = visor;

            //identificar o valor da variável "bt"
            switch (bt) {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    // se entrei aqui, é pq foi selecionado um 'algarismo'
                    // vou decidir o que fazer qd no visor só existir o 'zero'
                    if (visor == "0") // if(visor.equals("0"))
                    { 
                        ecra = bt;
                    }
                    else {
                        ecra = visor + bt;
                    }
                    break;
                case ",":
                    if (!visor.Contains(",")) {
                        ecra = visor + bt;
                    }
                    break;

                // se entrar aqui é pq selecionou um operador
                case "+":
                case "-":
                case "*":
                case "/":
                    if ((bool)Session["primeiraVezOperador"] == true)
                    {
                        Session["primeiraVezOperador"] = false;
                    }
                    else {
                    }
                    break;
            }
            ViewBag.Ecra = ecra;
            return View();
        }

    }
}