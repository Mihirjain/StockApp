using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.data;
using api.dto.stocks;
using api.mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.stocks.ToList().Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.stocks.Find(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = _context.stocks.FirstOrDefault(x=> x.Id == id);
            if(stockModel == null){
                return NotFound();
            }

            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Industry = updateDto.Industry;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.MarketCap = updateDto.MarketCap;
            stockModel.Purchase = updateDto.Purchase;

            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stockModel = _context.stocks.FirstOrDefault(x => x.Id == id);
            if(stockModel == null){
                return NotFound();
            }

            _context.stocks.Remove(stockModel);
            _context.SaveChanges();
            return NoContent();
        }
    }
}