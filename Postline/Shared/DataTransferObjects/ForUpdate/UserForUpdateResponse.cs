using System.Collections.Generic;

namespace Shared.DataTransferObjects.ForUpdate
{
    public class UserForUpdateResponse
    {
        public bool IsSuccessfulUpdate { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}