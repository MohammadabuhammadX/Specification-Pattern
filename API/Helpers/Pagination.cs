﻿namespace API.Helpers
{
    public class Pagination<T> where T : class 
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public Pagination(int pageIndex, int pageSize, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
        }
    }
}

