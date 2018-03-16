using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        [HttpGet]//facultativo , porque por defeito e sempre este o verbo do utilizador 
        public ActionResult Index()
        {
            // inicializaçao dos primeiros valores da calculadora
            Session["primeiroOperador"] = true;
            ViewBag.display = 0;
            return View();
        }
        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string display)
        {
            //avaliar o valor atribuido
            switch (bt)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":

                    if (display.Equals("0")) display = bt;
                    else display += bt;

                    break;

                case "+/-":
                    display = Convert.ToDouble(display)* -1 +"";
                    break;

                case ",":
                    if (!display.Contains(","))display += "," ;
                    break;

                case "+":
                case "-":
                case "x":
                case ":":
                    // se e a primeira vez que carrego no operador 
                    if (!(bool)Session["primeiroOperador"])
                    {
                            //recuperar os valores dos operadores 
                            double operando1 = Convert.ToDouble((string)Session["primeiroOperando"]);
                            double operando2 = Convert.ToDouble(display);
                            switch (Session["operadorAnterior"])
                            {
                                case "+":
                                    display = operando1 + operando2 + "";
                                    break;
                                case "-":
                                    display = operando1 - operando2 + "";
                                    break;
                                case "x":
                                    display = operando1 * operando2 + "";
                                    break;
                                case ":":
                                    display = operando1 / operando2 + "";
                                    break;
                            }
                        }
                        Session["primeiroOperando"] = display;
                        Session["iniciaOperando"] = true;
                    if (bt.Equals("="))
                    {
                        // marcar o operador como primeiro operador 
                        Session["primeiroOperado"] = false;
                    }
                    else
                    {
                        // guardar o valor do operador 
                        Session["operadorAnterior"] = bt;
                        Session["primeiroOperando"] = false;
                        
                    }
                    //marcar o display para reinicio
                        Session["iniciaOperando"] = true;


                    break;
                case "C":
                    //reiniciar a calculadora  
                    Session["iniciaOperando"] = true;
                    Session["primeiroOperando"] = true;
                    display = "0";
                    break;
            }
            //preparar os dados para serem enviados para a view
            ViewBag.Display = display;

            return View();
        }


    }
}