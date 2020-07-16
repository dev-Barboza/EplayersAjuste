  
using System.Collections.Generic;
using EplayersC.Models;

namespace EplayersC.Interfaces
{
    public interface IEquipe
    {
        void Create(Equipe e);
        List<Equipe> ReadAll();
        void Update(Equipe e);
        void Delete(int IdEquipe); 
    }
}