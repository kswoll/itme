using System;
using System.Linq.Expressions;
using ItMe.Shared.Models;

namespace ItMe.Server.Database
{
    public static class Mappers
    {
        public static Expression<Func<DbPerson, PersonModel>> MapPerson { get; } = person => new PersonModel
        {
            Id = person.Id,
            Name = person.Name,
        };

        public static Expression<Func<DbBlogPost, BlogPostModel>> MapBlogPost { get; } = post => new BlogPostModel
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            Excerpt = post.Excerpt,
            Slug = post.Slug,
            Created = post.Created,
            LastUpdated = post.LastUpdated
        };
    }
}