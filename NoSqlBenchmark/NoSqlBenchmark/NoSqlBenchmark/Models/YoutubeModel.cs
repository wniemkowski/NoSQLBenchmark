using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orient.Client;

namespace NoSqlBenchmark.Models
{
    [Serializable]
    public class Thumbnail
    {
        public string sqDefault { get; set; }
        public string hqDefault { get; set; }
    }

    [Serializable]
    public class Player
    {
        public string def { get; set; }
    }

    [Serializable]
    public class Content
    {
        public string name { get; set; }
    }

    [Serializable]
    public class Status
    {
        public string value { get; set; }
        public string reason { get; set; }
    }

    [Serializable]
    public class Restriction
    {
        public string type { get; set; }
        public string relationship { get; set; }
        public string countries { get; set; }
    }

    [Serializable]
    public class AccessControl
    {
        public string comment { get; set; }
        public string commentVote { get; set; }
        public string videoRespond { get; set; }
        public string rate { get; set; }
        public string embed { get; set; }
        public string list { get; set; }
        public string autoPlay { get; set; }
        public string syndicate { get; set; }
    }

    [Serializable]
    public class Data
    {
        public string id { get; set; }
        public DateTime uploaded { get; set; }
        public DateTime updated { get; set; }
        public string uploader { get; set; }
        public string category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public IList<string> tags { get; set; }
        public Thumbnail thumbnail { get; set; }
        public Player player { get; set; }
        public Content content { get; set; }
        public int duration { get; set; }
        public string aspectRatio { get; set; }
        public double rating { get; set; }
        public string likeCount { get; set; }
        public int ratingCount { get; set; }
        public int viewCount { get; set; }
        public int favoriteCount { get; set; }
        public int commentCount { get; set; }
        public Status status { get; set; }
        public IList<Restriction> restrictions { get; set; }
        public AccessControl accessControl { get; set; }
    }

    [Serializable]
    public class YoutubeModel : BaseModel
    {
        public string apiVersion { get; set; }
        public Data data { get; set; }

        public override ODocument GetDocument()
        {
            throw new NotImplementedException();
        }
    }

}
