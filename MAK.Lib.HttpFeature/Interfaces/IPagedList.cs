﻿namespace Interfaces;

public interface IPagedList<T> where T : class
{
    //Task<PagedList<T>> GetPagedList(PagingEntity entityParameter);
    Task<DataResult<PagedList<T>>> GetPagedList(PagingEntity entityParameter);
}
