using System.Collections.Generic;
using Shared.RequestFeatures;

namespace Shared.DataTransferObjects.ForShow
{
    public class PostDtoWithPagination
    {
        public IEnumerable<PostDto> Posts { get; set; }

        public MetaData Data { get; set; }

       
    }
}