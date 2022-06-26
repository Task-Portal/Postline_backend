using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ICommentRepository
    {
    
        
        Task<IEnumerable<Comment>> GetCommentsAsync(Guid postId, bool trackChanges);
        Task<Comment> GetCommentAsync(Guid postId, Guid id, bool trackChanges);
        // void CreateCommentForCompany(Guid postId, Comment employee);
        // void DeleteComment(Comment employee);
        //
        
        
        
        // Task<PagedList<Comment>> CommentsAsync(Guid postId,
        //     CommentParameters commentParameters, bool trackChanges);
        // Task<Comment> CommentAsync(Guid postId, Guid id, bool trackChanges);
        // void CommentForPost(Guid postId, Comment comment);
        // void Comment(Comment comment);
    }
}