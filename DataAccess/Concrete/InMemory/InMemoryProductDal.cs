using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        //constructor newlendikden sonra bunları oluşturcak
        public InMemoryProductDal()
        {
            _products = new List<Product> {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStok=15 },
                new Product{ProductId=2,CategoryId=1,ProductName="kamera",UnitPrice=500,UnitsInStok=3 },
                new Product{ProductId=3,CategoryId=2,ProductName="telefon",UnitPrice=1500,UnitsInStok=2 },
                new Product{ProductId=4,CategoryId=2,ProductName="klavye",UnitPrice=150,UnitsInStok=65},
                new Product{ProductId=5,CategoryId=2,ProductName="fare",UnitPrice=85,UnitsInStok=1 }
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //linq kullanımı

            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);//p nin product ıdesi bizim gönderdiğimiz product ıd sine eşitmi
            _products.Remove(productToDelete);
            
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllCategory(int categoryId)
        {
            return _products.Where(p=>p.CategoryId==categoryId).ToList();
        }

        public void Update(Product product)
        {
            //gönderdiğim ürün idsine sahip olan listedeki ürünü bul
            Product productToUpdate = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);//p nin product ıdesi bizim gönderdiğimiz product ıd sine eşitmi
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStok = product.UnitsInStok;
        }
    }
}
