using Common.Lib.Core;
using Common.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.Lib.Models
{
    public class Subject : Entity
    {
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }

        public Subject()
        {

        }
        public Subject(string SubjectCode, string subjectName)
        {
            this.SubjectCode = SubjectCode;
            this.SubjectName = subjectName;

        }

        #region static validations
        public static ValidationResult<string> ValidateSubjectName(string subjectName)
        {
            ValidationResult<string> tempsubjectName = new ValidationResult<string>();

            tempsubjectName.ValidationSuccesful = true;

            #region Check null or empty
            if (string.IsNullOrEmpty(subjectName))
            {
                tempsubjectName.ValidationSuccesful = false;
                tempsubjectName.Messages.Add("subjectName null or empty.");
            }
            #endregion

            #region Check if exists
            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
            var subjectNameList = repo.QueryAll().FirstOrDefault(x => x.SubjectName == subjectName);

            if (subjectName != default)
            {
                tempsubjectName.ValidationSuccesful = false;
                tempsubjectName.Messages.Add("Ya existe una asignatura con este nombre");

            }
            #endregion

            if (tempsubjectName.ValidationSuccesful == true)
            {
                tempsubjectName.ValidatedResult = subjectName;
            }

            return tempsubjectName;
        }

        public static ValidationResult<string> ValidateIdSubject(string subjectCode)
        {
            ValidationResult<string> tempIdSubject = new ValidationResult<string>();

            tempIdSubject.ValidationSuccesful = true;

            #region Check null or empty
            if (string.IsNullOrEmpty(subjectCode))
            {
                tempIdSubject.ValidationSuccesful = false;
                tempIdSubject.Messages.Add("subjectName null or empty.");
            }
            #endregion

            #region Check if exists
            var repo = Subject.DepCon.Resolve<IRepository<Subject>>();
            var subjectCodeList = repo.QueryAll().FirstOrDefault(x => x.SubjectCode == subjectCode);

            if (subjectCode != default)
            {
                tempIdSubject.ValidationSuccesful = false;
                tempIdSubject.Messages.Add("Ya existe una asignatura con este codigo");

            }
            #endregion

            if (tempIdSubject.ValidationSuccesful == true)
            {
                tempIdSubject.ValidatedResult = subjectCode;
            }

            return tempIdSubject;
        }
        #endregion


        #region Domain Validations
        public void ValidateSubjectName(ValidationResult valResult)
        {
            var subjectNamevalidation = ValidateSubjectName(this.SubjectName);
            if (subjectNamevalidation.ValidationSuccesful == false)
            {
                valResult.ValidationSuccesful = false;
                valResult.Messages.AddRange(subjectNamevalidation.Messages);
            }
        }

        public void ValidateIdSubject(ValidationResult valResult)
        {
            var idSubjectvalidation = ValidateIdSubject(this.SubjectCode);
            if (idSubjectvalidation.ValidationSuccesful == false)
            {
                valResult.ValidationSuccesful = false;
                valResult.Messages.AddRange(idSubjectvalidation.Messages);
            }
        }

        public SaveValidation<Subject> Save()
        {
            var saveResult = base.Save<Subject>();
            return saveResult;
        }
        #endregion

        public override ValidationResult Validate()
        {
            var output = base.Validate();

            ValidateIdSubject(this.SubjectCode);
            ValidateSubjectName(this.SubjectName);

            return output;
        }
        /*
        public override Repository<T> GetRepo<T>()
        {
            var output = new SubjectRepository();

            return output as Repository<T>;
        }
        public SubjectRepository GetSubjectRepo()
        {

            return GetRepo<Subject>() as SubjectRepository;
        }
        */
    }
}