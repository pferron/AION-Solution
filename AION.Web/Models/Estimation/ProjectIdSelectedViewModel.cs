﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Web.Models
{
    public class ProjectIdSelectedViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}