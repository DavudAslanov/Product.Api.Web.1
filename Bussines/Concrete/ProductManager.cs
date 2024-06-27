using Bussines.Abstract;
using Bussines.BaseMessages;
using Core.Extenstion;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAcces.Abstract;
using Entities.Concrete.Dtos.Products;
using Entities.Concrete.TableModels;
using FileUpload.API.Core.Utilities.Helpers.FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IFileHelper _fileHelper;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(ProductCreateDto dto)
        {
            var model = ProductCreateDto.ToProduct(dto);
            string webrootpath = "";
            model.Photo = PictureHelper.UploadImage(dto.Photo,webrootpath);

            _productDal.Add(model);
            return new SuccessResult(UiMessage.ADD_MESSAGE);
        }

        public IResult Delete(int id)
        {
            var model = GetById(id).Data;
            model.Deleted = id;
            _productDal.Update(model);
            return new SuccessResult(UiMessage.DELETED_MESSAGE);
        }

        public IDataResult<List<ProductDto>> GetAll()
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetCategories());
        }

        public IDataResult<Product> GetById(int id)
        {
            var model = _productDal.GetById(id);

            return new SuccessDataResult<Product>(model);
        }

        public IResult Update(ProductUpdateDto dto)
        {
            var model = ProductUpdateDto.Toproduct(dto);
            var value = GetById(dto.Id).Data;
            string webrootpath = "";
            if (model.Photo == null)
            {
                model.Photo = value.Photo;
            }
            else
            {
                model.Photo = PictureHelper.UploadImage(dto.Photo, webrootpath);
            }

            model.LastUpdatedDate = DateTime.Now;

            _productDal.Update(model);

            return new SuccessResult(UiMessage.UPDATE_MESSAGE);
        }
    }
}
