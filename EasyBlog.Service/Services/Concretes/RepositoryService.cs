using AutoMapper;
using EasyBlog.Data.UnitOfWorks;

namespace EasyBlog.Service.Services.Concretes;

public abstract class RepositoryService
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;

    protected RepositoryService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
}