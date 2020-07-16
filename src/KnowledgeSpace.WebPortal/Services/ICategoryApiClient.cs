﻿using KnowledgeSpace.ViewModels.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeSpace.WebPortal.Services
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryVm>> GetCategories(int take);
        Task<CategoryVm> GetCategoryById(int id);
    }
}