using EasyBlog.Data.UnitOfWorks;
using EasyBlog.Entity.Entities;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasyBlog.Web.Filters
{
    public class ArticleVisitorFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleVisitorFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var visitors = _unitOfWork.GetRepository<Visitor>().GetAllAsync().Result;

            var getIp = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            var getUserAgent = context.HttpContext.Request.Headers["User-Agent"];


            if (visitors.Any(v => v.IpAddress == getIp))
                return next();

            var visitor = new Visitor(getIp, getUserAgent);
            _unitOfWork.GetRepository<Visitor>().AddAsync(visitor);
            _unitOfWork.Save();
            return next();
        }
    }
}
