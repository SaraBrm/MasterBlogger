using MB.Application.Contracts.Article;
using MB.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MB.Infrastructure.EFCore.Repositories
{
    public class ArticleRepository:IArticleRepository
    {
        private readonly MasterBloggerContext _context;

        public ArticleRepository(MasterBloggerContext context)
        {
            _context = context;
        }

        public void CreateAndSave(Article entity)
        {
            _context.Articles.Add(entity);
            Save();
        }

        public bool Exists(string title)
        {
            return _context.Articles.Any(x => x.Title == title);
        }

        public Article Get(long id)
        {
            return _context.Articles.FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> GetList()
        {
            return _context.Articles.Include(x=>x.ArticleCategory).Select(x=> new ArticleViewModel
            {
                Id = x.Id,
                Title= x.Title,
                ArticleCategory=x.ArticleCategory.Title,
                IsDeleted=x.IsDeleted,
                CreationDate=x.CreationDate.ToString(CultureInfo.InvariantCulture)
            }).ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
