using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CuentaNomina.Models;

namespace CuentaNomina.Controllers
{
    public class NominaController : Controller
    {        
        public ActionResult Alta()
        {
            Cuenta user = new Cuenta();
            user.nombre = TempData["usuario"].ToString();
            return View(user);
        }
        [HttpPost]
        public ActionResult Alta(IFormCollection registro)
        {
            AdminNomina nomina = new AdminNomina();
            Empleado pers = new Empleado
            {
                dni = registro["dni"],
                nombre = registro["nombre"],
                domicilio = registro["domicilio"],
                fechanac = DateTime.Parse(registro["fechanac"]),
                fechaing = DateTime.Parse(registro["fechaing"]),
                puesto = registro["puesto"],
                salario = int.Parse(registro["salario"]),
                usuario = registro["usuario"]
            };
            nomina.Alta(pers);
            return RedirectToAction("Alta");
        }
        public ActionResult Leer()
        {            
            Cuenta user = new Cuenta();
            user.nombre = TempData["usuario"].ToString();
            ViewBag.usuario = TempData["usuario"].ToString();
            AdminNomina nomina = new AdminNomina();
            List<Empleado> lista = nomina.LeerTodo(user.nombre);
            if (lista.Count() == 0)
            {
                Empleado pers = new Empleado()
                {
                    usuario = user.nombre,
                    dni = null
                };
                lista.Add(pers);
            }
            return View(lista);
        }
        public ActionResult Modificacion(string dni)
        {
            AdminNomina nomina = new AdminNomina();
            Empleado pers = new Empleado();
            string usuario = TempData["usuario"].ToString();
            pers = nomina.Leer(dni, usuario);
            return View(pers);
        }
        [HttpPost]
        public ActionResult Modificacion(IFormCollection registro)
        {
            AdminNomina nomina = new AdminNomina();
            Empleado pers = new Empleado
            {
                dni = registro["dni"],
                nombre = registro["nombre"],
                domicilio = registro["domicilio"],
                fechanac = DateTime.Parse(registro["fechanac"]),
                fechaing = DateTime.Parse(registro["fechaing"]),
                puesto = registro["puesto"],
                salario = int.Parse(registro["salario"]),
                usuario = registro["usuario"]
            };
            nomina.Modificar(pers);
            return RedirectToAction("Leer");
        }

        public ActionResult Baja(string dni)
        {
            AdminNomina nomina = new AdminNomina();
            Empleado pers = new Empleado();
            string usuario = TempData["usuario"].ToString();
            pers = nomina.Leer(dni, usuario);
            nomina.Borrar(usuario, dni);
            return RedirectToAction("Leer");
        }
    }
}
