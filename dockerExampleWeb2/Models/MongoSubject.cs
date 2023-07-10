using MongoDB.Bson;

namespace dockerExampleWeb2.Models
{
    public class MongoSubject
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }
    }
}
