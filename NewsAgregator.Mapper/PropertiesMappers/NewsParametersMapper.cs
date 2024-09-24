using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Mappers.ParametersMappers
{
    [Mapper]
    public static partial class NewsParametersMapper
    {

        [MapProperty(nameof(NewsStatus.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(NewsStatus.Title), nameof(Parameter.Text))]
        public static partial Parameter? NewsStatusToParameter(NewsStatus? newsStatus);


        [MapProperty(nameof(Comment.Id), nameof(CommentParameter.Id))]
        [MapProperty(nameof(Comment.NewsId), nameof(CommentParameter.NewsId))]
        [MapProperty([nameof(Comment.Account), nameof(Comment.Account.UserName)],
        [nameof(CommentParameter.UserName)])]
        [MapProperty(nameof(Comment.Date), nameof(CommentParameter.Date))]
        [MapProperty(nameof(Comment.Text), nameof(CommentParameter.Text))]
        public static partial CommentParameter? CommentToCommentParameter(Comment? comment);

    }
}
