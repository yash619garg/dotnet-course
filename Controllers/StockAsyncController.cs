using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/StockAsync")]
    [ApiController]
    public class StockAsyncController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockAsyncController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // var stocks = await _context.Stocks.Select(x => x.ToStockDto()).ToListAsync();

            var stocks = await _stockRepo.GetAllStocksAsync(query);
            var stockDto = stocks.Select(x => x.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // var stock = await _context.Stocks.FindAsync(id);
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToStockFromCreateDto();
            // await _context.Stocks.AddAsync(stockModel);
            // await _context.SaveChangesAsync();
            await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetStock), new { id = stockModel.Id }, stockModel.ToStockDto());

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto stockRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            // if (stockModel == null)
            // {
            //     return NotFound();
            // }
            // stockModel.CompanyName = stockRequestDto.CompanyName;
            // stockModel.Symbol = stockRequestDto.Symbol;
            // stockModel.LastDiv = stockRequestDto.LastDiv;
            // stockModel.Purchase = stockRequestDto.Purchase;
            // stockModel.Industry = stockRequestDto.Industry;
            // stockModel.MarketCap = stockRequestDto.MarketCap;
            // await _context.SaveChangesAsync();

            var stockModel = await _stockRepo.UpdateAsync(id, stockRequestDto);
            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            // if (stockModel == null)
            // {
            //     return NotFound();
            // }
            // _context.Stocks.Remove(stockModel);
            // // no RemoveAsync 
            // await _context.SaveChangesAsync();
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}