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
            Session["write"] = true;
            Session["op"] = "";
            Session["aux"] = 0;
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
                case "+/-":
                    if (visor != "0")
                    {
                        if (!visor.Contains("-"))
                        {
                            ecra = "-" + visor;
                        }
                        else
                        {
                            ecra = visor.Replace("-", "");
                        }
                    }
                break;

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
                    else if (!(bool)Session["write"]) {
                        ecra = bt;
                        Session["write"] = true;
                    }
                    else
                    {
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
                    if ((bool)Session["primeiraVezOperador"])
                    {
                        Session["primeiraVezOperador"] = false;
                        Session["op"] = bt;
                        Session["aux"] = Convert.ToDouble(visor);
                        Session["write"] = false;
                    }
                    else
                    {
                        Session["write"] = false;
                        switch ((String)Session["op"])
                        {
                            case "+":
                                Session["aux"] = (double)Session["aux"] + Convert.ToDouble(visor);
                                break;
                            case "-":
                                Session["aux"] = (double)Session["aux"] - Convert.ToDouble(visor);
                                break;
                            case "*":
                                Session["aux"] = (double)Session["aux"] * Convert.ToDouble(visor);
                                break;
                            case "/":
                                if (visor == "0"){ }
                                else
                                {
                                    Session["aux"] = (double)Session["aux"] / Convert.ToDouble(visor);
                                }
                            break;
                        }
                        Session["op"] = bt;
                    }
                    break;

                //ao carregar no igual
                case "=":
                    String op = (String)Session["op"];
                    if ( op == "") {
                        break;
                    }
                    else
                    {
                        switch ((String)Session["op"])
                        {
                            case "+":
                                Session["aux"] = (double)Session["aux"] + Convert.ToDouble(visor);
                                break;
                            case "-":
                                Session["aux"] = (double)Session["aux"] - Convert.ToDouble(visor);
                                break;
                            case "*":
                                Session["aux"] = (double)Session["aux"] * Convert.ToDouble(visor);
                                break;
                            case "/":
                                if (visor == "0")
                                    ecra = "Nao te armes em esperto amigo";
                                else
                                {
                                    Session["aux"] = (double)Session["aux"] / Convert.ToDouble(visor);
                                }
                                break;
                        }

                        ecra = Convert.ToString(Session["aux"]);

                        Session["primeiraVezOperador"] = true;
                        Session["write"] = false;
                        Session["op"] = "";
                        Session["aux"] = 0;
                        break;
                    }

                case "C":
                    Session["primeiraVezOperador"] = true;
                    Session["write"] = true;
                    Session["op"] = "";
                    Session["aux"] = 0;
                    ecra = "0";
                    break;
            }
            ViewBag.Ecra = ecra;
            return View();
        }

    }
}