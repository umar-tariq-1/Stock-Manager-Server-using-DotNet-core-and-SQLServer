using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Mappers;

namespace server.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(server.Interfaces.ICommentRepository commentRepository) : ControllerBase
    {
        private readonly server.Interfaces.ICommentRepository _commentRepository = commentRepository;

        [HttpGet("{id}")]
        public async Task<ActionResult<server.Models.Comment>> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentById(id);
            if (comment == null)
            {
                return NotFound("Comment not found");
            }
            var commentDto = comment.commentDto();
            return Ok(commentDto);
        }

        [HttpGet("/api/comments")]
        public async Task<ActionResult<List<server.Models.Comment>>> GetAllComments()
        {
            var comments = await _commentRepository.GetAllComments();
            var commentDtos = comments.Select(c => c.commentDto());
            return Ok(commentDtos);
        }
    }
}