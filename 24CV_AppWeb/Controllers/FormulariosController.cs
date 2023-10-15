﻿using _24CV_AppWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace _24CV_AppWeb.Controllers
{
    public class FormulariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarCorreo(string email, string comentario)
        {
            TempData["Email"]=email;
            TempData["Comentario"] = comentario;
            return View("Index");
        }

        [HttpPost]
        public IActionResult EnviarInformacion(EmailViewModel model)
        {
            TempData["Email"] = model.Email;
            TempData["Comentario"] = model.Comentario;
            SendEmail(model.Email,model.Comentario);
            return View("Index", model);
        }
        public bool SendEmail(string email,string comentario)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("mail.shapp.mx",587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("moises.puc@shapp.mx", "Dhaserck_999");
            mail.From = new MailAddress("moises.puc@shapp.mx", "Administrador");
            mail.To.Add(email);
            mail.Subject = "Notificación de contacto";
            mail.IsBodyHtml = true;
            mail.Body= $"Se ha recibido información del correo <h1>{email}</h1> <br/> <p>{comentario}</p>";
            smtp.Send(mail);
            return true;
        }
        public IActionResult Contacto()
        {
            return View();
        }

        public IActionResult EnviarContacto(InformacionViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ProcesarInformacion(model))
                {
                    TempData["Mensaje"] = "Comentario enviado con éxito.";
                }
                else
                {
                    TempData["Mensaje"] = "Error al enviar el comentario.";
                }
            }
            return View("Contacto", model);
        }
        public bool ProcesarInformacion(InformacionViewModel model)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("mail.shapp.mx", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("moises.puc@shapp.mx", "Dhaserck_999");
                mail.From = new MailAddress("moises.puc@shapp.mx", "Administrador");
                mail.To.Add(model.Email);
                mail.Subject = "Notificación de contacto";
                mail.IsBodyHtml = true;
                mail.Body = $"<h1>Solicitud de contacto</h1> " +
                            $"<p>Nombre: {model.Nombre}</p>" +
                            $"<p>Apellido: {model.Apellido}</p>" +
                            $"<p>Email: {model.Email}</p>" +
                            $"<p>Fecha de Nacimiento: {model.FechaNacimiento}</p>" +
                            $"<p>Turno: {model.Turno}</p>" +
                            $"<p>Comentarios: {model.Comentarios}</p>";
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al enviar el correo", ex);
            }
        }
    }
}
