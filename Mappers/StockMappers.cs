using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Mappers
{
    public static class StockMappers
    {
        public static server.Dtos.Stock.StockDto getStockWithoutComments(this server.Models.Stock stock)
        {
            return new server.Dtos.Stock.StockDto
            {
                CompanyName = stock.CompanyName,
                Id = stock.Id,
                Industry = stock.Industry,
                LastDiv = stock.LastDiv,
                MarketCap = stock.MarketCap,
                Purchase = stock.Purchase,
                Symbol = stock.Symbol
            };
        }

        public static server.Models.Stock createStock(this server.Dtos.Stock.CreateStockDto stock)
        {
            return new server.Models.Stock
            {
                CompanyName = stock.CompanyName,
                Industry = stock.Industry,
                LastDiv = stock.LastDiv,
                MarketCap = stock.MarketCap,
                Purchase = stock.Purchase,
                Symbol = stock.Symbol
            };
        }
    }
}