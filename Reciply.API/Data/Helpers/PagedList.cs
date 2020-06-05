using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Reciply.API.Data
{
    public class PagedList<T> : List<T>
    {
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            ItemsCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items); // add recipes to PagedList
        }

        public int CurrentPage { get; set; }
        public int ItemsCount { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync(); // count all items
            var items = await source
                .Skip((pageNumber - 1) * pageSize) // skip first x elements (2 page and 5 users -> (2-1) * 5 = 5 (skip))
                .Take(pageSize) // take next 5 elements
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}