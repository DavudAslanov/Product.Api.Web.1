using Bussines.Abstract;
using Bussines.BaseMessages;
using Core.Extenstion;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAcces.Abstract;
using DataAcces.Concrete;
using Entities.Concrete.Dtos.Products;
using Entities.Concrete.TableModels;
using Entities.Dtos;
using Entities.TableModels;
using FileUpload.API.Core.Utilities.Helpers.FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;

        }

        public IResult Add(CategoryCreateDto dto)
        {
            var model = CategoryCreateDto.ToCategory(dto);
            _categoryDal.Add(model);
            return new SuccessResult(UiMessage.ADD_MESSAGE);
        }

        public IResult Delete(int id)
        {
            var model = GetById(id).Data;
            model.Deleted = id;
            _categoryDal.Update(model);
            return new SuccessResult(UiMessage.DELETED_MESSAGE);
        }

        public IDataResult<List<CategoryDto>> GetAll()
        {
            var models = _categoryDal.GetAll(x => x.Deleted == 0);
            List<CategoryDto> result = new List<CategoryDto>();
            foreach (var model in models)
            {
                CategoryDto dto = new CategoryDto()
                {
                    ID = model.Id,
                    CategoryName =model.CategoryName
                };
                result.Add(dto);
            }
            return new SuccessDataResult<List<CategoryDto>>(result);
        }

        public IDataResult<Category> GetById(int id)
        {
            var model = _categoryDal.GetById(id);

            return new SuccessDataResult<Category>(model);
        }

        public IResult Update(CategoryUpdateDto dto)
        {
            var model = CategoryUpdateDto.ToCategory(dto);
            var value = GetById(dto.ID).Data;

            model.LastUpdatedDate = DateTime.Now;

            _categoryDal.Update(model);

            return new SuccessResult(UiMessage.UPDATE_MESSAGE);
        }
    }
}
