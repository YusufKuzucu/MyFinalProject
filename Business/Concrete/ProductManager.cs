using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _IProductDal;
        public ProductManager(IProductDal productDal)
        {
            _IProductDal = productDal;
        }
        public List<Product> GetAll()
        {
            //iş kodları yapıyoruz burda
            return _IProductDal.GetAll();
        }
    }
}
