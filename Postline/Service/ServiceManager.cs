using System;
using AutoMapper;
using Contracts;
using EmailService;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NETCore.MailKit.Core;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IPostService> _postService;
        private readonly Lazy<ICommentService> _commentService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IUserService> _userService;

        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper, 
            UserManager<User> userManager,
            IConfiguration configuration, IEmailSender emailService)
        {
            _postService = new Lazy<IPostService>(() => new PostService(logger, repositoryManager, mapper,userManager));
            _commentService = new Lazy<ICommentService>(() => new CommentService(logger, repositoryManager,mapper));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(logger, repositoryManager, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                new AuthenticationService(logger, mapper, userManager, configuration, emailService));
            _userService = new Lazy<IUserService>(() => new UserService( mapper, userManager, configuration));
        }

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IPostService PostService => _postService.Value;
        public ICommentService CommentService => _commentService.Value;
        public ICategoryService CategoryService => _categoryService.Value;
        public IUserService UserService => _userService.Value;
    }
}