using Entities.TableModels;

namespace Entities.Dtos
{
    public class CategoryUpdateDto
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }

        public static CategoryUpdateDto ToCategory(Category category)
        {
            CategoryUpdateDto dto = new()
            {
                ID = category.Id,
                CategoryName = category.CategoryName,
            };
            return dto;
        }
        public static Category ToCategory(CategoryUpdateDto dto)
        {
            Category category = new Category()
            {
                Id = dto.ID,
                CategoryName = dto.CategoryName,
            };
            return category;
        }
    }
}
