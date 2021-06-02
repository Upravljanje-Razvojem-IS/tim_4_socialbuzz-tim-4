using System;
using System.Collections.Generic;

namespace Logistics.Core.Mock
{
    public static class ItemMockService
    {
        public static IList<ItemMock> ItemMocks = new List<ItemMock>
        {
            new ItemMock
            {
                Id = Guid.Parse("398783eb-ac1e-46bf-b458-7be1c435a957"),
                Item = "item1",
                PriceOfOne = 1000,
                WeightOfOne = 2
            },
            new ItemMock
            {
                Id = Guid.Parse("62c8e655-65b2-4e74-b7d2-894c25f5ccaf"),
                Item = "item2",
                PriceOfOne = 4500,
                WeightOfOne = 14
            },
            new ItemMock
            {
                Id = Guid.Parse("ba5ed8df-c07e-4b55-a0dd-ceae2887911b"),
                Item = "item3",
                PriceOfOne = 14000,
                WeightOfOne = 22
            },
            new ItemMock
            {
                Id = Guid.Parse("919d1c7f-2688-4687-8a66-7c0430c95f94"),
                Item = "item4",
                PriceOfOne = 4000,
                WeightOfOne = 0.5F
            }
        };

    }
}
