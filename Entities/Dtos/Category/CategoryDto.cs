using Entities.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CategoryDto
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }

        public static List<CategoryDto> ToPosition(Category category)
        {
            CategoryDto dto = new CategoryDto()
            {
                ID = category.Id,
                CategoryName = category.CategoryName,
            };
            List<CategoryDto> dtoList = new List<CategoryDto>();
            dtoList.Add(dto);
            return dtoList;
        }
    }
}
