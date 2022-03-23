﻿using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Reddit_Api
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember("FullAddress",
                    opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<CompanyForUpdateDto, Company>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>();

            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<PostForCreationDto, Post>();
            CreateMap<PostForUpdateDto, Post>().ReverseMap();
            


            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<CommentForCreationDto, Comment>();
            CreateMap<CommentForUpdateDto, Comment>().ReverseMap();

            CreateMap<UserCommentVote, UserCommentVoteDto>();
            CreateMap<UserPostVote, UserPostVoteDto>();

            CreateMap<User, UserDto>().ReverseMap();



        }
    }
}
