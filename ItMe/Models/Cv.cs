﻿using System.Collections.Generic;

namespace ItMe.Models
{
    public class Cv
    {
        public int Id { get; set; }
        public string Blurb { get; set; }
        public List<Job> Jobs { get; set; }
        public List<ExternalProfile> Profiles { get; set; }
        public List<Language> Languages { get; set; }
        public List<Skill> Skills { get; set; }
    }
}