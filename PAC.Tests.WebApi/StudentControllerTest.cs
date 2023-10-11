﻿namespace PAC.Tests.WebApi;
using System.Collections.ObjectModel;

using System.Data;
using Moq;
using PAC.IBusinessLogic;
using PAC.Domain;
using PAC.WebAPI;
using Microsoft.AspNetCore.Mvc;

[TestClass]
public class StudentControllerTest
{
    [TestClass]
    public class UsuarioControllerTest
    {
        [TestInitialize]
        public void InitTest()
        {
        }

        [TestMethod]
        public void GetOkTest()
        {
            Student oneStudent = new Student();
            Student secondStudent = new Student();
            List<Student> students = new List<Student>();
            students.Add(oneStudent);
            students.Add(secondStudent);

            var studentServiceMock = new Mock<IStudentLogic>(MockBehavior.Strict);
            studentServiceMock.Setup(s => s.GetStudents()).Returns(students);
            var studentController = new StudentController(studentServiceMock.Object);

            var result = studentController.Get();
            var okResult = result as OkObjectResult;
            var value = okResult.Value;
            IEnumerable<Student> valueEnumerable = value as IEnumerable<Student>;
            List<Student> studentList = valueEnumerable.ToList();
            studentServiceMock.VerifyAll();
            Assert.IsTrue(students.SequenceEqual(studentList));
        }

        [TestMethod]
        public void GetIdOkTest()
        {
            Student oneStudent = new Student();

            var studentServiceMock = new Mock<IStudentLogic>(MockBehavior.Strict);
            studentServiceMock.Setup(s => s.GetStudentById(It.IsAny<int>())).Returns(oneStudent);
            var studentController = new StudentController(studentServiceMock.Object);

            var result = studentController.Get(oneStudent.Id);
            var okResult = result as ObjectResult;
            var value = okResult.Value as Student;
            studentServiceMock.VerifyAll();
            Assert.AreEqual(value, oneStudent);
        }
    }
}
