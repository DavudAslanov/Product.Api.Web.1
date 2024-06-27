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
    public interface IMessageService
    {
        IResult Add(MessageCreateDto dto);

        IResult Update(MessageUpdateDto dto);

        IResult Delete(int id);

        IDataResult<List<MessageDto>> GetAll();

        IDataResult<Message> GetById(int id);
    }
}
