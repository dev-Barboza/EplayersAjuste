using System.Collections.Generic;
using EplayersC.Models;

namespace EplayersC.Interfaces
{
    public interface INoticias
    {
        void Create(Noticias noticias );

         List<Noticias> ReadAll();

         void Update(Noticias noticias);

         void Delete(int IdNoticia);
    }
}