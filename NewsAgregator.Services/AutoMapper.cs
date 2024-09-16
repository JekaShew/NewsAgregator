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

            CreateMap<Data.Models.ComplaintStatus, ViewModels.Data.ComplaintStatusVM>().ReverseMap();
            CreateMap<Data.Models.ComplaintType, ViewModels.Data.ComplaintTypeVM>().ReverseMap();
            CreateMap<Data.Models.NewsStatus, ViewModels.Data.NewsStatusVM>().ReverseMap();
            CreateMap<Data.Models.Policy, ViewModels.Data.PolicyVM>().ReverseMap();
            CreateMap<Data.Models.AccountStatus, ViewModels.Data.AccountStatusVM>().ReverseMap();
            CreateMap<Data.Models.WeatherStatus, ViewModels.Data.WeatherStatusVM>().ReverseMap();
            //CreateMap<Data.Models.Account, ViewModels.Data.AccountVM>().ReverseMap();

            CreateMap<Data.Models.Account, ViewModels.Data.CreateAccountVM>()
                .ForMember(d => d.AccountStatus, (options) => options.Ignore())
                .ForMember(d => d.Role, (options) => options.Ignore())
                .ForMember(d => d.UserComplaints, (options) => options.Ignore())
                .ForMember(d => d.AdministratorComplaints, (options) => options.Ignore())
                .ForMember(d => d.RecipientUsers, (options) => options.Ignore())
                .ForMember(d => d.Comments, (options) => options.Ignore())
                .ForMember(d => d.SenderAdministrators, (options) => options.Ignore());
            CreateMap<ViewModels.Data.CreateAccountVM, Data.Models.Account>()
                .ForMember(d => d.AccountStatus, (options) => options.Ignore())
                .ForMember(d => d.Role, (options) => options.Ignore())
                .ForMember(d => d.UserComplaints, (options) => options.Ignore())
                .ForMember(d => d.AdministratorComplaints, (options) => options.Ignore())
                .ForMember(d => d.RecipientUsers, (options) => options.Ignore())
                .ForMember(d => d.Comments, (options) => options.Ignore())
                .ForMember(d => d.SenderAdministrators, (options) => options.Ignore());

            CreateMap<Data.Models.News, ViewModels.Data.NewsVM>()
                .ForMember(d => d.NewsStatus, (options) => options.Ignore())
                .ForMember(d => d.Complaints, (options) => options.Ignore())
                .ForMember(d => d.Comments, (options) => options.Ignore());
            CreateMap<ViewModels.Data.NewsVM, Data.Models.News>()
                .ForMember(d => d.NewsStatus, (options) => options.Ignore())
                .ForMember(d => d.Complaints, (options) => options.Ignore())
                .ForMember(d => d.Comments, (options) => options.Ignore());

            CreateMap<Data.Models.Weather, ViewModels.Data.WeatherVM>()
                .ForMember(d => d.WeatherStatusCommon, (options) => options.Ignore())
                .ForMember(d => d.WeatherStatusMorning, (options) => options.Ignore())
                .ForMember(d => d.WeatherStatusDay, (options) => options.Ignore())
                .ForMember(d => d.WeatherStatusEvening, (options) => options.Ignore())
                .ForMember(d => d.WeatherStatusNight, (options) => options.Ignore());
            CreateMap<ViewModels.Data.WeatherVM, Data.Models.Weather>()
                .ForMember(d => d.WeatherStatusCommon, (options) => options.Ignore())
                .ForMember(d => d.WeatherStatusMorning, (options) => options.Ignore())
                .ForMember(d => d.WeatherStatusDay, (options) => options.Ignore())
                .ForMember(d => d.WeatherStatusEvening, (options) => options.Ignore())
                .ForMember(d => d.WeatherStatusNight, (options) => options.Ignore());

            CreateMap<Data.Models.Comment, ViewModels.Data.CommentVM>()
                .ForMember(d => d.Account, (options) => options.Ignore())
                .ForMember(d => d.News, (options) => options.Ignore())
                .ForMember(d => d.Complaints, (options) => options.Ignore());
            CreateMap<ViewModels.Data.CommentVM, Data.Models.Comment>()
                .ForMember(d => d.Account, (options) => options.Ignore())
                .ForMember(d => d.News, (options) => options.Ignore())
                .ForMember(d => d.Complaints, (options) => options.Ignore());

            CreateMap<Data.Models.Complaint, ViewModels.Data.ComplaintVM>()
                .ForMember(d => d.Comment, (options) => options.Ignore())
                .ForMember(d => d.News, (options) => options.Ignore())
                .ForMember(d => d.ComplaintStatus, (options) => options.Ignore())
                .ForMember(d => d.ComplaintType, (options) => options.Ignore())
                .ForMember(d => d.User, (options) => options.Ignore())
                .ForMember(d => d.Administrator, (options) => options.Ignore());
            CreateMap<ViewModels.Data.ComplaintVM, Data.Models.Complaint>()
               .ForMember(d => d.Comment, (options) => options.Ignore())
               .ForMember(d => d.News, (options) => options.Ignore())
               .ForMember(d => d.ComplaintStatus, (options) => options.Ignore())
               .ForMember(d => d.ComplaintType, (options) => options.Ignore())
               .ForMember(d => d.User, (options) => options.Ignore())
               .ForMember(d => d.Administrator, (options) => options.Ignore());

            CreateMap<Data.Models.NotificationMessage, ViewModels.Data.NotificationMessageVM>()
                .ForMember(d => d.User, (options) => options.Ignore())
                .ForMember(d => d.Administrator, (options) => options.Ignore());
            CreateMap<ViewModels.Data.NotificationMessageVM, Data.Models.NotificationMessage>()
                .ForMember(d => d.User, (options) => options.Ignore())
                .ForMember(d => d.Administrator, (options) => options.Ignore());

            CreateMap<Data.Models.Role, ViewModels.Data.RoleVM>().ReverseMap();
            CreateMap<Data.Models.RolePolicy, ViewModels.Data.RolePolicyVM>().ReverseMap();
            
           
        }
    }
}
