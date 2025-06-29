using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Mappers;

namespace server.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(Data.ApplicationDBContext context, Interfaces.IStockRepository stockRepository) : ControllerBase
    {
        private readonly Data.ApplicationDBContext _context = context;
        private readonly Interfaces.IStockRepository _stockRepository = stockRepository;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock(int id)
        {
            var stock = await _stockRepository.GetStockByIdAsyn(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.getStockWithoutComments());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] server.Dtos.Stock.CreateStockDto createStockDto)
        {
            // Validate the input
            if (createStockDto == null)
            {
                return BadRequest();
            }
            else if (string.IsNullOrEmpty(createStockDto.Symbol) || string.IsNullOrEmpty(createStockDto.CompanyName) || string.IsNullOrEmpty(createStockDto.Industry))
            {
                return BadRequest("Symbol, CompanyName, and Industry cannot be empty.");
            }

            else if (createStockDto.Purchase < 0 || createStockDto.LastDiv < 0 || createStockDto.MarketCap < 0)
            {
                return BadRequest("Purchase, LastDiv, and MarketCap must be non-negative.");
            }

            //Create after validation
            var newStock = createStockDto.createStock();
            await _stockRepository.CreateStockAsync(newStock);
            return CreatedAtAction(nameof(GetStock), new { id = newStock.Id }, newStock.getStockWithoutComments());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] server.Dtos.Stock.UpdateStockDto stock)
        {
            if (stock == null)
            {
                return BadRequest("Stock cannot be null.");
            }
            else if (string.IsNullOrEmpty(stock.Symbol) || string.IsNullOrEmpty(stock.CompanyName) || string.IsNullOrEmpty(stock.Industry))
            {
                return BadRequest("Symbol, CompanyName, and Industry cannot be empty.");
            }
            else if (stock.Purchase < 0 || stock.LastDiv < 0 || stock.MarketCap < 0)
            {
                return BadRequest("Purchase, LastDiv, and MarketCap must be non-negative.");
            }

            var updatedStock = await _stockRepository.UpdateStockAsync(id, stock);

            if (updatedStock == null)
            {
                return NotFound();
            }

            return Ok(updatedStock.getStockWithoutComments());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _stockRepository.DeleteStockAsync(id);
            if (stock == null)
            {
                return NotFound("Stock not found.");
            }

            return NoContent();
        }
    }
}