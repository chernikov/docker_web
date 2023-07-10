using MongoDB.Driver;

namespace dockerExampleWeb2.Models
{
    public class SchoolMongoContext : MongoClient
    {
        public SchoolMongoContext(string connectionString) : base(connectionString)
        {
        }

        public IMongoCollection<MongoStudent> Students => GetDatabase("school").GetCollection<MongoStudent>("students");

        public IList<MongoStudent> AllStudents
        {
            get
            {
                var filter = Builders<MongoStudent>.Filter.Empty;
                return Students.Find(filter).ToListAsync().Result;
            }
        }
    }
}
