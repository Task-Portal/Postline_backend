using Entities.Exceptions.Abstract;

namespace Entities.Exceptions.BadRequestExceptions
{
    public class MaxDateRangeBadRequestException :BadRequestException
    {
        public MaxDateRangeBadRequestException(string message) : base(message)
        {
        }
        
         public MaxDateRangeBadRequestException() : base("" +
                                                         "Max Post date can't be less than Min post date")
        {
        }
        
        
        
    }
}