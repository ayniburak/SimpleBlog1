﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog2.Infrastructure
{
    public class PagedData<T> : IEnumerable<T>
    {
        public PagedData(IEnumerable<T> currentItems, int totalCount, int page, int perPage)
        {
            _currentItems = currentItems;

            TotalCount = totalCount;
            Page = page;
            PerPage = perPage;

            TotalPages = (int)Math.Ceiling((float)TotalCount / PerPage);
            HasNextPage = Page < TotalPages;
            HasPreviousPage = Page > 1;
        }
        private readonly IEnumerable<T> _currentItems;
        public int TotalCount { get; private set; }
        public int Page { get; private set; }
        public int PerPage { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasNextPage { get; private set; }
        public bool HasPreviousPage { get; private set; }

        public int NextPage
        {
            get
            {
                if (!HasNextPage)
                {
                    throw new InvalidOperationException();
                }
                return Page + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (!HasPreviousPage)
                {
                    throw new InvalidOperationException();
                }
                return Page - 1;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}