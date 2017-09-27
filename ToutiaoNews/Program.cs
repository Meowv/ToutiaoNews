using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ToutiaoNews.Data;
using ToutiaoNews.Helper;

namespace ToutiaoNews
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start...");
            GetToutiaoNews();
            Console.WriteLine("end...");
        }

        static string domain = "https://www.toutiao.com/";
        static string domain_para = "api/pc/feed/?category=";
        static string connection = "Server=192.168.40.111;Database=toutiao;User ID=sa;Password=123456";

        static string image_url, title, source, labels, category, content, time;

        public static void GetToutiaoNews()
        {
            List<string> list = new List<string>
            {
                "__all__",
                "news_hot",
                "news_tech",
                "news_society",
                "news_entertainment",
                "news_game",
                "news_sports",
                "news_car",
                "news_finance",
                "funny",
                "essay_joke&as=A115993CAA5F9BC",
                "news_military",
                "news_world",
                "news_fashion",
                "news_travel",
                "news_discovery",
                "news_baby",
                "news_regimen",
                "news_essay",
                "news_history",
                "news_food"
            };

            foreach (var _category in list)
            {
                GetToutiaoNewsCategory(_category);

                string api = domain + domain_para + _category;

                Console.WriteLine(category + ":" + api);

                string json = HttpHelper.HttpGet(api);
                JObject obj = JObject.Parse(json);
                if (obj["message"].ToString() == "success")
                {
                    GetToutiaoNews(obj);
                }
            }
        }

        private static void GetToutiaoNewsCategory(string _category)
        {
            switch (_category)
            {
                case "__all__":
                    category = "推荐";
                    break;
                case "news_hot":
                    category = "热点";
                    break;
                case "news_tech":
                    category = "科技";
                    break;
                case "news_society":
                    category = "社会";
                    break;
                case "news_entertainment":
                    category = "娱乐";
                    break;
                case "news_game":
                    category = "游戏";
                    break;
                case "news_sports":
                    category = "体育";
                    break;
                case "news_car":
                    category = "汽车";
                    break;
                case "news_finance":
                    category = "财经";
                    break;
                case "funny":
                    category = "搞笑";
                    break;
                case "essay_joke&as=A115993CAA5F9BC":
                    category = "段子";
                    break;
                case "news_military":
                    category = "军事";
                    break;
                case "news_world":
                    category = "国际";
                    break;
                case "news_fashion":
                    category = "时尚";
                    break;
                case "news_travel":
                    category = "旅游";
                    break;
                case "news_discovery":
                    category = "探索";
                    break;
                case "news_baby":
                    category = "育儿";
                    break;
                case "news_regimen":
                    category = "养生";
                    break;
                case "news_essay":
                    category = "美文";
                    break;
                case "news_history":
                    category = "历史";
                    break;
                case "news_food":
                    category = "美食";
                    break;
            }
        }

        private static void GetToutiaoNews(JObject obj)
        {
            foreach (var data in obj["data"])
            {
                string article_genre = data["article_genre"].ToString();
                if (article_genre == "article")
                {
                    try
                    {
                        image_url = data["image_url"].ToString();
                        title = data["title"].ToString();
                        GetToutiaoNewsLabel(data);
                        source = data["source"].ToString();
                        string source_url = data["source_url"].ToString();

                        GetToutiaoNewsContent(source_url);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private static void GetToutiaoNewsContent(string source_url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(domain + source_url);
            List<HtmlNode> nodeList = doc.DocumentNode.SelectNodes("//*/script").AsParallel().ToList();
            foreach (var list in nodeList)
            {
                if (list.InnerHtml.Contains("BASE_DATA"))
                {
                    string result = list.InnerHtml;

                    content = Regex.Match(result, ".*content:.'([^'']+)'.*", RegexOptions.IgnoreCase).Value.Replace("content:", "").Replace("'", "").Replace(@".replace(/<br \/>|\n|\r/ig, ),", "").Trim();
                    time = Regex.Match(result, ".*time:.'([^'']+)'.*", RegexOptions.IgnoreCase).Value.Replace("time:", "").Replace("'", "").Trim();

                    InsertToutiaoNews();

                    labels = "";
                }
            }
        }

        private static void InsertToutiaoNews()
        {
            using (NewsDbContext db = new NewsDbContext(connection))
            {
                bool exist = db.News.Any(a => a.Title == title && a.Source == source);
                if (!exist)
                {
                    db.News.Add(new Entity.News()
                    {
                        Title = title,
                        Source = source,
                        Logo = image_url,
                        Labels = labels,
                        Category = category,
                        Pubdate = Convert.ToDateTime(time),
                        Detail = content
                    });
                    db.SaveChanges();

                    Console.WriteLine(title + " ---- ok");
                }
            }
        }

        private static void GetToutiaoNewsLabel(JToken data)
        {
            foreach (string label in data["label"])
            {
                labels += label + ",";
            }
            labels = labels.Substring(0, labels.Length - 1);
        }
    }
}