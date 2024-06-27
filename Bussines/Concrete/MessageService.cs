using Bussines.Abstract;
using Bussines.BaseMessages;
using Core.Extenstion;
using Core.Results.Abstract;
using Core.Results.Concrete;
using DataAcces.Abstract;
using DataAcces.Concrete;
using Entities.Concrete.Dtos.Products;
using Entities.Dtos;
using Entities.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageService(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public IResult Add(MessageCreateDto dto)
        {
            var model = MessageCreateDto.ToMessage(dto);
            _messageDal.Add(model);
            return new SuccessResult(UiMessage.ADD_MESSAGE);
        }

        public IResult Delete(int id)
        {
            var model = GetById(id).Data;
            model.Deleted = id;
            _messageDal.Update(model);
            return new SuccessResult(UiMessage.DELETED_MESSAGE);
        }

        public IDataResult<List<MessageDto>> GetAll()
        {
            var models = _messageDal.GetAll(x => x.Deleted == 0);
            List<MessageDto> result = new List<MessageDto>();
            foreach (var model in models)
            {
                MessageDto dto = new MessageDto()
                {
                    ID = model.Id,
                    Mesage = model.Mesage,
                };
                result.Add(dto);
            }
            return new SuccessDataResult<List<MessageDto>>(result);
        }

        public IDataResult<Message> GetById(int id)
        {
            var model = _messageDal.GetById(id);

            return new SuccessDataResult<Message>(model);
        }

        public IResult Update(MessageUpdateDto dto)
        {
            var model = MessageUpdateDto.ToMessage(dto);
            var value = GetById(dto.ID).Data;

            model.LastUpdatedDate = DateTime.Now;

            _messageDal.Update(model);

            return new SuccessResult(UiMessage.UPDATE_MESSAGE);
        }
    }
}
