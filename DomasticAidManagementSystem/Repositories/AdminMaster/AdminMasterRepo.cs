using DomasticAidManagementSystem.Models.AdminMaster;
using DomasticAidManagementSystem.Models.Categories;
using DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb;
using IIITS.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DomasticAidManagementSystem
{
    public class AdminMasterRepo : IAdminMasterRepo
    {
        private readonly LMSMasterServiceDBContext _dbContext;

        public AdminMasterRepo(LMSMasterServiceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DashBoard> getCountDetails()
        {
            var categoryCount = await _dbContext.Categories.CountAsync();
            var subCategoryCount = await _dbContext.SubCategories.CountAsync();
            //var categoryMappingCount = await _dbContext.CategoryMappings.CountAsync();
            return new DashBoard
            {
                CategoryCount = categoryCount,
                CategoryMapCount = subCategoryCount,
                SubCategoryCount = subCategoryCount,
            };
        }

        public async Task<DashBoard> AddCategoryMap(string categoryName)
        {
             var category = new CategoryDbType { CategoryName = categoryName };
            var details = await _dbContext.AddAsync(category);
            return new DashBoard
            {
                Status = 1
            };
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var details = await _dbContext.Categories.ToListAsync();
            return details.Select(category => new Category
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            }).ToList();
        }

        public async Task<IEnumerable<Team>> LoadTeams()
        {
            var details = await _dbContext.Teams.ToListAsync();
            return details.Select(category => new Team
            {
                TeamId = category.TeamId ,
                TeamName = category.TeamName,
            }).ToList();
        }

        public async Task<IEnumerable<Employee>> LoadEmployee()
        {
            var details = await _dbContext.Employees.ToListAsync();
            return details.Select(category => new Employee
            {
                EmpId = category.EmpId,
                EmpName = category.EmpName,
                 EmpAadharNumber= category.EmpAadharNumber,
                  EmpPhoneNumber= category.EmpPhoneNumber
            }).ToList();
        }


        public async Task<IEnumerable<SubCategory>> GetSubCategories(int categoryId)
        {
            try
            {
                var details = await _dbContext.SubCategories
                                  .ToListAsync();

                var categories = await _dbContext.Categories.ToDictionaryAsync(c => c.CategoryId, c => c.CategoryName);
                var uoms = await _dbContext.UnitOfMeasures.ToDictionaryAsync(c => c.UnitId, c => c.UnitName);

                return details.Select(category => new SubCategory
                {
                    SubCategoryId = category.SubCategoryId,
                    SubCategoryName = category.SubCategoryName,
                    BasePrice = category.BasePrice,
                    CategoryId = category.CategoryId,
                    MainCategoryName = categories.ContainsKey(category.CategoryId) ? categories[category.CategoryId] : "Unknown",
                    UnitOfMeasureName = categories.ContainsKey(category.CategoryId) ? uoms[category.CategoryId] : "Unknown"
                }).ToList();
            
                   }
       catch(Exception ex)
       {
           return null;
       }
}

        public async Task<IEnumerable<UnitOfMeasure>> GetAllUom()
        {
            try
            {
                var details = await _dbContext.UnitOfMeasures.ToListAsync();
                return details.Select(category => new UnitOfMeasure
                {
                    UnitId = category.UnitId,
                    UnitName = category.UnitName,
                }).ToList();
            }
            catch(Exception ec)
            {
                return null;
            }
        }

        public async Task<DashBoard> AddMainCategory(string categoryName)
        {
            var category = new CategoryDbType { CategoryName = categoryName };
            var details = await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return new DashBoard
            {
                Status = 1
            };
        }

        public async Task<DashBoard> AddTeam(string teamName)
        {
            try
            {
                var team = new TeamDbType { TeamName = teamName };
                var details = await _dbContext.AddAsync(team);
                await _dbContext.SaveChangesAsync();
                return new DashBoard
                {
                    Status = 1
                };
            }
            catch(Exception ex)
            {
                return new DashBoard();
            }
        }

        public async Task<DashBoard> AddEmployees(Employee employee)
        {
            try
            {
                var empDbType = new EmployeeDbType { EmpName = employee.EmpName, EmpTeamId = employee.EmpTeamId, EmpPhoneNumber = employee.EmpPhoneNumber, EmpAadharNumber= employee.EmpAadharNumber };
                await _dbContext.Employees.AddAsync(empDbType);
                await _dbContext.SaveChangesAsync();
                return new DashBoard
                {
                    Status = 1
                };
            }
            catch(Exception ex)
            {
                return new DashBoard
                {
                    Status = -1
                };
            }
        }




        public async Task<DashBoard> UpdateSubCategory(int subCategoryId, int categoryId, string subCategoryName, int uomId, decimal BasePrice)
        {
            try
            {
                var existingSubCategory = await _dbContext.SubCategories.Where(x => x.SubCategoryId == subCategoryId).FirstOrDefaultAsync();
                var subCategory = new SubCategoryDbType { CategoryId = categoryId, SubCategoryName = subCategoryName, UnitOfMeasureId = existingSubCategory.UnitOfMeasureId, BasePrice = BasePrice };

                existingSubCategory.CategoryId = categoryId;
                existingSubCategory.SubCategoryName = subCategoryName;
                existingSubCategory.UnitOfMeasureId = uomId;  
                existingSubCategory.BasePrice = BasePrice;
                await _dbContext.SaveChangesAsync();
                return new DashBoard
                {
                    Status = 1
                };
            }
            catch (Exception ex)
            {
                return new DashBoard
                {
                    Status = -1
                };
            }
        }

        public async Task<DashBoard> AddSubCategory(int categoryId, string subCategoryName, int uomId, decimal BasePrice)
        {
            try
            {
                var subCategory = new SubCategoryDbType { CategoryId = categoryId, SubCategoryName = subCategoryName, UnitOfMeasureId = uomId, BasePrice = BasePrice };
                await _dbContext.SubCategories.AddAsync(subCategory);
                await _dbContext.SaveChangesAsync();
                return new DashBoard
                {
                    Status = 1
                };
            }
            catch (Exception ex)
            {
                return new DashBoard
                {
                    Status = -1
                };
            }
        }
    }
}
