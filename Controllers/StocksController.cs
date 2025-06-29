using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Mappers;

namespace server.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StocksController(ApplicationDBContext context, Interfaces.IStockRepository stockRepository) : ControllerBase

    {
        private readonly Data.ApplicationDBContext _context = context;
        private readonly Interfaces.IStockRepository _stockRepository = stockRepository;

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var stocks = await _stockRepository.GetStocksAsyn();
            return Ok(stocks.Select(s => s.getStockWithoutComments()).ToList());
        }

    }
}