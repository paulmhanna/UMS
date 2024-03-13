using Persistence.Entities;

namespace Application.Services;

public interface ICourseService
{
    Task<Course> GetCourseById(int id);
    Task<bool> CreateCourse(Course course);
    Task<bool> UpdateCourse(int id, Course newCourse);
    Task<bool> DeleteCourse(int id);
}