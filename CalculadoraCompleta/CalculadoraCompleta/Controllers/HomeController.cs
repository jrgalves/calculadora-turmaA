using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {
        // esta forma não funciona...
        // bool primeiroOperador = true;


        // GET: Home
        [HttpGet] // facultativo, pq por defeito é sempre este o verbo utilizado
        public ActionResult Index()
        {
            // inicialização dos primeiros valores da calculadora
            Session["primeiroOperador"] = true;
            Session["iniciaOperando"] = true;
            ViewBag.Display = "0";

            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string display)
        {

            // avaliar o valor atribuído à variável 'bt'
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
                    if ((bool)Session["iniciaOperando"] ||
                         display.Equals("0")) display = bt;
                    else display += bt;
                    Session["iniciaOperando"] = false;
                    break;

                case "+/-":
                    display = Convert.ToDouble(display) * -1 + "";
                    break;

                case ",":
                    if (!display.Contains(",")) display += ",";
                    break;

                case "+":
                case "-":
                case "x":
                case ":":
                case "=":
                    // se NÃO é a primeira vez que carrego num operador
                    if (!(bool)Session["primeiroOperador"])
                    {
                        // recuperar os valores dos operandos
                        double operando1 =
                            Convert.ToDouble((string)Session["primeiroOperando"]);
                        double operando2 = Convert.ToDouble(display);

                        switch ((string)Session["operadorAnterior"])
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
                        } //  switch((string)Session["operadorAnterior"])
                    } // if
                      // guardar os dados do display para utilização futura
                      // guardar o valor do 1º operando
                    Session["primeiroOperando"] = display;
                    // limpar display
                    Session["iniciaOperando"] = true;

                    if (bt.Equals("="))
                    {
                        // marcar o operador como primeiro operador
                        Session["primeiroOperador"] = true;
                    }
                    else
                    {
                        // guardar o valor do operador
                        Session["operadorAnterior"] = bt;
                        Session["primeiroOperador"] = false;
                    }

                    // marcar o display para reinício
                    Session["iniciaOperando"] = true;

                    break;

                case "C":
                    // reiniciar a calculadora
                    Session["iniciaOperando"] = true;
                    Session["primeiroOperador"] = true;
                    display = "0";
                    break;
            }

            // preparar os dados para erem enviados para a View
            ViewBag.Display = display;


            return View();
        }
    }
}