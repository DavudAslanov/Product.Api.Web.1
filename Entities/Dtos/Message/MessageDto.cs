using Entities.Concrete.Dtos.Products;
using Entities.Concrete.TableModels;
using Entities.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class MessageDto
    {
        public int ID { get; set; }
        public string Mesage { get; set; }

        public static List<MessageDto> ToProduct(Message mesage)
        {
            MessageDto dto = new MessageDto()
            {
                ID = mesage.Id,
                Mesage = mesage.Mesage,

            };
            List<MessageDto> mesageDtos = new List<MessageDto>();
            mesageDtos.Add(dto);
            return mesageDtos;
        }
        public static Message ToProduct(MessageDto dto)
        {
            Message mesage = new Message()
            {
                Id = dto.ID,
                Mesage = dto.Mesage,
            };
            return mesage;
        }
    }
}
