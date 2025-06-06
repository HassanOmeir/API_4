﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DataTransferObjects
{
	public class PaginatedResult<T>
	{
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
