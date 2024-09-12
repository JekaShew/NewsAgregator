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
    public static partial class NewsMapper
    {
        [MapProperty([nameof(News.NewsStatus), nameof(News.NewsStatus.Id)],
            [nameof(NewsVM.NewsStatus), nameof(NewsVM.NewsStatus.Id)])]
        [MapProperty([nameof(News.NewsStatus), nameof(News.NewsStatus.Title)],
            [nameof(NewsVM.NewsStatus), nameof(NewsVM.NewsStatus.Text)])]
        public static partial NewsVM? NewsToNewsVM(News? news);

        public static partial News? NewsVMToNews(NewsVM? newsVM);

    }
}
