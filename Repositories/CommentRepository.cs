using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Interfaces;
using server.Models;

namespace server.Repositories
{
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        private readonly ApplicationDBContext _context = context;
        public async Task<List<Comment>> GetAllComments()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            return await _context.Comments.FindAsync(id);
        }
    }
}