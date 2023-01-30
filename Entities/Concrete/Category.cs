using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Category:IEntity
    {

        //cıplak class kalmasın ÇCK
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
