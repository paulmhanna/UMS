using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Context;
using Persistence.Entities;

namespace Application.Services;

public class CourseService : ICourseService
{
    private readonly MyDbContext _context;
    private readonly IMemoryCache _cache;


    public CourseService(MyDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<Course> GetCourseById(int id)
    {
        if (!_cache.TryGetValue("Course-" + id, out Course cachedCourse))
        {
            cachedCourse = await _context.Courses.Where(c => c.Id == id).FirstOrDefaultAsync();
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };
            _cache.Set("Course-" + id, cachedCourse, cacheOptions);
        }

        return cachedCourse;
    }

    public async Task<bool> CreateCourse(Course course)
    {
        try
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> UpdateCourse(int id, Course newCourse)
    {
        try
        {
            if (!_cache.TryGetValue("Course-" + id, out Course cachedCourse))
            {
                cachedCourse = await _context.Courses.Where(c => c.Id == id).FirstOrDefaultAsync();
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                };
                _cache.Set("Course-" + id, cachedCourse, cacheOptions);
            }

            if (cachedCourse == null)
            {
                return false;
            }

            cachedCourse.Name = newCourse.Name;
            cachedCourse.MaxStudentsNumber = newCourse.MaxStudentsNumber;
            cachedCourse.EnrolmentDateRange = newCourse.EnrolmentDateRange;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteCourse(int id)
    {
        if (!_cache.TryGetValue("Course-" + id, out Course cachedCourse))
        {
            cachedCourse = await _context.Courses.Where(c => c.Id == id).FirstOrDefaultAsync();
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };
            _cache.Set("Course-" + id, cachedCourse, cacheOptions);
        }
        if (cachedCourse == null)
        {
            return false;
        }

        _context.Courses.Remove(cachedCourse);
        return true;
    }
}