using A4.Lib.Models;
using Common.Lib.Core;
using Common.Lib.DAL.EFCore;
using Common.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using A4.DAL.Repositories;
using A4.DAL;

using A4.Lib.RepositoryInterfaces;
using ASPNET_Application.DbContextFactory;

namespace WpfApp1.AppBootsraper
{
    public class Bootstraper
    {
        public IDependencyContainer Init()
        {
            var depCon = new SimpleDependencyContainer();

            RegisterRepositories(depCon);

            Entity.DepCon = depCon;

            return depCon;
        }

        public void RegisterRepositories(IDependencyContainer depCon)
        {
            var studentRepoBuilder = new Func<object[], object>((parameters) =>
            {
                return new StudentRepository(GetDbConstructor());
            });
            var subjectRepoBuilder = new Func<object[], object>((parameters) =>
            {
                return new EfCoreRepository<Subject>(GetDbConstructor());
            });
            var examRepoBuilder = new Func<object[], object>((parameters) =>
            {
                return new EfCoreRepository<Exam>(GetDbConstructor());
            });

            depCon.Register<IRepository<Student>, StudentRepository>(studentRepoBuilder);
            depCon.Register<IStudentRepo, StudentRepository>((parameters) => new StudentRepository(GetDbConstructor()));

            depCon.Register<IRepository<Subject>, EfCoreRepository<Subject>>(subjectRepoBuilder);
            depCon.Register<IRepository<Exam>, EfCoreRepository<Exam>>(examRepoBuilder);
        }
        private static AcademyDbContext GetDbConstructor()
        {
            var factory = new AcademyDbContextFactory();
            return factory.CreateDbContext(null);
        }
    }
}
