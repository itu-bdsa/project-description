public class ClassroomService : IClassroomService{
    private IList<Student> _students;public ClassroomService()
    {
        _students = new List<Student>();
    }public bool AddStudent(Student student)
    {
        if(student != null)
        {
            _students.Add(student);
            return true;
        }        return false;
     }public IEnumerable<Student> GetAllStudents()
    {
        return _students;
    }
}