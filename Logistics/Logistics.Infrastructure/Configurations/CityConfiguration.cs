using FluentValidation;
using Logistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Configurations
{
    public class CityConfiguration : AbstractValidator<City>
    {
        public CityConfiguration()
        {
            RuleFor(c => c.Name).NotEmpty().Length(1, 55);
            RuleFor(c => c.PostalCode).NotEmpty().Length(1, 10);
            RuleFor(c => c.Latitude).NotEmpty();
            RuleFor(c => c.Longitude).NotEmpty();
        }
    }
}
