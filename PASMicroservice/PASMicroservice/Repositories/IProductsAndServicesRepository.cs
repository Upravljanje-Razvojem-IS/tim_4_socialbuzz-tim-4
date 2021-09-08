using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASMicroservice.Entities;

namespace PASMicroservice.Repositories
{
    public interface IProductsAndServicesRepository
    {
        List<ProductsAndServices> GetPAS();
        ProductsAndServices GetPASById(Guid id);
        ProductsAndServicesConfirmation CreatePAS(ProductsAndServices pas);
        ProductsAndServicesConfirmation UpdatePAS(ProductsAndServices pas);
        void DeletePAS(Guid id);
        void Save();
    }
}
