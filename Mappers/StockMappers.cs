using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Mappers
{
    public static class StockMappers
    {
        public static server.Dtos.Stock.StockDto ToStockDto(this server.Models.Stock stock)
        {
            return new server.Dtos.Stock.StockDto
            {
                CompanyName = stock.CompanyName,
                Id = stock.Id,
                Industry = stock.Industry,
                LastDiv = stock.LastDiv,
                MarketCap = stock.MarketCap,
                Purchase = stock.Purchase,
                Symbol = stock.Symbol,
                Comments = [.. stock.Comments.Select(c => c.ToCommentDto())]
            };
        }

        public static server.Models.Stock CreateStockDto(this server.Dtos.Stock.CreateStockDto stock)
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