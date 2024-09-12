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
        public CommentServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<CommentParameters> GetCommentParametersAsync()
        {
            var commentParameters = new CommentParameters()
            {
                Accounts = (await _appDBContext.Accounts.ToListAsync()).Select(a => CommentParametersMapper.AccountToParameter(a)).ToList(),
                Newses = (await _appDBContext.Newses.ToListAsync()).Select(n => CommentParametersMapper.NewsToParameter(n)).ToList(),
            };
            return commentParameters;
        }

        public async Task AddCommentAsync(CommentVM commentVM)
        {
            var newComment = CommentMapper.CommentVMToComment(commentVM);
            newComment.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newComment);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            _appDBContext.Comments.Remove(await _appDBContext.Comments.FirstOrDefaultAsync(c => c.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<CommentVM> TakeCommentByIdAsync(Guid id)
        {
            var comment = CommentMapper.CommentToCommentVM(await _appDBContext.Comments
                .AsNoTracking()
                .Include(a => a.Account)
                .Include(n => n.News)
                .FirstOrDefaultAsync(c => c.Id == id));

            var commentParameters = await GetCommentParametersAsync();
            comment.Accounts = commentParameters.Accounts;
            comment.Newses = commentParameters.Newses;

            return comment;
        }

        public async Task<List<CommentVM>> TakeCommentsAsync()
        {
            var commentVMs = (await _appDBContext.Comments
                .AsNoTracking()
                .Include(a => a.Account)
                .Include(n => n.News)
                .ToListAsync()).Select(c => CommentMapper.CommentToCommentVM(c)).ToList();

            return null;
        }

        public async Task UpdateCommentAsync(CommentVM updatedCommentVM)
        {
            var comment = await _appDBContext.Comments.FirstOrDefaultAsync(c => c.Id == updatedCommentVM.Id);
            //comment = CommentMapper.CommentVMToComment(updatedCommentVM);

            await _appDBContext.SaveChangesAsync();

            if (comment != null)
            {
                comment.Text = updatedCommentVM.Text;
                comment.Date = updatedCommentVM.Date;
                comment.AccountId = updatedCommentVM.AccountId;
                comment.NewsId = updatedCommentVM.NewsId;

                await _appDBContext.SaveChangesAsync();
            }
        }
    }
}
