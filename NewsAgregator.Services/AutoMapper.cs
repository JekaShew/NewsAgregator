using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Data.Models.Account, ViewModels.Data.AccountVM>().ReverseMap();
            CreateMap<Data.Models.Comment, ViewModels.Data.CommentVM>().ReverseMap();
            CreateMap<Data.Models.Complaint, ViewModels.Data.ComplaintVM>().ReverseMap();
            CreateMap<Data.Models.ComplaintStatus, ViewModels.Data.ComplaintStatusVM>().ReverseMap();
            CreateMap<Data.Models.ComplaintType, ViewModels.Data.ComplaintTypeVM>().ReverseMap();
            CreateMap<Data.Models.News, ViewModels.Data.NewsVM>().ReverseMap();
            CreateMap<Data.Models.NewsStatus, ViewModels.Data.NewsStatusVM>().ReverseMap();
            CreateMap<Data.Models.Policy, ViewModels.Data.PolicyVM>().ReverseMap();
            CreateMap<Data.Models.Role, ViewModels.Data.RoleVM>().ReverseMap();
            CreateMap<Data.Models.RolePolicy, ViewModels.Data.RolePolicyVM>().ReverseMap();
            CreateMap<Data.Models.AccountStatus, ViewModels.Data.AccountStatusVM>().ReverseMap();
            CreateMap<Data.Models.Weather, ViewModels.Data.WeatherVM>().ReverseMap();
            CreateMap<Data.Models.WeatherStatus, ViewModels.Data.WeatherStatusVM>().ReverseMap();
        }
    }
}
