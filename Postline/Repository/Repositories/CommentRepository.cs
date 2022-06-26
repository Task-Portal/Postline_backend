using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CommentRepository:RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Comment>> GetCommentsAsync(Guid postId, bool trackChanges) =>
            await FindByCondition(e => e.PostId.Equals(postId), trackChanges)
                .OrderBy(e => e.CommentedDate)
                .ToListAsync();

        public async Task<Comment> GetCommentAsync(Guid postId, Guid id, bool trackChanges) =>
            await FindByCondition(e => e.PostId.Equals(postId) && e.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateCommentForCompany(Guid postId, Comment comment)
        {
            comment.PostId = postId;
            Create(comment);
        }

        public void DeleteComment(Comment comment) => Delete(comment);
    }
}