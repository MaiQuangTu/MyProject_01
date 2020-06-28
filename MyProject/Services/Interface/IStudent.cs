
using MyProject.Models.ModeInput;
using MyProject.Models.ModelResult;
using MyProject.Models.ViewModels;

namespace MyProject.Services.Interface
{
    public interface IStudent
    {
        PagedResult<StudentResult> GetStudent(Pageable pageable);
        PagedResult<StudentResult> GetStudentByName(string name, Pageable pageable);

        public void InsertStudent(StudentInputInsert model, int userId);
        public void UpdateStudent(StudentInputUpdate model, int userId);
    }
}
