using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Dtos.Stock;
using server.Models;

namespace server.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetStocksAsyn();
        Task<Stock?> GetStockByIdAsyn(int id);

        Task<Stock> CreateStockAsync(Stock stock);

        Task<Stock?> UpdateStockAsync(int id, UpdateStockDto stock);

        Task<Stock?> DeleteStockAsync(int id);

        Task<bool> StockExistsAsync(int id);
    }
}