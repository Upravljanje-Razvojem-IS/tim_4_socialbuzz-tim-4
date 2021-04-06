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
            RuleFor(c => c.Item).NotEmpty().Length(1, 55);
            RuleFor(c => c.Weight).NotEmpty().GreaterThan(0);
            RuleFor(c => c.Pieces).NotEmpty().GreaterThan(0);
            RuleFor(c => c.WeightRangeId).NotEmpty();
            RuleFor(c => c.DeliveryServiceId).NotEmpty();
            RuleFor(c => c.FromAddressId).NotEmpty();
            RuleFor(c => c.ToAddressId).NotEmpty();
            
        }
    }
}
