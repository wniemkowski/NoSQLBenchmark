using System;
using Amazon.DynamoDBv2.DataModel;
using Orient.Client;

namespace NoSqlBenchmark.Models
{
    [Serializable]
    public class RedditModel : BaseModel
    {
        private static int _id = 0;
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public int Upvotes { get; set; }
        public OP Op { get; set; }
        public int[] Ids { get; set; }

        public static RedditModel GetDemo()
        {
            return new RedditModel
            {
                CreatedBy = "John",
                Date = DateTime.Now,
                Id = _id++,
                Upvotes = int.MaxValue,
                Message = "Hello",
                Ids = new[] { 1, 2, 3 },
                Op = new OP
                {
                    Name = "a",
                    Age = 5
                }
            };
        }

        public override ODocument GetDocument()
        {
            ODocument doc = new ODocument();
            doc.OClassName = "RedditModel";

            doc.SetField("_id", _id);
            doc.SetField("Message", Message);
            doc.SetField("Date", Date);
            doc.SetField("CreatedBy", CreatedBy);
            doc.SetField("Upvotes", Upvotes);
            doc.SetField("OP", new ODocument()
                .SetField("Name", Op.Name)
                .SetField("Age", Op.Age));
            doc.SetField("Ids", Ids);

            return doc;
        }
    }

    [Serializable]
    [DynamoDBTable("RedditModel")]
    public class OP
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
