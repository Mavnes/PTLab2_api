﻿using Microsoft.EntityFrameworkCore;
using PTLab2_api.Data.Database;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Repositories.Interfaces;

namespace PTLab2_api.Data.Repositories.Implimentations
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(ShopDbContext context) : base(context) { }

        public Sale? GetBaseSale()
        {
            var sales = _context.Sales.AsEnumerable();

            if (!sales.Any())
                return null;
            
            return sales.OrderBy(s => s.MinTotalExpenses)
                        .First();
        }
    }
}
