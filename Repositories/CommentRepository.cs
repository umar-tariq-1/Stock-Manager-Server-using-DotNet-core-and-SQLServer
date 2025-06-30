using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos.Comment;
using server.Interfaces;
using server.Models;

namespace server.Repositories
{
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var existingComment = await GetCommentById(id);
            if (existingComment == null)
            {
                return null;
            }
            _context.Comments.Remove(existingComment);
            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<List<Comment>> GetAllComments()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDto updateCommentDto)
        {
            var existingComment = await GetCommentById(id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = updateCommentDto.Title;
            existingComment.Content = updateCommentDto.Content;

            await _context.SaveChangesAsync();
            return existingComment;
        }

    }
}