using CuentaNomina.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CuentaNomina.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Bienvenido(IFormCollection login)
        {
            Cuenta cuenta = new Cuenta();
            AdminCuenta mant = new AdminCuenta();
            cuenta = mant.Leer(login["name"]);
            if (cuenta == null)
            {
                Cuenta msj = new Cuenta();
                msj.nombre = "Usuario inexistente";
                return View("Index", msj);
            }
            else
            {
                if (cuenta.clave != login["pass"])
                {
                    cuenta.nombre = "Clave incorrecta";
                    return View("Index", cuenta);

                }
                else
                {
                    return View(cuenta);
                }
            }
        }
    }
}