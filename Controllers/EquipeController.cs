using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EplayersC.Models;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace EplayersC.Controllers
{
    public class EquipeController : Controller
    {
        Equipe equipeModel = new Equipe();

        /// <summary>
        /// ler index view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }

        /// <summary>
        /// Cadastrar a equipe
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe equipe   = new Equipe();
            equipe.IdEquipe = Int32.Parse( form["IdEquipe"]);
            equipe.Nome     = form["Nome"];

            // Inicio Upload Imagem
            
            var file    = form.Files[0];

            
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

            
            if(file != null)
            {
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

               
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
               
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                
                equipe.Imagem   = file.FileName;
            }
            else
            {
                equipe.Imagem   = "padrao.png";
            }
           

            equipeModel.Create(equipe);
            return LocalRedirect("~/Equipe");
        }

        [Route("[controller]/{id}")]
        public IActionResult Excluir(int id)
        {
            equipeModel.Delete(id);
            return LocalRedirect("~/Equipe");

        }

    }
}