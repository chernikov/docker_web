using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dockerExampleWeb2.Models
{
    public class MongoStudent
    {
        public ObjectId Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int? Age { get; set; }

        public IList<MongoPoint> Points { get; set; }


    }
}
