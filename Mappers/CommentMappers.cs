using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentResponseDto ToCommentDto(this Comment commentModel)
        {
            return new CommentResponseDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto commentRequestDto, int stockId)
        {
            return new Comment
            {
                Title = commentRequestDto.Title,
                Content = commentRequestDto.Content,
                StockId = stockId
            };
        }

    }
}