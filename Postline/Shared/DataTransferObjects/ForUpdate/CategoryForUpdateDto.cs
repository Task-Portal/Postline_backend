using System;

namespace Shared.DataTransferObjects.ForUpdate
{
    public class CategoryForUpdateDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
    }
}