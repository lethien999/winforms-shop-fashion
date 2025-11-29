using System.Collections.Generic;
using WinFormsFashionShop.DTO;

namespace WinFormsFashionShop.Business.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllCategories();
        CategoryDTO? GetCategoryById(int id);
        void CreateCategory(CreateCategoryDTO category);
        void UpdateCategory(UpdateCategoryDTO category);
        void DeleteCategory(int id);
        IEnumerable<CategoryDTO> GetActiveCategories();
    }
}

