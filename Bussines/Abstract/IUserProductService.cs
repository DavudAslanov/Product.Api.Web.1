using Core.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Abstract
{
    public interface IUserProductService
    {
        ServiceResult GetSelectedProducts(int userId);

        ServiceResult SelectProduct(int userId, int productId); 
        //ServiceResult SelectProduct(Guid userId, int productId);
        ServiceResult DeselectProduct(int userId, int productId);
    }
}
