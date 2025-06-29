using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Mappers
{
    public static class CommentMappers
    {
        public static server.Dtos.Comment.CommentDto commentDto(this server.Models.Comment comment)
        {

            return new Dtos.Comment.CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }
    }
}