﻿namespace Service.Contracts
{
    public interface IServiceManager
    {
        IPostService PostService { get; }
        ICommentService CommentService { get; }
        ICategoryService CategoryService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}