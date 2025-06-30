using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Title can't be longer than 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(200, ErrorMessage = "Content can't be longer than 500 characters.")]
        public string Content { get; set; } = string.Empty;
    }
}