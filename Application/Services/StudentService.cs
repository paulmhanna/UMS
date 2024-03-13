using Application.Commands;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;

namespace Application.Services;

public class StudentService : IStudentService
{
    private readonly MyDbContext _context;

    public StudentService(MyDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> EnrollStudent(ClassEnrollment ce)
    {
        var existingCourse = await (from teacherCourse in _context.TeacherPerCourses
            join course in _context.Courses on teacherCourse.CourseId equals course.Id
            where teacherCourse.Id == ce.ClassId
            select course).FirstOrDefaultAsync();
        
        if (existingCourse == null)
            return false;

        var currentDate = DateTime.Now.Date;
        var currentDateOnly = new DateOnly(currentDate.Year, currentDate.Month, currentDate.Day);

        if (currentDateOnly < existingCourse.EnrolmentDateRange.Value.LowerBound || currentDateOnly > existingCourse.EnrolmentDateRange.Value.UpperBound)
            return false;

        var classEnrollment = new ClassEnrollment
        {
            ClassId = ce.ClassId,
            StudentId = ce.StudentId
        };

        _context.ClassEnrollments.Add(classEnrollment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<long> GetStudentId(string firebaseId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.FirebaseId == firebaseId);
        return user.Id;
    }
}