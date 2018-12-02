using System;
using System.Linq;
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
            FavIconS3Key = person.FavIconS3Key
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

        public static Expression<Func<DbCv, Cv>> MapCv { get; } = cv => new Cv
        {
            Id = cv.Id,
            Blurb = cv.Blurb,
            Jobs = cv.Jobs
                .Select(x => new Job
                {
                    Id = x.Id,
                    Company = x.Company,
                    Roles = x.Roles
                        .Select(y => new JobRole
                        {
                            Id = y.Id,
                            Title = y.Title,
                            Description = y.Description,
                            Start = y.Start,
                            End = y.End
                        })
                        .ToList()
                })
                .ToList(),
            Profiles = cv.Profiles
                .Select(x => new ExternalProfile
                {
                    Id = x.Id,
                    Name = x.Name,
                    Uri = x.Uri
                })
                .ToList(),
            Languages = cv.Languages
                .Select(x => new Language
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList()
        };
    }
}