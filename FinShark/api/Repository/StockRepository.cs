using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;   
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {

        private readonly ApplicationDBContext _context; 
        public StockRepository(ApplicationDBContext context){
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ID == id);

            if(stock == null)
            {
                return null;
            }
            _context.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;   
        }

        public Task<List<Stock>> GetAllAsync()
        {
            return  _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = _context.Stocks.FirstOrDefault(x => x.ID == id);
            if(existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;
            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}