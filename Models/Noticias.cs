using System;
using System.Collections.Generic;
using System.IO;
using EplayersC.Interfaces;

namespace EplayersC.Models
{
    public class Noticias : EPlayersBase , INoticias
    {
        public int IdNoticia { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Imagem { get; set; }

        private const string PATH = "Database/noticias.csv";

        public Noticias()
        {
            CreateFolderAndFile(PATH);
        }

        /// <summary>
        /// criar Pasta e caminho PATH
        /// </summary>
        /// <param name="n"></param>
        public void Create(Noticias n)
        {
            string[] linhas = {PrepareLine(n)};
            File.AppendAllLines(PATH, linhas);
        }

        /// <summary>
        ///  Organizar linha segundo os atributos
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private string PrepareLine (Noticias x)
        {
            return $"{x.IdNoticia};{x.Titulo};{x.Texto};{x.Imagem}";
        }

        /// <summary>
        /// Deletar determinada noticia
        /// </summary>
        /// <param name="IdNoticia"></param>
        public void Delete(int IdNoticia)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            
            linhas.RemoveAll(a => a.Split(";")[0] == IdNoticia.ToString());

            RewriteCSV(PATH, linhas);
        }

        /// <summary>
        /// retornar e ler as noticias com atributos dados
        /// </summary>
        /// <returns></returns>
        public List<Noticias> ReadAll()
        {
            List<Noticias> news = new List<Noticias>();
            string[] linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");
                Noticias report = new Noticias();
                report.IdNoticia = Int32.Parse(linha[0]);
                report.Titulo = linha[1];
                report.Texto = linha[2];
                report.Imagem = linha[3];

                news.Add(report);
            }
            return news;
        }

        /// <summary>
        /// reescever e atualizar linha
        /// </summary>
        /// <param name="n"></param>
        public void Update(Noticias x)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
           
            linhas.RemoveAll(a => a.Split(";")[0] == x.IdNoticia.ToString());
            linhas.Add( PrepareLine(x) );
            RewriteCSV(PATH, linhas);
        }


        
    }
}