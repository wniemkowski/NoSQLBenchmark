using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlBenchmark.Models
{
    [Serializable]
    public class News
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public long Id { get; set; }
        public int Likes { get; set; }
        public OP Op { get; set; }
        public int[] Ids { get; set; }

        public static News GetDemo()
        {
            return new News
            {
                CreatedBy = "John",
                Date = DateTime.Now,
                Id = long.MaxValue,
                Likes = int.MaxValue,
                Message = "Hello",
                Ids = new int[] {1,2,3},
                Op = new OP
                {
                    Name = "a",
                    Age = 5
                }
            };
        }
    }
    [Serializable]
    public class OP
    {
        public string Name{ get; set; }
        public int Age { get; set; }
    }
}
