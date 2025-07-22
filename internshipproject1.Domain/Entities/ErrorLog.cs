using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace internshipproject1.Domain.Entities
{
    public class ErrorLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;
        public string? StackTrace { get; set; }
        public string RequestPath { get; set; } = null!; // endpoint
        public string QueryString { get; set; } = null!; // query parameters
        public string RequestMethod { get; set; } = null!; // method 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
   
    
    }
}
