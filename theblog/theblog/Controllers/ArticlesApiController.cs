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
    public class ArticlesApiController : ControllerBase
    {
        //using api/article to get data for all articles
        [HttpGet]
        [Route("")]
        public List<Articles> GetArticles()
        {
            JmanblogContext context = new JmanblogContext();

            List<Articles> articlelist = (from items in context.Articles
                                   orderby items.ArticleDate descending
                                   select items).ToList();
            return articlelist;
        }

        //using api/article/5 to get data for a specific articleId in Articles
        [HttpGet]
        [Route("article/id/{articleId}")]
        public Articles GetArticle(int articleId)
        {
            JmanblogContext context = new JmanblogContext();

            if (articleId > 0)
            {
                Articles article = context.Articles.Find(articleId);
                return article;
            }
            else
            {
                return null;
            }

        }

        [HttpGet]
        [Route("article/{articleShort}")]
        // using api/article/shortname to get the article that matches the shortname in database
        // If not found, return null

        public List<Articles> GetArticleByShortname([FromRoute] string articleShort)
        {
            int spacesToAdd = (20 - articleShort.Length);
            string searchArticleShort = articleShort + new string(' ', spacesToAdd);

            JmanblogContext context = new JmanblogContext();
            List<Articles> articles = (from items in context.Articles
                                       where items.ArticleShort.Equals(searchArticleShort)
                                       orderby items.ArticleDate
                                       select items).ToList();
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
        [Route("article/topic/{topicId}")]
        // using api/article/topic/4 to list all articles for certain topicId
        // If no articles for the topic are found then return null

        public List<Articles> GetArticlesByTopic([FromRoute] int topicId)
        {
            JmanblogContext context = new JmanblogContext();
            List<Articles> articles = (from items in context.Articles
                                         where items.ArticleMainTopic == topicId
                                         orderby items.ArticleDate
                                         select items).ToList();
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