using Common.Lib.Core;
using Common.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.Lib.Models
{
    public class Exam : Entity
    {
        public double FinalMark { get; set; }

        public string Title { get; set; }
        public Student Student
        {
            get
            {
                var repo = Student.DepCon.Resolve<IRepository<Student>>();
                var studentList = repo.QueryAll().ToList();

                 return studentList.First(x => x.Id == StudentId);
            }
        }
        public Guid StudentId { get; set; }
        public Subject Subject 
        {
             get
             {
                var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
                var subjectList = repo.QueryAll().ToList();

                return subjectList.First(x => x.Id == SubjectId);
             }
        }
        public Guid SubjectId { get; set; }

        public Exam()
        {

        }
        public Exam(double finalMark, Guid student, Guid subject, String title)
        {

            this.FinalMark = finalMark;
            this.StudentId = student;
            this.SubjectId = subject;
            this.Title = title;
        }

        #region static Validations
        public static ValidationResult<double> ValidateFinalMark(string finalmark)
        {
            ValidationResult<double> tempfinalMark = new ValidationResult<double>();

            tempfinalMark.ValidationSuccesful = true;

            #region Check null or empty
            if (string.IsNullOrEmpty(finalmark))
            {
                tempfinalMark.ValidationSuccesful = false;
                tempfinalMark.Messages.Add("finalmark null or empty.");
            }
            #endregion

            #region format

            double doublevar;
            bool conversionvalid = double.TryParse(finalmark, out doublevar);
            if (!conversionvalid)
            {
                tempfinalMark.ValidationSuccesful = false;
                tempfinalMark.Messages.Add("finalmark conversion failed");
            }
            #endregion

            #region check if between 0 and 10

           
            if (doublevar>10 || doublevar <0)
            {
                tempfinalMark.ValidationSuccesful = false;
                tempfinalMark.Messages.Add("Final mark has to be a value between 0 and 10");
            }
            #endregion

            if (tempfinalMark.ValidationSuccesful == true)
            {
                tempfinalMark.ValidatedResult = doublevar;
            }

            return tempfinalMark;
        }

        public static ValidationResult<string> ValidateTitle(string title)
        {
            ValidationResult<string> tempTitle = new ValidationResult<string>();

            tempTitle.ValidationSuccesful = true;

            #region Check null or empty
            if (string.IsNullOrEmpty(title))
            {
                tempTitle.ValidationSuccesful = false;
                tempTitle.Messages.Add("Title null or empty");
            }
            #endregion

           

            if (tempTitle.ValidationSuccesful == true)
            {
                tempTitle.ValidatedResult = title;
            }

            return tempTitle;
        }



        public static ValidationResult<Student> ValidateStudent(string dniNumber, Guid currentId = default)
        {
            ValidationResult<Student> tempIdStudent = new ValidationResult<Student>();

            tempIdStudent.ValidationSuccesful = true;

            #region Check null or empty
            if (string.IsNullOrEmpty(dniNumber))
            {
                tempIdStudent.ValidationSuccesful = false;
                tempIdStudent.Messages.Add("dninumber null or empty.");
            }
            #endregion

            if (tempIdStudent.ValidationSuccesful == true)
            {
                var st = new Student();
                var repo = Student.DepCon.Resolve<IRepository<Student>>();
                var studentList = repo.QueryAll().ToList();

                st = studentList.First(x => x.Dni == dniNumber);


                tempIdStudent.ValidatedResult = st;
            }

            return tempIdStudent;
        }

        public static ValidationResult<Subject> ValidateSubject(string subjectCode, Guid currentId = default)
        {
            ValidationResult<Subject> tempSubjectId = new ValidationResult<Subject>();

            tempSubjectId.ValidationSuccesful = true;

            #region Check null or empty
            if (string.IsNullOrEmpty(subjectCode))
            {
                tempSubjectId.ValidationSuccesful = false;
                tempSubjectId.Messages.Add("dninumber null or empty.");
            }
            #endregion
            
            if (tempSubjectId.ValidationSuccesful == true)
            {
                Subject subj = new Subject();
                var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
                var subjectList = repo.QueryAll().ToList();

                subj = subjectList.First(x => x.SubjectCode == subjectCode);

                
                tempSubjectId.ValidatedResult = subj;

            }
            
            return tempSubjectId;
        }
        #endregion

        #region Domain Validations
        public void ValidateFinalMark(ValidationResult valResult)
        {
            var finalMarkvalidation = ValidateFinalMark(this.FinalMark.ToString());
            if (finalMarkvalidation.ValidationSuccesful == false)
            {
                valResult.ValidationSuccesful = false;
                valResult.Messages.AddRange(finalMarkvalidation.Messages);
            }
        }
        public void ValidateTitle(ValidationResult valResult)
        {
            var titlevalidation = ValidateTitle(this.Title);
            if (titlevalidation.ValidationSuccesful == false)
            {
                valResult.ValidationSuccesful = false;
                valResult.Messages.AddRange(titlevalidation.Messages);
            }
        }

        public void ValidateStudent(ValidationResult valResult)
        {
            var studentValidation = ValidateStudent(this.Student.Dni);
            if (studentValidation.ValidationSuccesful == false)
            {
                valResult.ValidationSuccesful = false;
                valResult.Messages.AddRange(studentValidation.Messages);
            }
        }
        public void ValidateSubject(ValidationResult valResult)
        {
            var subjectValidation = ValidateSubject(this.Subject.SubjectCode);
            if (subjectValidation.ValidationSuccesful == false)
            {
                valResult.ValidationSuccesful = false;
                valResult.Messages.AddRange(subjectValidation.Messages);
            }
        }
        #endregion


        public SaveValidation<Exam> Save()
        {
            var saveResult = base.Save<Exam>();
            return saveResult;
        }


        public override ValidationResult Validate()
        {
            var output = base.Validate();
            // check if guid is available. 
            //If not, it means that the Id we are checking is used by this subject, so we need to update the info
            ValidateFinalMark(output);
            ValidateTitle(output);
            ValidateStudent(output);
            ValidateSubject(output);

            return output;
        }
        /*
        public override Repository<T> GetRepo<T>()
        {
            var output = new ExamRepository();

            return output as Repository<T>;
        }
        public ExamRepository GetExamRepo()
        {

            return GetRepo<Exam>() as ExamRepository;
        }
        */
    }
}
