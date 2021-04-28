﻿using AutoMapper;
using Logistics.API.Models.PurchaseModels;
using Logistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
