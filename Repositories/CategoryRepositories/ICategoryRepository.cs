﻿using Project05_DapperNorthwind.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project05_DapperNorthwind.Repositories.CategoryRepositories
{
    public  interface ICategoryRepository
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CreateCategoryDto categoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task<GetByIdCategoryDto> GetByIdCategoryAsync(int id);

    }
}