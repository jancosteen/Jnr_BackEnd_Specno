﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        ICompanyService CompanyService { get; }
        IEmployeeService EmployeeService { get; }
        IPostService PostService { get; }
        ICommentService CommentService { get; }
        IUserPostVoteService UserPostVoteService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
