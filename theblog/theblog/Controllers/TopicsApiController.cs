using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using theblog.Models;


namespace theblog.Controllers
{
    [Route("api")]
    [ApiController]
    public class TopicsApiController : ControllerBase
    {
        [HttpGet]
        [Route("topics")]
        public List<ArticleCountByTopic> GetTopicSummary()
        {
            JmanblogContext context = new JmanblogContext();

            List<Topics> topics = (from items in context.Topics
                                       orderby items.MainTopicName
                                       select items).ToList();

            List<Articles> articles = context.Articles.ToList();
            List<ArticleCountByTopic> topicoutput = new List<ArticleCountByTopic>();

            //setting temporary variables to generate list output
            int subTopicCountOut = 0;

            for (int i = 0; i<topics.Count; i++)
            {
                subTopicCountOut = 0;

                for (int j=0; j<articles.Count; j++)
                {
                    if (articles[j].ArticleMainTopic == topics[i].TopicId)
                    {
                        subTopicCountOut++;
                    }
                }

                //if there are articles under a subtopic then add object to the ArticleCountByTopic -list
                if (subTopicCountOut != 0)
                {
                    topicoutput.Add(new ArticleCountByTopic()
                    {
                        TopicId = topics[i].TopicId,
                        MainTopicName = topics[i].MainTopicName,
                        SubTopicName = topics[i].SubTopicName,
                        ArticleCount = subTopicCountOut
                    });

                }


            }
            return topicoutput;
        }

        [HttpGet]
        [Route("main/{mainTopicName}")]
        public List<Articles> GetAllArticlesByMainTopic([FromRoute] string mainTopicName)
        {
            JmanblogContext context = new JmanblogContext();
            List<Articles> articles = new List<Articles>();
            List<Topics> topics = (from items in context.Topics
                                   where items.MainTopicName.Equals(mainTopicName)
                                   orderby items.SubTopicName
                                   select items).ToList();

            //if searching for a topic that does not match, return null
            //otherwise add all matching articles to empty List<Articles>
            //return value could be modified to output a reason for null as an article if necessary
            if (topics.Count == 0)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < topics.Count; i++)
                {
                    articles.AddRange(from items in context.Articles
                                      where items.ArticleMainTopic == topics[i].TopicId
                                      orderby items.ArticleDate
                                      select items);
                }
            }

            if (articles.Count == 0)
            {
                return null;
            }
            else
            {
                return articles;
            }
        }

        [HttpGet]
        [Route("sub/{subTopicName}")]
        public List<Articles> GetAllArticlesBySubTopic([FromRoute] string subTopicName)
        {
            JmanblogContext context = new JmanblogContext();
            List<Articles> articles = new List<Articles>();
            //match the subTopicName to subtopics in topics
            //in reality there can only be one match -unless there are errors in the database
            //subtopicname is not a unique key and more than one id can exist in worst case scenario
            //database should be modified to improve performance
            List<Topics> topics = (from items in context.Topics
                                   where items.SubTopicName.Equals(subTopicName)
                                   orderby items.SubTopicName
                                   select items).ToList();

            //if searching for a topic that does not match, return null
            //otherwise add all matching articles to empty List<Articles>
            if (topics.Count == 0)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < topics.Count; i++)
                {
                    articles.AddRange(from items in context.Articles
                                      where items.ArticleMainTopic == topics[i].TopicId
                                      orderby items.ArticleDate
                                      select items);
                }
            }

            if (articles.Count == 0)
            {
                return null;
            } else
            {
                return articles;
            }
            
        }

    }
}