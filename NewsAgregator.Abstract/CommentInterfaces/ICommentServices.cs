using NewsAgregator.ViewModels.Additional;
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
        public Task<List<CommentVM>> TakeCommentsAsync();

        public Task<CommentVM> TakeCommentByIdAsync(Guid id);

        public Task AddCommentAsync(CommentVM comment);

        public Task DeleteCommentAsync(Guid id);

        public Task UpdateCommentAsync(CommentVM updatedComment);
        public Task<CommentParameters> GetCommentParametersAsync();
    }
}
