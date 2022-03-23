﻿using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IPostService> _postService;
        private readonly Lazy<ICommentService> _commentService;
        private readonly Lazy<IUserPostVoteService> _userPostVoteService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, 
            IMapper mapper, IEmployeeLinks employeeLinks, UserManager<User> userManager,
            IOptionsMonitor<JwtConfiguration> configuration
            )
        {
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repositoryManager, logger, mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, logger, mapper, employeeLinks));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
            _postService = new Lazy<IPostService>(() => new PostService(repositoryManager, logger, mapper, userManager));
            _commentService = new Lazy<ICommentService>(() => new CommentService(repositoryManager, logger, mapper, userManager));
            _userPostVoteService = new Lazy<IUserPostVoteService>(() => new UserPostVoteService(repositoryManager, logger, mapper, userManager));
        }
        public ICompanyService CompanyService => _companyService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;
        public IPostService PostService => _postService.Value;
        public ICommentService CommentService => _commentService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IUserPostVoteService UserPostVoteService => _userPostVoteService.Value;


    }
}
