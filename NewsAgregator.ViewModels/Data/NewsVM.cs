﻿using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.ViewModels.Data
{
    public class NewsVM
    {
        public Guid Id { get; set; }
        [Required, MinLength(3)]
        public string? Title { get; set; }
        [Required, MinLength(3)]
        public string? Description { get; set; }
        public string? Text { get; set; }
        public string? TextHTML { get; set; }
        [Required, MinLength(3)]
        public string SourceUrl { get; set; }
        public DateTime? Date { get; set; }
        public float? PositiveRating { get; set; }

        public Guid? SourceId { get; set; }
        public Parameter? Source { get; set; }

        public Guid? NewsStatusId { get; set; }
        public List<Parameter>? NewsStatuses { get; set; }
        public Parameter? NewsStatus { get; set; }

        public List<ComplaintVM>? Complaints { get; set; }

        public List<CommentParameter>? Comments { get; set; }

        //public void FromDataModel(NewsAgregator.Data.Models.NewsStatus newsStatus)
        //{
        //    if (newsStatus != null)
        //    {
        //        NewsStatus = new Parameter
        //        {
        //            Id = newsStatus.Id,
        //            Text = newsStatus.Title,

        //        };
        //    }
        //    else
        //    {
        //        NewsStatus = new Parameter
        //        {
        //            Id = null,
        //            Text = "",

        //        };
        //    }
        //}
    }
}
