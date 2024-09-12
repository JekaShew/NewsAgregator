using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Mapper.PropertiesMappers
{
    [Mapper]
    public static partial class ComplaintParametersMapper
    {
        [MapProperty(nameof(Comment.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(Comment.Text), nameof(Parameter.Text))]
        public static partial Parameter? CommentToParameter(Comment? comment);

        [MapProperty(nameof(News.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(News.Title), nameof(Parameter.Text))]
        public static partial Parameter? NewsToParameter(News? news);
        
        [MapProperty(nameof(ComplaintStatus.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(ComplaintStatus.Title), nameof(Parameter.Text))]
        public static partial Parameter? ComplaintStatusToParameter(ComplaintStatus? complaintStatus);
        
        [MapProperty(nameof(ComplaintType.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(ComplaintType.Title), nameof(Parameter.Text))]
        public static partial Parameter? ComplaintTypeToParameter(ComplaintType? complaintType);

        [MapProperty(nameof(Account.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(Account.UserName), nameof(Parameter.Text))]
        public static partial Parameter? AccountToParameter(Account? account);
    }
}
