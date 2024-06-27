using Entities.Concrete.Dtos.Products;
using Entities.Concrete.TableModels;
using Entities.TableModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class MessageUpdateDto
    {
        public int ID { get; set; }

        public string Mesage { get; set; }


        public static MessageUpdateDto ToMessage(Message message)
        {
            MessageUpdateDto dto = new()
            {
               ID=message.Id,
               Mesage=message.Mesage,
            };
            return dto;
        }

        public static Message ToMessage(MessageUpdateDto message)
        {
            Message dto = new()
            {
                Id=message.ID,
                Mesage=message.Mesage
            };
            return dto;
        }
    }
}
