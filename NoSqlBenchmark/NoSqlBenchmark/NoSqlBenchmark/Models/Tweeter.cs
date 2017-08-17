using System;
using System.Collections.Generic;
using Orient.Client;

namespace NoSqlBenchmark.Models
{
    [Serializable]
    public class Metadata
    {
        public string result_type { get; set; }
        public int recent_retweets { get; set; }
    }

    [Serializable]
    public class Result
    {
        public string text { get; set; }
        public int to_user_id { get; set; }
        public string to_user { get; set; }
        public string from_user { get; set; }
        public int id { get; set; }
        public int from_user_id { get; set; }
        public string iso_language_code { get; set; }
        public string source { get; set; }
        public string profile_image_url { get; set; }
        public string created_at { get; set; }
        public Metadata metadata { get; set; }
    }

    [Serializable]
    public class TweeterModel : BaseModel
    {
        public IList<Result> results { get; set; }
        public int since_id { get; set; }
        public int max_id { get; set; }
        public string refresh_url { get; set; }
        public int results_per_page { get; set; }
        public string next_page { get; set; }
        public double completed_in { get; set; }
        public int page { get; set; }
        public string query { get; set; }

        public override ODocument GetDocument()
        {
            var doc = new ODocument();
            doc.OClassName = "TweeterModel";
            doc.SetField("since_id", since_id);
            doc.SetField("max_id", max_id);
            doc.SetField("refresh_url", refresh_url);
            doc.SetField("results_per_page", results_per_page);
            doc.SetField("next_page", next_page);
            doc.SetField("completed_in", completed_in);
            doc.SetField("page", page);
            doc.SetField("query", query);
            foreach (var r in results)
            {
                doc.SetField("results", new ODocument()
                    .SetField("text", r.text)
                    .SetField("to_user_id", r.to_user_id)
                    .SetField("to_user", r.to_user)
                    .SetField("from_user", r.from_user)
                    .SetField("id", r.id)
                    .SetField("from_user_id", r.from_user_id)
                    .SetField("iso_language_code", r.iso_language_code)
                    .SetField("source", r.source)
                    .SetField("profile_image_url", r.profile_image_url)
                    .SetField("created_at", r.created_at)
                    .SetField("metadata", new ODocument())
                        .SetField("result_type",r.metadata.result_type)
                        .SetField("recent_retweets",r.metadata.recent_retweets)
                );
            }
            return doc;
        }
    }


}
