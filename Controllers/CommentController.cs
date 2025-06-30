using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Dtos.Comment;
using server.Dtos.Stock;
using server.Interfaces;
using server.Mappers;
using server.Models;

namespace server.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(ICommentRepository commentRepository, IStockRepository stockRepository) : ControllerBase
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IStockRepository _stockRepository = stockRepository;


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound("Comment not found");
            }
            var commentDto = comment.ToCommentDto();
            return Ok(commentDto);
        }


        [HttpGet("/api/comments")]
        public async Task<ActionResult<List<Comment>>> GetAllComments()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _commentRepository.GetAllComments();
            var commentDtos = comments.Select(c => c.ToCommentDto());
            return Ok(commentDtos);
        }


        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment([FromBody] CreateCommentDto createCommentDto, int stockId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool doesExist = await _stockRepository.StockExistsAsync(stockId);

            if (!doesExist)
            {
                return NotFound("Stock not found");
            }

            var commentModel = createCommentDto.CreateCommentDto(stockId);
            var newComment = await _commentRepository.CreateCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.Id }, newComment.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Comment>> UpdateComment([FromBody] UpdateCommentDto updateCommentDto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedComment = await _commentRepository.UpdateCommentAsync(id, updateCommentDto);

            if (updatedComment == null)
            {
                return NotFound("Comment not found.");
            }

            return Ok(updatedComment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deletedComment = await _commentRepository.DeleteCommentAsync(id);

            if (deletedComment == null)
            {
                return NotFound("Comment not found.");
            }

            return NoContent();
        }
    }
}