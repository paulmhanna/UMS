using Persistence.Entities;

namespace Application.Services;

public interface IStudentService
{
    Task<bool> EnrollStudent(ClassEnrollment ce);
    Task<long> GetStudentId(string firebaseId);
    
}