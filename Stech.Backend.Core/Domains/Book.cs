﻿using Stech.Backend.Core;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Stech.Backend.Core
{
    public class Book : IEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("author")]
        public virtual Author Author { get; set; }
        [JsonPropertyName("salesCount")]
        public int SalesCount { get; set; }        

        public void Sell()
        {
            this.SalesCount++;
        }
    }
}
