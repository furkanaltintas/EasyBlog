using AutoMapper;
using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.Entities;
using EasyBlog.Service.Services.Abstractions;
using EasyBlog.Service.Services.Managers;

namespace EasyBlog.Service.Services.Concretes;

public class DashboardService : RepositoryService, IDashboardService
{
    public DashboardService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }


    public async Task<List<int>> GetYearlyArticleCounts()
    {
        var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync(a => !a.IsDeleted);

        var startDate = DateTime.Now.Date;
        startDate = new DateTime(startDate.Year, 1, 1); // 1.1.2025

        List<int> datas = new();

        for (int i = 1; i <= 12; i++)
        {
            var startedDate = new DateTime(startDate.Year, i, 1);
            var endedDate = startedDate.AddMonths(1);
            var data = articles.Where(a => a.CreatedDate >= startedDate && a.CreatedDate < endedDate).Count();
            datas.Add(data);
        }

        return datas;
    }

    public async Task<int> GetTotalArticleCount() =>
        await _unitOfWork.GetRepository<Article>().CountAsync();

    public async Task<int> GetTotalCategoryCount() =>
        await _unitOfWork.GetRepository<Category>().CountAsync();

    public async Task<int> GetTotalUserCount() =>
        await _unitOfWork.GetRepository<AppUser>().CountAsync();
}