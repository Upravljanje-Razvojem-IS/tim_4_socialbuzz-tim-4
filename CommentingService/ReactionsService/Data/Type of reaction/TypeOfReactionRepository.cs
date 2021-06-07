using ReactionsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Data.Type_of_reaction
{
    public class TypeOfReactionRepository : ITypeOfReactionRepository
    {
        private readonly ContextDB contextDB;

        public TypeOfReactionRepository(ContextDB contextDB)
        {
            this.contextDB = contextDB;
        }

        public void CreateTypeOfReaction(TypeOfReaction typeOfReaction)
        {
            contextDB.Add(typeOfReaction);
        }

        public void DeleteTypeOfReaction(int typeOfReactionID)
        {
            var typeOfReaction = GetTypeOfReactionByID(typeOfReactionID);
            contextDB.Remove(typeOfReaction);
        }

        public List<TypeOfReaction> GetAllTypesOfReaction()
        {
            return contextDB.TypeOfReaction.ToList();
        }

        public TypeOfReaction GetTypeOfReactionByID(int typeOfReactionID)
        {
            return contextDB.TypeOfReaction.FirstOrDefault(e => e.TypeOfReactionID == typeOfReactionID);
        }

        public bool SaveChanges()
        {
            return contextDB.SaveChanges() > 0;
        }

        public void UpdateTypeOfReaction(TypeOfReaction typeOfReaction)
        {
            //Mapiranje
        }
    }
}
