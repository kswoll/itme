using System;
using System.Linq.Expressions;
using ItMe.Models;

namespace ItMe.Database
{
    public static class Mappers
    {
        public static Expression<Func<DbPerson, Person>> MapPerson { get; } = person => new Person
        {
            Id = person.Id,
            Name = person.Name,
        };

        public static Expression<Func<DbBlogPost, BlogPost>> MapBlogPost { get; } = post => new BlogPost
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