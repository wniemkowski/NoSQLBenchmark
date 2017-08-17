using System;
using System.Collections.Generic;
using System.Linq;
using Faker;
using FizzWare.NBuilder;
using MongoDB.Driver;
using ServiceStack.Text;

namespace NoSqlBenchmark.Models
{
    public class ModelFactory
    {
        private static IList<RedditModel> _news;
        private static IList<TweeterModel> _tweeter;
        private static IList<YoutubeModel> _youtube;
        public static int _iterator;
        public static int Max { get; set; }

        public BaseModel GetDemoModel<T>() where T:BaseModel
        {
            if (typeof(T) == typeof(RedditModel))
            {
                return RedditModel.GetDemo();
            }
            return new BaseModel();
        }

        public BaseModel GetDemoModel(ModelDataType dataType)
        {
            switch (dataType)
            {
                case ModelDataType.Reddit:
                    return _news[--_iterator];
                case ModelDataType.Tweeter:
                    return _tweeter[--_iterator];
                case ModelDataType.Youtube:
                    return _youtube[--_iterator];
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
            }
        }

        public ModelDataType GetModelDataType<T>()
        {
            if(typeof(T) == typeof(RedditModel)) return ModelDataType.Reddit;
            if (typeof(T) == typeof(TweeterModel)) return ModelDataType.Tweeter;
            if (typeof(T) == typeof(YoutubeModel)) return ModelDataType.Youtube;
            throw new ArgumentException();
        }

        public static void Populate(ModelDataType type, int count = 10000)
        {
            Max = _iterator = count;
            var rand = new Random();
            switch (type)
            {
                case ModelDataType.Reddit:
                    if (_news != null && count >= _news.Count) return;
                    var rlist = Builder<RedditModel>.CreateListOfSize(count).All()
                        .With(n => n.Message = Lorem.Sentence(10))
                        .With(n => n.CreatedBy= Name.FullName(NameFormats.WithPrefix))
                        .With(n => n.Ids = Builder<int>.CreateListOfSize(10).All()
                            .With(nr => RandomNumber.Next(1,100))
                            .Build().ToArray())
                        .With(n => n.Op = Builder<OP>.CreateNew()
                            .With(op => op.Age = Faker.RandomNumber.Next(1,100))
                            .With(op => op.Name = Faker.Name.FullName(NameFormats.WithSuffix))
                            .Build());
                    _news = rlist.Build();
                    break;
                case ModelDataType.Tweeter:
                    if (_tweeter != null && count >= _tweeter.Count) return;
                    var tlist = Builder<TweeterModel>.CreateListOfSize(count).All()
                        .With(n => n.completed_in = rand.NextDouble())
                        .With(n => n.max_id = rand.Next(1000))
                        .With(n => n.next_page = Faker.Internet.DomainName())
                        .With(n => n.page = rand.Next(1000))
                        .With(n => n.query = Faker.Lorem.GetFirstWord())
                        .With(n => n.refresh_url = Faker.Internet.DomainName())
                        .With(n => n.results = Builder<Result>.CreateListOfSize(100).All()
                            .With(r => r.created_at = DateTime.Now.Subtract(TimeSpan.FromDays(rand.Next(10000)))
                                .ToString("d"))
                            .With(r => r.from_user = Faker.Name.FullName())
                            .With(r => r.from_user_id = rand.Next())
                            .With(r => r.id = rand.Next())
                            .With(r => r.iso_language_code = Faker.Address.ZipCode())
                            .With(r => r.metadata = Builder<Metadata>.CreateNew()
                                .With(m => m.recent_retweets = rand.Next())
                                .With(m => m.result_type = Faker.Lorem.GetFirstWord()).Build())
                            .With(r => r.profile_image_url = Faker.Internet.DomainName())
                            .With(r => r.source = Faker.Internet.UserName())
                            .With(r => r.text = Faker.Lorem.Sentence(10))
                            .With(r => r.to_user = Faker.Internet.UserName()).Build())
                        .With(n => n.results_per_page = rand.Next(100))
                        .With(n => n.since_id = rand.Next());

                    _tweeter = tlist.Build();
                    break;
                case ModelDataType.Youtube:
                    if (_youtube != null && count >= _youtube.Count) return;
                    var ylist = Builder<YoutubeModel>.CreateListOfSize(count).All()
                        .With(y => y.apiVersion = rand.NextDouble().ToString())
                        .With(y => y.data = Builder<Data>.CreateNew()
                            .With(d => d.accessControl = Builder<AccessControl>.CreateNew()
                                .With(ac => ac.autoPlay = RandomNumber.Next(1).ToString())
                                .With(ac => ac.comment = Lorem.Sentence(10))
                                .With(ac => ac.commentVote = Lorem.Sentence(5))
                                .With(ac => ac.embed = rand.Next(1).ToString())
                                .With(ac => ac.list = Lorem.Sentence(10))
                                .With(ac=> ac.rate = rand.Next(1).ToString())
                                .With(ac => ac.syndicate = Lorem.GetFirstWord())
                                .With(ac => ac.videoRespond = Lorem.GetFirstWord()).Build())
                            .With(d => d.aspectRatio = rand.Next(10).ToString() + ":" + rand.Next(10))
                            .With(d=> d.category = Lorem.Sentence(2))
                            .With(d => d.commentCount = rand.Next(10000))
                            .With(d => d.content = Builder<Content>.CreateNew()
                                .With(c => c.name = Faker.Lorem.Paragraph(5)).Build())
                            .With(d => d.description = Lorem.Sentence(10))
                            .With(d => d.duration = rand.Next(10000))
                            .With(d => d.favoriteCount = rand.Next(100))
                            .With(d => d.id = rand.Next().ToString())
                            .With(d => d.likeCount = rand.Next(10000).ToString())
                            .With(d => d.player = Builder<Player>.CreateNew()
                                .With(p => p.def = Lorem.GetFirstWord()).Build())
                            .With(d => d.rating = rand.Next(100))
                            .With(d => d.restrictions = Builder<Restriction>.CreateListOfSize(10).All()
                                .With(r => r.countries = Faker.Address.Country())
                                .With(r => r.relationship = Faker.Lorem.GetFirstWord())
                                .With(r => r.type = Lorem.GetFirstWord()).Build())
                            .With(d => d.ratingCount = rand.Next(1000))
                            .With(d => d.status = Builder<Status>.CreateNew()
                                .With(s => s.reason = Faker.Lorem.GetFirstWord())
                                .With(s => s.value = Lorem.GetFirstWord()).Build())
                            .With(d => d.tags = Faker.Lorem.Words(10).ToList())
                            .With(d => d.thumbnail = Builder<Thumbnail>.CreateNew()
                                .With(t => t.hqDefault = rand.Next(1).ToString())
                                .With(t => t.sqDefault= rand.Next(1).ToString()).Build())
                            .With(d => d.title = Lorem.Sentence(5))
                            .With(d => d.updated = DateTime.Now.Subtract(TimeSpan.FromDays(rand.Next(10000))))
                            .With(d => d.uploaded = DateTime.Now.Subtract(TimeSpan.FromDays(rand.Next(10000))))
                            .With(d => d.uploader = Faker.Name.FullName()).Build());
                    _youtube = ylist.Build();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}