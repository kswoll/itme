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
    }
}