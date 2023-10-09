using _24CV_AppWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace _24CV_AppWeb.Controllers
{
    public class Ejemploscontroller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contacto()
        {
            Persona persona = new Persona();
            persona.Nombre =  "Moises";
            persona.Apellidos =  "Torres";
            persona.FechaNacimiento = new DateTime(1982, 01, 15);
            return View(persona);
        }
    }
}
