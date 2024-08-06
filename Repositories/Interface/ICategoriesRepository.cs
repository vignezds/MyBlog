using MyBlog.API.Models.Domain;

namespace MyBlog.API.Repositories.Interface
{
    public interface ICategoriesRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> getCategoriesAsync();
        Task<Category?> getCategoryById(Guid id);
        Task<Category?> updatecategory(Category category);
        Task<Category?> deletecategory(Guid id);
    }
}
