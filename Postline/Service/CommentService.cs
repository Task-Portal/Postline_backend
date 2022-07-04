using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using LoggerService;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CommentService : ICommentService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;


        public CommentService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CommentDto>> GetCommentsAsync(Guid postId, bool trackChanges)
        {
            await CheckIfPostExists(postId, trackChanges);

            var commentsFromDb = await _repository.Comment.GetCommentsAsync(postId, trackChanges);
            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(commentsFromDb);

            return commentsDto;
        }

        public async Task<CommentDto> GetCommentAsync(Guid postId, Guid id, bool trackChanges)
        {
            await CheckIfPostExists(postId, trackChanges);

            var commentDb = await GetCommentForPostAndCheckIfItExists(postId, id, trackChanges);

            var comment = _mapper.Map<CommentDto>(commentDb);
            return comment;
        }

        // public async Task<CommentDto> CreateCommentForPostAsync(Guid postId,
        //     CommentForCreationDto commentForCreation, bool trackChanges)
        // {
        //     await CheckIfPostExists(postId, trackChanges);
        //
        //     var commentEntity = _mapper.Map<Comment>(commentForCreation);
        //
        //     _repository.Comment.CreateCommentForPost(postId, commentEntity);
        //     await _repository.SaveAsync();
        //
        //     var commentToReturn = _mapper.Map<CommentDto>(commentEntity);
        //
        //     return commentToReturn;
        // }

        // public async Task DeleteCommentForPostAsync(Guid postId, Guid id, bool trackChanges)
        // {
        //     await CheckIfPostExists(postId, trackChanges);
        //
        //     var commentDb = await GetCommentForPostAndCheckIfItExists(postId, id, trackChanges);
        //
        //     _repository.Comment.DeleteComment(commentDb);
        //     await _repository.SaveAsync();
        // }

        // public async Task UpdateCommentForPostAsync(Guid postId, Guid id,
        //     CommentForUpdateDto commentForUpdate,
        //     bool compTrackChanges, bool empTrackChanges)
        // {
        //     await CheckIfPostExists(postId, compTrackChanges);
        //
        //     var commentDb = await GetCommentForPostAndCheckIfItExists(postId, id, empTrackChanges);
        //
        //     _mapper.Map(commentForUpdate, commentDb);
        //     await _repository.SaveAsync();
        // }

        // public async Task<(CommentForUpdateDto commentToPatch, Comment commentEntity)> GetCommentForPatchAsync
        //     (Guid postId, Guid id, bool compTrackChanges, bool empTrackChanges)
        // {
        //     await CheckIfPostExists(postId, compTrackChanges);
        //
        //     var commentDb = await GetCommentForPostAndCheckIfItExists(postId, id, empTrackChanges);
        //
        //     var commentToPatch = _mapper.Map<CommentForUpdateDto>(commentDb);
        //
        //     return (commentToPatch: commentToPatch, commentEntity: commentDb);
        // }
        //
        // public async Task SaveChangesForPatchAsync(CommentForUpdateDto commentToPatch, Comment commentEntity)
        // {
        //     _mapper.Map(commentToPatch, commentEntity);
        //     await _repository.SaveAsync();
        // }

        private async Task CheckIfPostExists(Guid postId, bool trackChanges)
        {
            var post = await _repository.Post.GetPostAsync(postId, trackChanges);
            if (post is null)
                throw new PostNotFoundException(postId);
        }

        private async Task<Comment> GetCommentForPostAndCheckIfItExists
            (Guid postId, Guid id, bool trackChanges)
        {
            var commentDb = await _repository.Comment.GetCommentAsync(postId, id, trackChanges);
            if (commentDb is null)
                throw new CommentNotFoundException(id);

            return commentDb;
        }
    }
}