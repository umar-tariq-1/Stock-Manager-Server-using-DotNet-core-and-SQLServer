using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Comment;
using server.Models;

namespace server.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllComments();
        public Task<Comment?> GetCommentById(int id);

        public Task<Comment> CreateCommentAsync(Comment comment);
        public Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDto updateCommentDto);
        public Task<Comment?> DeleteCommentAsync(int id);
    }
}