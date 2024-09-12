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
    public static partial class ComplaintMapper
    {
        [MapProperty([nameof(Complaint.News), nameof(Complaint.News.Id)],
            [nameof(ComplaintVM.News), nameof(ComplaintVM.News.Id)])]
        [MapProperty([nameof(Complaint.News), nameof(Complaint.News.Title)],
            [nameof(ComplaintVM.News), nameof(ComplaintVM.News.Text)])]

        [MapProperty([nameof(Complaint.Comment), nameof(Complaint.Comment.Id)],
            [nameof(ComplaintVM.Comment), nameof(ComplaintVM.Comment.Id)])]
        [MapProperty([nameof(Complaint.Comment), nameof(Complaint.Comment.Text)],
            [nameof(ComplaintVM.Comment), nameof(ComplaintVM.Comment.Text)])]

        [MapProperty([nameof(Complaint.ComplaintStatus), nameof(Complaint.ComplaintStatus.Id)],
            [nameof(ComplaintVM.ComplaintStatus), nameof(ComplaintVM.ComplaintStatus.Id)])]
        [MapProperty([nameof(Complaint.ComplaintStatus), nameof(Complaint.ComplaintStatus.Title)],
            [nameof(ComplaintVM.ComplaintStatus), nameof(ComplaintVM.ComplaintStatus.Text)])]

        [MapProperty([nameof(Complaint.ComplaintType), nameof(Complaint.ComplaintType.Id)],
            [nameof(ComplaintVM.ComplaintType), nameof(ComplaintVM.ComplaintType.Id)])]
        [MapProperty([nameof(Complaint.ComplaintType), nameof(Complaint.ComplaintType.Title)],
            [nameof(ComplaintVM.ComplaintType), nameof(ComplaintVM.ComplaintType.Text)])]

        [MapProperty([nameof(Complaint.User), nameof(Complaint.User.Id)],
            [nameof(ComplaintVM.User), nameof(ComplaintVM.User.Id)])]
        [MapProperty([nameof(Complaint.User), nameof(Complaint.User.UserName)],
            [nameof(ComplaintVM.User), nameof(ComplaintVM.User.Text)])]

        [MapProperty([nameof(Complaint.Administrator), nameof(Complaint.Administrator.Id)],
            [nameof(ComplaintVM.Administrator), nameof(ComplaintVM.Administrator.Id)])]
        [MapProperty([nameof(Complaint.Administrator), nameof(Complaint.Administrator.UserName)],
            [nameof(ComplaintVM.Administrator), nameof(ComplaintVM.Administrator.Text)])]
        public static partial ComplaintVM? ComplaintToComplaintVM(Complaint? notificationMessage);

        public static partial Complaint? ComplaintVMToComplaint(ComplaintVM? notificationMessageVM);

    }
}
