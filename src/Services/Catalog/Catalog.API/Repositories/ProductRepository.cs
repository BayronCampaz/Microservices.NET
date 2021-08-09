using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Find<Product>(p => true).ToListAsync();
        }


        public async Task Create(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Delete(string id)
        {
            var delete =  await _context.Products.DeleteOneAsync(p => p.Id == id);
            return delete.IsAcknowledged && delete.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetBy(string field, string value)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(field, value);
            return await _context.Products.Find<Product>(filter).ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            return await _context.Products.Find<Product>(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Product product)
        {
            var update = await _context.Products.
                                        ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

            return update.IsAcknowledged && update.ModifiedCount > 0;
        }
    }
}
