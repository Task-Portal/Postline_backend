using System;

namespace Shared.RequestFeatures
{
    public class PostParameters:RequestParameters
    {
        public string CategoryName { get; set; }
       
        public DateTime PostFrom { get; set; }
        public DateTime PostTo { get; set; }
        
        public bool ValidDateTimeRange => PostTo >= PostFrom;
        //
        public bool FilterAvailable =>
            CategoryName != null || PostFrom != default;

    }
}