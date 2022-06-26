using System;

namespace Entities.Exceptions
{
    public sealed class CommentNotFoundException : NotFoundException
    {
        public CommentNotFoundException(Guid categoryId)
            : base($"The category with id: {categoryId} doesn't exist in the database.")
        {
        }
    }
}