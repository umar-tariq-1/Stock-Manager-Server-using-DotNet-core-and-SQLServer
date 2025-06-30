using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Comment;
using server.Models;

namespace server.Mappers
{
    public static class CommentMappers
    {
        public static server.Dtos.Comment.CommentDto ToCommentDto(this Comment comment)
        {

            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }

        public static Comment CreateCommentDto(this CreateCommentDto createCommentDto, int stockId)
        {

            return new Comment
            {
                Title = createCommentDto.Title,
                Content = createCommentDto.Content,
                StockId = stockId
            };
        }
    }
}