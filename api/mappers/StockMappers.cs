using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.dto.stocks;
using api.models;

namespace api.mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stocks stockModel)
        {
            return new StockDto{
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }

        public static Stocks ToStockFromCreateDTO(this CreateStockRequestDto createStockDto)
        {
            return new Stocks{
                Symbol = createStockDto.Symbol,
                CompanyName = createStockDto.CompanyName,
                Purchase = createStockDto.Purchase,
                LastDiv = createStockDto.LastDiv,
                Industry = createStockDto.Industry,
                MarketCap = createStockDto.MarketCap
            };
        }
    }
}