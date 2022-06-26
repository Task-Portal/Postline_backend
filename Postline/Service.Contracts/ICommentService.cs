using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetCommentsAsync(Guid postId, bool trackChanges);
        Task<CommentDto> GetCommentAsync(Guid postId, Guid id, bool trackChanges);
        // Task<CommentDto> CreateCommentForPostAsync(Guid postId,
        //     CommentForCreationDto commentForCreation, bool trackChanges);
        
        // Task DeleteCommentForPostAsync(Guid postId, Guid id, bool trackChanges);
        // Task UpdateCommentForPostAsync(Guid postId, Guid id,
        //     CommentForUpdateDto commentForUpdate, bool compTrackChanges, bool empTrackChanges);
        // Task<(CommentForUpdateDto commentToPatch, Comment commentEntity)> GetCommentForPatchAsync(
        //     Guid postId, Guid id, bool compTrackChanges, bool empTrackChanges);
        // Task SaveChangesForPatchAsync(CommentForUpdateDto commentToPatch, Comment commentEntity);
    }
}