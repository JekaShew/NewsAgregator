using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Mapper.DataMappers
{
    [Mapper]
    public static partial class CommentMapper
    {
        [MapProperty([nameof(Comment.Account), nameof(Comment.Account.Id)],
            [nameof(CommentVM.Account), nameof(CommentVM.Account.Id)])]
        [MapProperty([nameof(Comment.Account), nameof(Comment.Account.UserName)],
            [nameof(CommentVM.Account), nameof(CommentVM.Account.Text)])]

        [MapProperty([nameof(Comment.News), nameof(Comment.News.Id)],
            [nameof(CommentVM.News), nameof(CommentVM.News.Id)])]
        [MapProperty([nameof(Comment.News), nameof(Comment.News.Title)],
            [nameof(CommentVM.News), nameof(CommentVM.News.Text)])]
        public static partial CommentVM? CommentToCommentVM(Comment? comment);

        public static partial Comment? CommentVMToComment(CommentVM? commentVM);

    }
}
