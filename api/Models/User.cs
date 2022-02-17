using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Lapis.API.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string Email { get; set; } = "";
        [BsonRequired]
        public string Password { get; set; } = "";
        [BsonRequired]
        public string Name { get; set; } = "";
        [BsonRequired]
        public string Avatar { get; set; } = "https://i.ibb.co/kxpVwFp/Default-Avatar.jpg";
        [BsonRequired]
        public string Wallpaper { get; set; } = "https://i.ibb.co/fHFShGb/Default-Wallpaper.png";
        [BsonRequired]
        public bool Verify { get; set; } = false;
        [BsonRequired]
        public string[] Roles { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}