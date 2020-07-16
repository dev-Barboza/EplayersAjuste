using System.Collections.Generic;
using EplayersC.Models;

namespace EplayersC.Interfaces
{
    public interface INoticias
    {
         void Create(Noticias n);
        List<Noticias> ReadAll();
        void Update(Noticias n);
        void Delete(int IdNoticia);
        
    }
}