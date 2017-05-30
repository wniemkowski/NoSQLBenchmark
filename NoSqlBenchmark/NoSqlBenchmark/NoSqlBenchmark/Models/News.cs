using System;
using Amazon.DynamoDBv2.DataModel;

namespace NoSqlBenchmark.Models
{
    [Serializable]
    [DynamoDBTable("News")]
    public class News : BaseModel
    {
        private static int _id = 0;
        [DynamoDBProperty]
        public string Message { get; set; }
        [DynamoDBProperty]
        public DateTime Date { get; set; }
        [DynamoDBProperty]
        public string CreatedBy { get; set; }
        [DynamoDBProperty]
        public int Likes { get; set; }
        [DynamoDBProperty]
        public OP Op { get; set; }
        [DynamoDBProperty("Ids")]
        public int[] Ids { get; set; }

        public static News GetDemo()
        {
            return new News
            {
                CreatedBy = "John",
                Date = DateTime.Now,
                Id = _id++,
                Likes = int.MaxValue,
                Message = "Hello",
                Ids = new[] { 1, 2, 3 },
                Op = new OP
                {
                    Name = "a",
                    Age = 5
                }
            };
        }
    }

    [Serializable]
    [DynamoDBTable("News")]
    public class OP
    {
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public int Age { get; set; }
    }
}
