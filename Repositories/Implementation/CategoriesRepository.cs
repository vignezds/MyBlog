using Microsoft.EntityFrameworkCore;
using MyBlog.API.Data;
using MyBlog.API.Models.Domain;
using MyBlog.API.Repositories.Interface;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace MyBlog.API.Repositories.Implementation
{
    public class CategoriesRepository : ICategoriesRepository
    {
        public CategoriesRepository(ApplicationDBContext dBContext)
        {
            DBContext = dBContext;
        }

        public ApplicationDBContext DBContext { get; }

        public async Task<Category> CreateAsync(Category category)
        {
            await DBContext.categories.AddAsync(category);
            await DBContext.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> getCategoriesAsync()
        {
            return await DBContext.categories.ToListAsync();
        }

        public async Task<Category?> getCategoryById(Guid id)
        {
           return await DBContext.categories.FirstOrDefaultAsync(x => x.id == id); 
        }

        public async Task<Category?> updatecategory(Category category)
        {
            var existingCategory = await DBContext.categories.FirstOrDefaultAsync(x => x.id == category.id);
            if (existingCategory != null)
            {
                DBContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await DBContext.SaveChangesAsync();
                return category;
            }
            return null;
        }

        public async Task<Category?> deletecategory(Guid id)
        {
            var existingCategory = await DBContext.categories.FirstOrDefaultAsync(x => x.id == id);
            if(existingCategory != null)
            {
                DBContext.Remove(existingCategory);
                await DBContext.SaveChangesAsync();
                return existingCategory;
            }
            return null;
        }
    }
}
