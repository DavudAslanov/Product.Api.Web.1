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
    public class MessageCreateDto
    {
        public string Mesage { get; set; }

        public static Message ToMessage(MessageCreateDto message)
        {
            Message mesage = new Message()
            {
               Mesage = message.Mesage,
            };
            return mesage;
        }
    }
}
