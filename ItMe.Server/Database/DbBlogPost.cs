﻿using System;

namespace ItMe.Server.Database
{
    public class DbBlogPost : DbPersonEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Excerpt { get; set; }
        public string Slug { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}