using System;
using System.Collections.Generic;
using BookStore.ProductService.Models;

namespace BookStore.ProductService.Persistence
{
    public interface IProductRepository
    {
        void Add(Product product);
        IEnumerable<Product> GetAll();
        Product GetBy(Guid id);
        bool Remove(Guid id);
        void Update(Product product);
    }
}