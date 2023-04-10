using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    //Encryption,Hashing
    public class ProductManager : IProductService
    {
        
        IProductDal _productDal;
        ICategoryService _categoryService;
        //bir entity manager başka bir dalı enjekte edemez
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }
        //add methodunu doğrula productvalidator kurallarına göre
        //Log(); bunu başında çalıştırabiliriz add den sonra da çalıştırabiliriz hata verirsede çalıştırabiliriz
        //atributlara biz tipleri typeof lamak zorundayız

        //Cliam -- iddalar
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
      //  [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            IResult result=BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfProductNameExists(product.ProductName), CheckIfCategoryLimit());
            //business codelar burada yazıyoruz
            //Cross Cutting Concerns leride buraya ekliyoruz
            if (result!=null)
            {
                return result;
            }
             _productDal.Add(product);
             return new SuccessResult(Messages.ProductAdded);
            //Cross Cuting Concerns ler(Kesişen kaygılar)
            //Validation
            //Log
            //Cache
            //Transaction
            //Auth

        }
        //key =cache verdiğimiz isim parametresiz olan örnek GetAll(),örnek =uniq olsun ,ProductManager.GetAll
        //parametreli olanlar Business.Concrete.ProductManager.GetById(1) böylede verebiliriz
        //[CacheAspect]//key,value
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 14)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            //iş kodları yapıyoruz burda
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
            
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==id));
        }
       // [CacheAspect]
        //[PerformanceAspect(5)]//çalışması 5 saniyeyi geçerse beni uyar
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId==productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
        //IProductService.Get-- IProductServicedeki tüm Get leri sil
        [CacheRemoveAspect("IProductService.Get")]//bellekteki tüm verileri içersinde Get olan ları iptal et
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            var products = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (products >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {

            //bir kategoride en fazla 15 ürün olabilir
            int products = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (products >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string productName)
        {
            var products = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (products)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
          
        }
        //eğer mevcut kategory sayısı 15 i geçtiyse  sisteme yeni ürün eklenemez
        //servisi sisteme enjekte ettik
        private IResult CheckIfCategoryLimit()
        {
            var products = _categoryService.GetAll();
            if (products.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();

        }

        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}