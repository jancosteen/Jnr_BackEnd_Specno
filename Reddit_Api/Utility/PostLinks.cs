using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;


namespace Reddit_Api.Utility
{
    public class PostLinks //: IPostLinks
    {
        /*private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<PostDto> _dataShaper;

        public PostLinks(LinkGenerator linkGenerator, IDataShaper<PostDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<PostDto> postsDto, string fields, string userId, HttpContext httpContext)
        {
            var shapedPosts = ShapeData(postsDto, fields);

            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedPosts(postsDto, fields, userId, httpContext, shapedPosts);

            return ReturnShapedPosts(shapedPosts);
        }

        private LinkResponse ReturnLinkedPosts(IEnumerable<PostDto> postsDto,
            string fields, string userId, HttpContext httpContext, List<Entity> shapedPosts)
        {
            var postsDtoList = postsDto.ToList();

            for (var index = 0; index < postsDtoList.Count(); index++)
            {
                var postLinks = CreateLinksForPost(httpContext, userId, postsDtoList[index].Id, fields);
                shapedPosts[index].Add("Links", postLinks);
            }

            var postCollection = new LinkCollectionWrapper<Entity>(shapedPosts);
            var linkedPosts = CreateLinksForPosts(httpContext, postCollection);

            return new LinkResponse { HasLinks = true, LinkedEntities = linkedPosts };
        }

        private LinkCollectionWrapper<Entity> CreateLinksForPosts(HttpContext httpContext, LinkCollectionWrapper<Entity> postsWrapper)
        {
            postsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetPostsForUser", values: new { }), "self", "GET"));
            return postsWrapper;
        }

        private List<Link> CreateLinksForPost(HttpContext httpContext, string userId, Guid id, string fields = "")
        {
            var links = new List<Link>
            {
                new Link
                (
                    _linkGenerator.GetUriByAction(httpContext, "GetPostForUser", values: new{userId, id, fields}),
                    "self",
                    "GET"
                ),
                new Link
                (
                    _linkGenerator.GetUriByAction(httpContext, "DeletePostForUser", values: new{userId, id, fields}),
                    "delete_post",
                    "DELETE"
                ),
                new Link
                (
                    _linkGenerator.GetUriByAction(httpContext, "UpdatePostForUser", values: new{userId, id, fields}),
                    "update_post",
                    "PUT"
                ),
                new Link
                (
                    _linkGenerator.GetUriByAction(httpContext, "PartiallyUpdatePostForUser", values: new{userId, id, fields}),
                    "partially_update_post",
                    "PATCH"
                )
            };

            return links;
        }



        private LinkResponse ReturnShapedPosts(List<Entity> shapedPosts) =>
            new LinkResponse { ShapedEntities = shapedPosts };

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<PostDto> postsDto, string fields) =>
            _dataShaper.ShapeData(postsDto, fields)
            .Select(e => e.Entity)
            .ToList();*/
    }
}

