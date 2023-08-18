using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CuentaNomina.Models;

namespace CuentaNomina.Controllers
{
    public class CuentaController : Controller
    {
        public ActionResult Alta(Cuenta cuenta)
        {
            return View(cuenta);
        }
        [HttpPost]
        public ActionResult Alta(IFormCollection usuario)
        {
            Cuenta cuenta = new Cuenta()
            {
                nombre = usuario["name"],
                clave = usuario["pass"],
            };
            AdminCuenta mant = new AdminCuenta();
            Cuenta cuenta1 = new Cuenta();
            cuenta1 = mant.Leer(cuenta.nombre);
            if (cuenta1 == null)
            {
                mant.Alta(cuenta);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                cuenta.nombre = "Ese usuario ya existe";
                return RedirectToAction("Alta", cuenta);
            }
        }
    }
}
