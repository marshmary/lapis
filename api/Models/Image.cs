using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lapis.API.Models
{
    public class Image
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string Thumbnail { get; set; } = "";
        [BsonRequired]
        public string Medium { get; set; } = "";
        [BsonRequired]
        public string Hight { get; set; } = "";
        [BsonRequired]
        public HashSet<string> Tags { get; set; }
        [BsonRequired]
        public string Orientation { get; set; } = "";
        [BsonRequired]
        public Size Size { get; set; }
        [BsonRequired]
        public Colors Colors { get; set; }
        [BsonRequired]
        public Credit Credit { get; set; }
        [BsonRequired]
        public string CreatedBy { get; set; } = "";
        [BsonRequired]
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? LastUpdated { get; set; }
        [BsonRequired]
        public bool IsDeleted { get; set; } = false;
        public DateTime? Deleted { get; set; }
    }

    public class Size
    {
        public int Height { get; set; } = 0;
        public int Width { get; set; } = 0;
    }

    public class Colors
    {
        public string Primary { get; set; } = "";
        public string Secondary { get; set; } = "";
        public string Tertiary { get; set; } = "";
    }

    public class Credit
    {
        public string Author { get; set; } = "";
        public string SourceUrl { get; set; } = "";
    }
}