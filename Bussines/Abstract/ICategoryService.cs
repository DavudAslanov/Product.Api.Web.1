using Core.Results.Abstract;
using Entities.Dtos;
using Entities.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Abstract
{
    public interface ICategoryService
    {
        IResult Add(CategoryCreateDto dto);

        IResult Update(CategoryUpdateDto dto);

        IResult Delete(int id);

        IDataResult<List<CategoryDto>> GetAll();

        IDataResult<Category> GetById(int id);
    }
}
