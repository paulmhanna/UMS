using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;

namespace Application.Services;

public class TeacherCourseService : ITeacherCourseService
{
    private readonly MyDbContext _context;

    public TeacherCourseService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<long> GetTeacherId(string firebaseId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.FirebaseId == firebaseId);
        return user.Id;
    }
    
    public async Task<bool> CreateTeacherCourse(TeacherPerCourse course)
    {
        try
        {
            var teacherCourse = new TeacherPerCourse
            {
                TeacherId = course.TeacherId,
                CourseId = course.CourseId,
            };
            _context.TeacherPerCourses.Add(teacherCourse);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> CreateTimeSlot(DateTime startTime, DateTime endTime)
    {
        try
        {
            var timeslot = new SessionTime
            {
                StartTime = startTime,
                EndTime = endTime
            };
            _context.SessionTimes.Add(timeslot);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> CourseToTimeslot(TeacherPerCoursePerSessionTime courseToTimeslot)
    {
        try
        {
            var courseTimeslot = new TeacherPerCoursePerSessionTime
            {
                TeacherPerCourseId = courseToTimeslot.TeacherPerCourseId,
                SessionTimeId = courseToTimeslot.SessionTimeId,
                
            };
            _context.TeacherPerCoursePerSessionTimes.Add(courseTimeslot);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}