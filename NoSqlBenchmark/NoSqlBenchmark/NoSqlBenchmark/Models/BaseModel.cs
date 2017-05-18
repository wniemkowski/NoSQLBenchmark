using System;
using Amazon.DynamoDBv2.DataModel;

namespace NoSqlBenchmark
{
    [Serializable]
    public class BaseModel
    {
        [DynamoDBHashKey]
        public long Id { get; set; }
    }
}