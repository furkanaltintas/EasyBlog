﻿namespace EasyBlog.Service.Services.Abstractions;

public interface IDashboardService
{
    Task<List<int>> GetYearlyArticleCounts();
    Task<int> GetTotalArticleCount();
    Task<int> GetTotalCategoryCount();
    Task<int> GetTotalUserCount();
}