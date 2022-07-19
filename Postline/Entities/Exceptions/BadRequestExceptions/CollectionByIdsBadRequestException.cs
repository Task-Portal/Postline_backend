using Entities.Exceptions.Abstract;

namespace Entities.Exceptions.BadRequestExceptions
{
    public sealed class CollectionByIdsBadRequestException : BadRequestException
    {
        public CollectionByIdsBadRequestException()
            : base("Collection count mismatch comparing to ids.")
        {
        }
    }
}