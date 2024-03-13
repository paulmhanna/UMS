using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

public static class ApplicationConfiguration
{
    public static void AddApplicationConfiguration(this IServiceCollection serviceCollection )
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<ICourseService,CourseService>();
        serviceCollection.AddScoped<ITeacherCourseService,TeacherCourseService>();
        serviceCollection.AddScoped<IStudentService,StudentService>();
        serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    }
}