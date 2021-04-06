using FluentValidation;
using Logistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Configurations
{
    public class AddressConfiguration : AbstractValidator<Address>
    {
        public AddressConfiguration()
        {
            RuleFor(c => c.City).SetValidator(new CityConfiguration());
            RuleFor(c => c.CityId).NotEmpty();
            RuleFor(c => c.Street).NotEmpty().Length(5, 55);
            RuleFor(c => c.Number).NotEmpty().GreaterThan(0);
        }
    }
}
