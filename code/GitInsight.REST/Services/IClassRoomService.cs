public interface IClassroomService    
{
    bool AddStudent(Student student);
    IEnumerable<Student> GetAllStudents();    
}