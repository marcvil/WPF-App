using A4.Lib.Models;
using Common.Lib.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.Lib.RepositoryInterfaces
{
    public interface IStudentRepo : IRepository<Student>
    {
        Student GetStudentByDni(string str);

    }
}
