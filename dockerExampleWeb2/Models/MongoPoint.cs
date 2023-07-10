using MongoDB.Bson;

namespace dockerExampleWeb2.Models
{
    public class MongoPoint
    {
        public ObjectId Id { get; set; }

        public MongoSubject Subject { get; set; }

        public int Point { get; set; }

    }
}
