using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASMicroservice.Entities;

namespace PASMicroservice.Repositories
{
    public interface IPASTypeRepository
    {
        List<PASType> GetTypes();
        PASType GetTypeById(int id);
        PASTypeConfirmation CreateType(PASType type);
        PASTypeConfirmation UpdateType(PASType type);
        void DeleteType(int id);
        void Save();
    }
}
