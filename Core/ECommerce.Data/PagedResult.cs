using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data
{
    public class PagedResult<TList>
    {
        public List<TList> List { get; set; }
        public long TotalCount { get; set; }

        public PagedResult(List<TList> list, long totalCount)
        {
            List = list;
            TotalCount = totalCount;
        }
    }
}
