public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _categoryRepository;

    public CategoryService(IGenericRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task<Category> CreateAsync(Category category)
    {
        await _categoryRepository.AddAsync(category);
        await _categoryRepository._context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(int id, Category category)
    {
        var existing = await _categoryRepository.GetByIdAsync(id);
        if (existing == null) return null;

        existing.Name = category.Name;
        existing.Description = category.Description;
        existing.CreatedAt = category.CreatedAt;

        _categoryRepository.Update(existing);
        await _categoryRepository._context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return false;

        _categoryRepository.Remove(category);
        await _categoryRepository._context.SaveChangesAsync();
        return true;
    }
}