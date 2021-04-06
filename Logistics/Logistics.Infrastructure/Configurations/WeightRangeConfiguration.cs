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
    public class WeightRangeConfiguration : AbstractValidator<WeightRange>
    {
        public WeightRangeConfiguration()
        {
            RuleFor(c => c.MinimalWeight).GreaterThan(0).NotEmpty();
            RuleFor(c => c.MaximalWeight).GreaterThan(c => c.MinimalWeight).NotEmpty();
            RuleFor(c => c.PriceCoefficient).NotEmpty();
        }
    }
}
