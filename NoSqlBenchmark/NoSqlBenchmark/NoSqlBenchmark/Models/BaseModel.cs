using System;
using Amazon.DynamoDBv2.DataModel;
using Orient.Client;

namespace NoSqlBenchmark.Models
{
    [Serializable]
    public class BaseModel : OBaseRecord
    {
        [DynamoDBHashKey]
        public long Id { get; set; }

        public virtual ODocument GetDocument()
        {
            return new ODocument();
        }
    }
}