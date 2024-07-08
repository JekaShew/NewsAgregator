using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.CommentInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.CommentServices
{
    public class CommentServices : ICommentServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly IMapper _mapper;
        public CommentServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }

        public async Task<CommentParameters> GetCommentParameters()
        {
            var commentParameters = new CommentParameters()
            {
                Accounts = await _appDBContext.Accounts.Select(acc => new Parameter { Id = acc.Id, Text = acc.UserName }).ToListAsync(),
                Newses = await _appDBContext.Newses.Select(n => new Parameter { Id = n.Id, Text = n.Title }).ToListAsync()
            };
            return commentParameters;

        }

        public async Task ConvertCommentParameters(CommentVM commentVM)
        {
            var account = await _appDBContext.Accounts
                        .AsNoTracking()
                        .FirstOrDefaultAsync(accs => accs.Id == commentVM.AccountId);
            var news = await _appDBContext.Newses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(r => r.Id == commentVM.NewsId);
            commentVM.FromDataModel(account, news);

        }

        public async Task AddComment(CommentVM comment)
        {
            var newComment = _mapper.Map<Data.Models.Comment>(comment);
            newComment.Id = Guid.NewGuid();
            newComment.Account = null;
            newComment.News = null;

            await _appDBContext.AddAsync(newComment);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteComment(Guid id)
        {
            _appDBContext.Comments.Remove(await _appDBContext.Comments.FirstOrDefaultAsync(c => c.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<CommentVM> TakeCommentById(Guid id)
        {
            var comment = _mapper.Map<CommentVM>(await _appDBContext.Comments
                .AsNoTracking()
                .Include(a => a.Account)
                .Include(n => n.News)
                .FirstOrDefaultAsync(c => c.Id == id));

            var commentParameters = await GetCommentParameters();
            await ConvertCommentParameters(comment);
            comment.Accounts = commentParameters.Accounts;
            comment.Newses = commentParameters.Newses;

            return comment;
        }

        public async Task<List<CommentVM>> TakeComments()
        {
            var commentVMs = _mapper.Map<List<CommentVM>>(await _appDBContext.Comments
                .AsNoTracking()
                .Include(a => a.Account)
                .Include(n => n.News)
                .ToListAsync());

            foreach (var commentVM in commentVMs)
            {
                await ConvertCommentParameters(commentVM);
            }

            return commentVMs;
        }

        public async Task UpdateComment(CommentVM updatedComment)
        {
            var comment = await _appDBContext.Comments.FirstOrDefaultAsync(c => c.Id == updatedComment.Id);

            if (comment != null)
            {
                comment.Text = updatedComment.Text;
                comment.Date = updatedComment.Date;
                comment.AccountId = updatedComment.AccountId;
                comment.Account = null;
                comment.NewsId = updatedComment.NewsId;
                comment.News = null;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
