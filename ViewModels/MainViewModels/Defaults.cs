using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel;

internal static class Defaults
{
    public static Product DefaultProduct { get; } = new()
    {
        Price = 0,
        MaximumDiscount = 0,
        CurrentDiscount = 0,
        StorageQuantity = 0,
        Type = "",
        MeasurmentUnit = "шт.",
        Manufacturer = "",
        Dealer = "",
        Category = "",
        Description = "",
        ImagePath = null
    };
}
