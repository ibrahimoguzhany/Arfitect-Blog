using ArfitectBlog.Data.Abstract;
using ArfitectBlog.Entities.Concrete;
using ArfitectBlog.Entities.Dtos;
using ArfitectBlog.Services.Abstract;
using ArfitectBlog.Shared.Utilities.Results.Abstract;
using ArfitectBlog.Shared.Utilities.Results.ComplexTypes;
using ArfitectBlog.Shared.Utilities.Results.Concrete;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace ArfitectBlog.Services.Concrete
{
    public class CategoryManager :ManagerBase, ICategoryService
    {

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper) : base(mapper,unitOfWork)
        {
        }

        public async Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
        {
            var category = await UnitOfWork.Categories.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<CategoryDto>(ResultStatus.Error, "Boyle bir kategori bulunamadi.", new CategoryDto()
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = "Boyle bir kategori bulunamadi."
            });
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId)
        {
            var result = await UnitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = Mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            else
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", null);
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAllAsync()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(null);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hicbir kategori bulunamadi.", new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hicbir kategori bulunamadi."
            });

        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNoneDeletedAsync()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(x => !x.IsDeleted);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hicbir kategori bulunamadi.", new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hicbir kategori bulunamadi."
            });

        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNoneDeletedAndActiveAsync()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(x => !x.IsDeleted & x.IsActive);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hicbir kategori bulunamadi.", null);
        }

        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = Mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await UnitOfWork.Categories.AddAsync(category);

            await UnitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryAddDto.Name} adli kategori basariyla eklenmistir.", new CategoryDto
            {
                Category = addedCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{categoryAddDto.Name} adli kategori basariyla eklenmistir."
            });

        }

        public async Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var oldCategory = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            var category = Mapper.Map(categoryUpdateDto, oldCategory);
            category.ModifiedByName = modifiedByName;
            var updatedCategory = await UnitOfWork.Categories.UpdateAsync(category);
            await UnitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.Success,
                $"{categoryUpdateDto.Name} adli kategori basariyla guncellenmistir.", new CategoryDto()
                {
                    Category = updatedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{categoryUpdateDto.Name} adli kategori basariyla eklenmistir."
                });
        }

        public async Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName)
        {
            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await UnitOfWork.Categories.UpdateAsync(category);
                await UnitOfWork.SaveAsync();

                return new DataResult<CategoryDto>(ResultStatus.Success,
                    $"{deletedCategory.Name} adli kategori basariyla silinmistir.", new CategoryDto()
                    {
                        Category = deletedCategory,
                        ResultStatus = ResultStatus.Success,
                        Message = $"{deletedCategory.Name} adli kategori basariyla silinmistir."
                    });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", new CategoryDto()
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir kategori bulunamadı."
            });
        }

        public async Task<IResult> HardDeleteAsync(int categoryId)
        {
            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await UnitOfWork.Categories.DeleteAsync(category);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} adli kategori basariyla silinmistir.");
            }
            return new Result(ResultStatus.Error, "Boyle bir kategori bulunamadi.");

        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var categoriesCount = await UnitOfWork.Categories.CountAsync();
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var categoriesCount = await UnitOfWork.Categories.CountAsync(c=>!c.IsDeleted);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }
    }
}
