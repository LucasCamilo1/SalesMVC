using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resoult = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                resoult = resoult.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                resoult = resoult.Where(x => x.Date <= maxDate.Value);
            }
            return await resoult
                .Include(x => x.Seller)
                .Include(X => X.Seller.Departament)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Departament, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resoult = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                resoult = resoult.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                resoult = resoult.Where(x => x.Date <= maxDate.Value);
            }
            return await resoult
                .Include(x => x.Seller)
                .Include(X => X.Seller.Departament)
                .OrderByDescending(x => x.Date)
                .GroupBy(x =>x.Seller.Departament)
                .ToListAsync();
        }
    }
}
