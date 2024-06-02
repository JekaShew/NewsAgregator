using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.CommentInterfaces
{
    public interface ICommentServices
    {
        public Task<List<CommentVM>> TakeComments();

        public Task<CommentVM> TakeCommentById(Guid id);

        public Task AddComment(CommentVM comment);

        public Task DeleteComment(Guid id);

        public Task UpdateComment(CommentVM updatedComment);
    }
}
