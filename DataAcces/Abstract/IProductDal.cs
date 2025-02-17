﻿using Core.DataAcces.Abstract;
using Entities.Concrete.Dtos.Products;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Abstract
{
    public interface IProductDal:IBaseInterfeys<Product>
    {
        List<ProductDto> GetCategories();
    }
}
