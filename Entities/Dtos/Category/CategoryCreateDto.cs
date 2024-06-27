using Entities.TableModels;

namespace Entities.Dtos
{
    public class CategoryCreateDto
    {
        public string CategoryName { get; set; }

        public static Category ToCategory(CategoryCreateDto dto)
        {
            Category category = new()
            {
                CategoryName = dto.CategoryName,
            };
            return category;
        }
    }
}
