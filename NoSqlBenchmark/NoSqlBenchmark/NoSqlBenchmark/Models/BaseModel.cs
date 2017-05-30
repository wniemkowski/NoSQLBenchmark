using System;
using Amazon.DynamoDBv2.DataModel;

namespace NoSqlBenchmark.Models
{
    [Serializable]
    public class BaseModel
    {
        [DynamoDBHashKey]
        public long Id { get; set; }
    }
}