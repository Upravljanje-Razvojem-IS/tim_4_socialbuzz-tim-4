using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASMicroservice.Models;

namespace PASMicroservice.Repositories
{
    interface IProductsAndServicesRepository
    {
        IEnumerable<ProductsAndServices> GetPAS();
        ProductsAndServices GetPASById(Guid id);
        void InsertPAS(ProductsAndServices PAS);
        void UpdatePAS(ProductsAndServices PAS);
        void DeletePAS(Guid id);
        void Save();
    }
}
