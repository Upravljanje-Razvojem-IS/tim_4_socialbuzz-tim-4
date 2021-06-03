using AutoMapper;
using Logistics.API.Models.PurchaseModels;
using Logistics.Core.Entities;

namespace Logistics.API.MapperProfiles
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<Purchase, PurchaseOverview>();
            CreateMap<Purchase, PurchaseDetails>();
            CreateMap<Purchase, PurchaseConfirmation>();
        }
    }
}
