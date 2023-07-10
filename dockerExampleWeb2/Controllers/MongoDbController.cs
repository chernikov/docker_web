using dockerExampleWeb2.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace dockerExampleWeb2.Controllers
{
    
    public class MongoDbController : Controller
    {
        private readonly SchoolMongoContext dbContext;

        public MongoDbController(SchoolMongoContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("mongo")]
        [HttpGet]
        public IActionResult Get()
        {
            var list = dbContext.AllStudents;
            return Ok(list);
        }
        
        [Route("mongo/{studentId}")]
        [HttpGet]
        public IActionResult Get([FromQuery] string studentId)
        {
            var objectId = new ObjectId(studentId);
            var filter = Builders<MongoStudent>.Filter.Eq(u => u.Id, objectId);
            var students = dbContext.Students.Find(filter).FirstOrDefault();
            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }

        [Route("mongo")]
        [HttpPost]
        public IActionResult Post([FromBody] MongoStudent student)
        {
            dbContext.Students.InsertOne(student);
            return Created("/mongo/" + student.Id, student);
        }


        [Route("mongo")]
        [HttpPut]
        public IActionResult Put([FromBody] MongoStudent student)
        {
            var filter = Builders<MongoStudent>.Filter.Eq(u => u.Age, 18);
            var update = Builders<MongoStudent>.Update
                    .Set(u => u.FirstName, student.FirstName)
                    .Set(u => u.LastName, student.LastName)
                    .Set(u => u.Age, student.Age)
                    .Set(u => u.Points, student.Points);
            var existStudent = dbContext.Students.Find(filter).FirstOrDefault();
            dbContext.Students.UpdateOne(filter, update);
            return Ok(student);
        }

    }   
}
