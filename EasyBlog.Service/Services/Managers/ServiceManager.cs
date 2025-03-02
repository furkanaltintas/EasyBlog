﻿using EasyBlog.Service.Services.Abstractions;

namespace EasyBlog.Service.Services.Managers;

public class ServiceManager : IServiceManager
{
    public ServiceManager(
        IAuthService authService,
        IArticleService articleService,
        ICategoryService categoryService)
    {
        AuthService = authService;
        ArticleService = articleService;
        CategoryService = categoryService;
    }

    public IAuthService AuthService { get; }

    public IArticleService ArticleService { get; }

    public ICategoryService CategoryService { get; }
}
