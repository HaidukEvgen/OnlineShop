﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineShop.Services.Catalog.Domain.Models.Data
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Dictionary<string, string> OptionalFields { get; set; }
    }
}
