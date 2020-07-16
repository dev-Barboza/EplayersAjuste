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
    public class NoticiasController : Controller
    {
        /// <summary>
        /// ler as noticias a partir de uma bag
        /// </summary>
        /// <returns></returns>
        Noticias noticiasModel = new Noticias();
        public IActionResult Index()
        {
            ViewBag.Noticias = noticiasModel.ReadAll();
            return View();
        }

        /// <summary>
        /// Cadastra uma nova noticia
        /// </summary>
        /// <param name="form">entarará dados</param>
        /// <returns></returns>
        public IActionResult Publicar(IFormCollection form)
        {
            Noticias noticiasN = new Noticias();
            noticiasN.IdNoticia = Int32.Parse( form["IdNoticia"]);
            noticiasN.Titulo   = form["Titulo"];
            noticiasN.Texto    = form["Texto"];
            
           
            var file    = form.Files[0];

            
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias");

           
            if(file != null)
            {
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

                
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias", folder, file.FileName);
                
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                
                noticiasN.Imagem   = file.FileName;
            }
            else
            {
                noticiasN.Imagem   = "padrao.png";
            }
            

            noticiasModel.Create(noticiasN);
            return LocalRedirect("~/Noticias");
        }

        [Route("Noticias/{id}")]
        public IActionResult Excluir(int id)
        {
            noticiasModel.Delete(id);
            return LocalRedirect("~/Noticias");

        }

    }
}