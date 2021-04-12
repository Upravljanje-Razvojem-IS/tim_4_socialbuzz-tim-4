using FluentValidation;
using Logistics.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Infrastructure.Configurations
{
    public class DistanceServiceConfiguration : AbstractValidator<DistancePrice>
    {
        public DistanceServiceConfiguration()
        {
            RuleFor(c => c.MinimalDistance).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(c => c.MaximalDistance).NotEmpty().GreaterThanOrEqualTo(c => c.MinimalDistance);
            RuleFor(c => c.Price).NotEmpty().GreaterThan(0);
        }
    }
}
