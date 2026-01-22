using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        // static class can only contain static members
        // Why static class?
        // A static class:
        // 1.cannot be instantiated (no object created)
        // 2.contains only static members
        // This is perfect for mapping because: mapping is utility-like functionality

        public static StockDto ToStockDto(this Stock stockModel)
        {
            // It returns StockDto : Means: result of mapping will be DTO.

            // What is this Stock stockModel?

            // This makes it an Extension Method.
            // Definition: Extension Method
            // An extension method allows you to add methods to an existing class without modifying its source code.

            // So now EF model Stock behaves like it already has this function:
            // stockModel.ToStockDto()
            // Even though Stock class doesn’t contain it.
            // Cleaner coding style.

            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(x => x.ToCommentDto()).ToList(),
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }


    }
}