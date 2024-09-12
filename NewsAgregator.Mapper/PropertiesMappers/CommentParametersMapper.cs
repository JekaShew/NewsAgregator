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
    public static partial class CommentParametersMapper
    {
        [MapProperty(nameof(Account.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(Account.UserName), nameof(Parameter.Text))]
        public static partial Parameter? AccountToParameter(Account? account);

        [MapProperty(nameof(News.Id), nameof(Parameter.Id))]
        [MapProperty(nameof(News.Title), nameof(Parameter.Text))]
        public static partial Parameter? NewsToParameter(News? news);
    }
}
