using Core.Results.Abstract;
using Entities.Concrete.Dtos.Products;
using Entities.Concrete.TableModels;

namespace Bussines.Abstract
{
    public interface IProductService
    {
        IResult Add(ProductCreateDto dto);

        IResult Update(ProductUpdateDto dto);

        IResult Delete(int  id);

        IDataResult<List<ProductDto>> GetAll();

        IDataResult<Product> GetById(int id);
    }
}
