﻿using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.AbreuBarber.Application.Common
{
    public abstract class PaginatedListInput
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public string Search { get; set; }
        public string Sort { get; set; }   
        public SearchOrder Dir { get; set; }
        public PaginatedListInput(
            int page,
            int perPage, 
            string search,
            string sort,
            SearchOrder dir)
        {
            Page = page;
            PerPage = perPage;
            Search = search;
            Sort = sort;
            Dir = dir;
        }
    }
}
