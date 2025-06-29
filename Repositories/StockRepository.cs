using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Dtos.Stock;
using server.Interfaces;
using server.Models;

namespace server.Repositories
{
    public class StockRepository(server.Data.ApplicationDBContext context) : IStockRepository
    {
        private readonly server.Data.ApplicationDBContext _context = context;

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (existingStock == null)
            {
                return null;
            }
            _context.Stocks.Remove(existingStock);
            await _context.SaveChangesAsync();
            return existingStock;

        }

        public async Task<Stock?> GetStockByIdAsyn(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            return stock;
        }

        public async Task<List<Stock>> GetStocksAsyn()
        {
            List<Stock> stocks = await _context.Stocks.ToListAsync();
            return stocks;
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockDto stock)
        {
            var existingStock = await GetStockByIdAsyn(id);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stock.Symbol;
            existingStock.CompanyName = stock.CompanyName;
            existingStock.Purchase = stock.Purchase;
            existingStock.LastDiv = stock.LastDiv;
            existingStock.Industry = stock.Industry;
            existingStock.MarketCap = stock.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;
        }

    }
}