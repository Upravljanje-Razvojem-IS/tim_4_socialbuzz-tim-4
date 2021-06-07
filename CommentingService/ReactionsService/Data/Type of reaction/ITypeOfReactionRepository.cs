using ReactionsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Data.Type_of_reaction
{
    public interface ITypeOfReactionRepository
    {
        List<TypeOfReaction> GetAllTypesOfReaction();

        TypeOfReaction GetTypeOfReactionByID(int typeOfReactionID);

        void CreateTypeOfReaction(TypeOfReaction typeOfReaction);

        void UpdateTypeOfReaction(TypeOfReaction typeOfReaction);

        void DeleteTypeOfReaction(int typeOfReactionID);

        public bool SaveChanges();
    }
}
