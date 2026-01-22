using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

// StockController handles incoming HTTP requests related to Stock data.
// Controllers are the part of backend that:
// ✅ receives requests
// ✅ returns responses

namespace api.Controllers
{
    [Route("api/stock")]
    // This sets the base URL for this controller.
    // So all endpoints inside this controller start with: api/stock

    [ApiController]
    // This tells ASP.NET Core:
    // This is a Web API controller
    // Enable automatic API behaviors like:
    // automatic model validation
    // better error responses
    // automatic binding rules

    public class StockController : ControllerBase
    {
        // This is an API controller, so it inherits from ControllerBase.
        // ✅ ControllerBase is used for APIs (JSON response, status codes)

        private readonly ApplicationDBContext _context;
        // This variable stores your DbContext.
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        // This is Dependency Injection (DI).
        // ASP.NET Core automatically creates ApplicationDBContext and gives it to controller.

        [HttpGet]
        // This method runs when request comes:
        // GET /api/stock
        public IActionResult GetAll()
        {
            // IActionResult represents the result/response returned by an API action method.
            // This method returns an HTTP response (status code + body).

            // ActionResult<T> is a strongly typed response.
            // It means:
            // “This action returns either:
            // an object of type T
            // OR
            // an HTTP result like NotFound/BadRequest”
            // So it combines: T (data type) + Action results

            var stocks = _context.Stocks.ToList();
            // _context.Stocks represents Stocks table
            // .ToList() executes query and returns list

            // using stockDto -- not wanna return comments
            var stocks2 = _context.Stocks.
            Select(s => s.ToStockDto()).ToList();
            // Select() is used for projection (transforming data)

            return Ok(stocks2);
            // Returns 200 OK with JSON data.
        }

        // GET /api/stock/5
        [HttpGet("{id}")]
        // {id} is a route parameter
        public IActionResult GetById([FromRoute] int id)
        {
            // [FromBody] is an attribute used in controller action parameters
            // It takes JSON sent by client and maps it to a Model / DTO
            // | Attribute      | Reads from   |
            // | -------------- | ------------ |
            // | [FromBody]     | JSON body    |
            // | [FromRoute]    | URL segment  |
            // | [FromQuery]    | Query string |
            // | [FromHeader]   | HTTP headers |

            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
                // 404 Not Found
            }

            // return Ok(stock);
            return Ok(stock.ToStockDto());
            // returns 200 OK + stock json
        }

        [HttpPost]
        //This method will run for HTTP POST requests. 
        // POST /api/stock

        public IActionResult CreateStock([FromBody] CreateStockDto stockDto)
        {
            // CreateStockDto stockDto :
            // This is a request DTO (input DTO).
            // Means:
            // Client will send only required fields to create stock.
            // This prevents sending unwanted fields like Id, etc
            // if client send extra field this dto will ignore them

            // [FromBody] :
            // ASP.NET Core will take JSON from request body and convert it into CreateStockDto.

            var stockModel = stockDto.ToStockFromCreateDto();
            // converting: CreateStockDto (API input) into Stock model/entity (DB object)
            // Because EF Core works with Entity models, not DTOs.


            _context.Stocks.Add(stockModel);
            // You are telling EF Core: Insert this new stock into Stocks table.

            _context.SaveChanges();
            // Now EF Core actually executes the SQL query:

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
            // CreatedAtAction() returns:
            // HTTP status code 201 Created
            // with a Location header pointing to the new resource
            // plus the created object data in response body

            // (A) nameof(GetById) : After creation, client can fetch stock using GetById endpoint.
            // So it points to:
            // GET /api/stock/{id}

            // (B) new { id = stockModel.Id } : This supplies the route parameter for GetById.
            // So final URL becomes like: /api/stock/10

            // (C) stockModel.ToStockDto() : Instead of returning full entity model, you return:
            // StockDto (safe response format)
        }

        [HttpPut]
        [Route("{id}")]
        // PUT /api/stock/{id}
        // This method is triggered when:
        // HTTP method = PUT
        // Route has id parameter

        public IActionResult UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto stockRequestDto)
        {
            // [FromRoute] int id
            // Takes the id from URL path.
            // Example: /api/stock/10 → id = 10

            // [FromBody] UpdateStockRequestDto stockRequestDto
            // Takes the update data from request body (JSON)
            // This is important because:
            // client should not send entity model directly
            // DTO restricts what can be updated

            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            // Find the stock record in DB where: Stock.Id == id
            // If found → stockModel contains DB record
            // If not found → stockModel is null

            if (stockModel == null)
            {
                return NotFound();
            }
            // If that stock doesn’t exist: return 404 Not Found

            stockModel.CompanyName = stockRequestDto.CompanyName;
            stockModel.Symbol = stockRequestDto.Symbol;
            stockModel.Purchase = stockRequestDto.Purchase;
            stockModel.LastDiv = stockRequestDto.LastDiv;
            stockModel.Industry = stockRequestDto.Industry;
            stockModel.MarketCap = stockRequestDto.MarketCap;

            // You are copying data from:
            // UpdateStockRequestDto (client input) into stockModel (EF entity)

            _context.SaveChanges();
            // EF Core will generate and run SQL like:

            return Ok(stockModel.ToStockDto());
            // returns HTTP 200 OK , response body = updated stock data in DTO format
        }

        [HttpDelete]
        [Route("{id}")]
        // This action will run when:
        // HTTP Method = DELETE
        // URL contains {id}

        public IActionResult DeleteStock([FromRoute] int id)
        {
            // id is coming from the URL route.
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            // Before deleting, you must check if the record exists.
            // If found → stockModel holds the entity
            // If not found → null
            if (stockModel == null)
            {
                return NotFound();
            }
            // If stock doesn’t exist → return: 404 Not Found


            _context.Stocks.Remove(stockModel);
            // EF Core marks this entity as: “Deleted” But it’s not deleted in DB yet.
            _context.SaveChanges();
            // Now EF Core executes SQL command like:
            return NoContent();
            // HTTP 204 No Content
            // This is the best practice for DELETE because:
            // deletion successful
            // no response body needed
        }



    }
}