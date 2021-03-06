﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sdk.Models
{
    public class EegData
    {
        /// <summary>
        /// DATA
        /// </summary>

        [JsonProperty("Configuration")]
        public Configuration Configuration = new Configuration();

        //Full Data
        [JsonProperty("Raw")]
        public Raw Raw = new Raw();

        [JsonProperty("Emotions")]
        public Affectiv Emotions = new Affectiv();

        [JsonProperty("Expressions")]
        public Expressions Expressions = new Expressions();

        [JsonProperty("Markers")]
        public List<Mark> Markers = new List<Mark>();

        public EegData()
        {
        }
    }
}