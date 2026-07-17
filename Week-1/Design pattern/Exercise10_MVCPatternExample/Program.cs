using System;

namespace MVCPatternExample
{
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
    }

    public class StudentView
    {
        public void DisplayStudentDetails(string studentName, string studentId, string studentGrade)
        {
            Console.WriteLine("--- Student Details ---");
            Console.WriteLine($"ID: {studentId}");
            Console.WriteLine($"Name: {studentName}");
            Console.WriteLine($"Grade: {studentGrade}");
            Console.WriteLine("-----------------------");
        }
    }

    public class StudentController
    {
        private Student _model;
        private StudentView _view;

        public StudentController(Student model, StudentView view)
        {
            _model = model;
            _view = view;
        }

        public void SetStudentName(string name)
        {
            _model.Name = name;
        }

        public string GetStudentName()
        {
            return _model.Name;
        }

        public void SetStudentId(string id)
        {
            _model.Id = id;
        }

        public string GetStudentId()
        {
            return _model.Id;
        }

        public void SetStudentGrade(string grade)
        {
            _model.Grade = grade;
        }

        public string GetStudentGrade()
        {
            return _model.Grade;
        }

        public void UpdateView()
        {
            _view.DisplayStudentDetails(_model.Name, _model.Id, _model.Grade);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student { Id = "101", Name = "Alice", Grade = "A" };
            StudentView view = new StudentView();
            StudentController controller = new StudentController(student, view);

            Console.WriteLine("Initial View:");
            controller.UpdateView();

            Console.WriteLine("\nUpdating Student Details...");
            controller.SetStudentName("Alice Smith");
            controller.SetStudentGrade("A+");

            Console.WriteLine("Updated View:");
            controller.UpdateView();
        }
    }
}
