﻿namespace Shared.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int MaxPageSize = 200; 
        public int PageNumber { get; set; } = 1; 
 
        private int _pageSize = 5; 
        public int PageSize 
        { 
            get 
            { 
                return _pageSize; 
            } 
            set 
            { 
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value; 
            } 
        } 
        
        public string OrderBy { get; set; } 
    }
}