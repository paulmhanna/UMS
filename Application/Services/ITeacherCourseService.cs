using Persistence.Entities;

namespace Application.Services;

public interface ITeacherCourseService
{
    Task<long> GetTeacherId(string firebaseId);
    Task<bool> CreateTeacherCourse(TeacherPerCourse course);
    Task<bool> CreateTimeSlot(DateTime startTime, DateTime endTime);
    Task<bool> CourseToTimeslot(TeacherPerCoursePerSessionTime courseToTimeslot);
}