using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class UserPostVoteService: IUserPostVoteService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        private readonly UserManager<User> _userManager;

        public UserPostVoteService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
             UserManager<User> userManager)// IPostLinks postLinks,
        {
            _repository = repository;
            _logger = logger; ;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserPostVoteDto>> GetUserPostVoteAsync(string userId, bool trackChanges)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new UserNotFoundException(userId);

            var upvEntities = await _repository.UserPostVote.GetUserVotedPosts(userId, trackChanges);


            var upvDto = _mapper.Map<IEnumerable<UserPostVoteDto>>(upvEntities);

            return upvDto;
        }
    }
}
