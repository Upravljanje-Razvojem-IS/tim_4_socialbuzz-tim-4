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
    public class DeliveryServiceConfiguration : AbstractValidator<DeliveryService>
    {
        public DeliveryServiceConfiguration()
        {
            RuleFor(c => c.Name).NotEmpty().Length(1, 55);
        }
    }
}
