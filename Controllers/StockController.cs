using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Dtos.Stock;
using server.Interfaces;
using server.Mappers;

namespace server.Controllers
{
    [Route("api/stock")]
    [ApiController]
    [Authorize]

    public class StockController(IStockRepository stockRepository) : ControllerBase
    {
        private readonly IStockRepository _stockRepository = stockRepository;

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStock(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepository.GetStockByIdAsyn(id);

            if (stock == null)
            {
                return NotFound("Stock not found.");
            }

            return Ok(stock.ToStockDto());
        }

        [HttpGet("/api/stocks")]
        public async Task<IActionResult> GetStocks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stocks = await _stockRepository.GetStocksAsyn();
            return Ok(stocks.Select(s => s.ToStockDto()).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockDto createStockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newStock = createStockDto.CreateStockDto();
            await _stockRepository.CreateStockAsync(newStock);
            return CreatedAtAction(nameof(GetStock), new { id = newStock.Id }, newStock.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedStock = await _stockRepository.UpdateStockAsync(id, stock);

            if (updatedStock == null)
            {
                return NotFound();
            }

            return Ok(updatedStock.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepository.DeleteStockAsync(id);

            if (stock == null)
            {
                return NotFound("Stock not found.");
            }

            return NoContent();
        }
    }
}