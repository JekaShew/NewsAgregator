using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.CommentInterfaces;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.CommentControllers
{

    [Route("/api/comment")]
    public class CommentController : Controller
    {
        private readonly ICommentServices _commentServices;

        public CommentController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        [HttpGet("getparameters")]
        public async Task<IActionResult> GetParameters()
        {
            var result = await _commentServices.GetCommentParameters();

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CommentVM commentVM)
        {
            await _commentServices.AddComment(commentVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _commentServices.TakeComments();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid/{id}")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _commentServices.TakeCommentById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _commentServices.DeleteComment(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(CommentVM commentVM)
        {
            await _commentServices.UpdateComment(commentVM);
            return Ok();
        }
    }
}
