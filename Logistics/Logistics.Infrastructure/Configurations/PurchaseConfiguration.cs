using FluentValidation;
using Logistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Configurations
{
    public class PurchaseConfiguration : AbstractValidator<Purchase>
    {
        public PurchaseConfiguration()
        {
            RuleFor(c => c.ItemId).NotEmpty();
            RuleFor(c => c.TotalWeight).NotEmpty().GreaterThan(0);
            RuleFor(c => c.TotalPriceWithWeightAndDistance).NotEmpty().GreaterThan(0);
            RuleFor(c => c.Pieces).NotEmpty().GreaterThan(0);
            RuleFor(c => c.WeightRangeId).NotEmpty();
            RuleFor(c => c.DistancePriceId).NotEmpty();
            RuleFor(c => c.FromAddressId).NotEmpty();
            RuleFor(c => c.ToAddressId).NotEmpty();
            
        }
    }
}
