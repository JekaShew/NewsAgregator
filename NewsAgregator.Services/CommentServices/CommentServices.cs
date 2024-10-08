﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Abstract.CommentInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Mapper.DataMappers;
using NewsAgregator.Mapper.PropertiesMappers;
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
        private readonly IAccountServices _accountServices;
        public CommentServices(AppDBContext appDBContext, IAccountServices accountServices)
        {
            _appDBContext = appDBContext;
            _accountServices = accountServices;
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
            if (commentVM.Date == null)
            {
                newComment.Date = DateTime.UtcNow;
            }
            if(commentVM.AccountId == null)
            {
                newComment.AccountId = _accountServices.GetCurrentAccountId();
            }

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
            var commentVM = CommentMapper.CommentToCommentVM(await _appDBContext.Comments
                .AsNoTracking()
                .Include(a => a.Account)
                .Include(n => n.News)
                .FirstOrDefaultAsync(c => c.Id == id));

            var commentParameters = await GetCommentParametersAsync();
            commentVM.Accounts = commentParameters.Accounts;
            commentVM.Newses = commentParameters.Newses;

            return commentVM;
        }

        public async Task<List<CommentVM>> TakeCommentsAsync()
        {
            var commentVMs = (await _appDBContext.Comments
                .AsNoTracking()
                .Include(a => a.Account)
                .Include(n => n.News)
                .ToListAsync()).Select(c => CommentMapper.CommentToCommentVM(c)).ToList();

            return commentVMs;
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
