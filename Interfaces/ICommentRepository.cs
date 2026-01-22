using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComments();
        Task<Comment?> GetCommentById(int id);
        Task<Comment> CreateComment(Comment commentModel);

        Task<Comment?> UpdateComment(int id, UpdateCommentRequestDto commentRequestDto);
        Task<Comment?> DeleteComment(int id);
    }
}