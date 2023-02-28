using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            //product namesi minimum 2 karakter olmalıdır
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();
            //unitprice 0 dan büyük olmalı
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            //içecek kategorisinin unitprice en az 10 olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //kendimiz method yazıyoruz urunismi a ile başlamalı kuralı
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("ürünler A harfi ile başlamalı");
            //WithMessage ile hata mesajı verdik

        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
