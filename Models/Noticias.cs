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
        /// <param name="ne"></param>
        public void Create(Noticias ne)
        {
            string[] linhas = {PrepararLinha(ne)};
            File.AppendAllLines(PATH, linhas);
        }

        /// <summary>
        /// Organizar linha segundo os atributos
        /// </summary>
        /// <param name="ne"></param>
        /// <returns></returns>
        private string PrepararLinha (Noticias ne)
        {
            return $"{ne.IdNoticia};{ne.Titulo};{ne.Texto};{ne.Imagem}";
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
                Noticias noticia = new Noticias();
                noticia.IdNoticia = Int32.Parse(linha[0]);
                noticia.Titulo = linha[1];
                noticia.Texto = linha[2];
                noticia.Imagem = linha[3];

                news.Add(noticia);
            }
            return news;
        }

        /// <summary>
        /// reescever e atualizar linha
        /// </summary>
        /// <param name="ne"></param>
        public void Update(Noticias ne)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            
            linhas.RemoveAll(a => a.Split(";")[0] == ne.IdNoticia.ToString());
            linhas.Add( PrepararLinha(ne) );
            RewriteCSV(PATH, linhas);
        }


        
    }
}