using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using wpf.Controllers;
using wpf.Model;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        Core db = new Core();

        // Тест метода получения запланированных записей
        [TestMethod]
        public void TestGetPlanedAppointments()
        {
            // Arrange
            int idUser = 1;
            List<Appointments> planedAppointments = db.context.Appointments.Where(x => x.IdPatient == idUser && x.Date >= DateTime.Now).ToList();
            AppoitmentsController appoitmentsController = new AppoitmentsController();
            // Act
            List<Appointments> planedAppointmentsRes = appoitmentsController.GetPlanedAppointments(idUser);

            // Assert
            Assert.AreEqual(planedAppointments.Count, planedAppointmentsRes.Count);
            for (int i = 0; i < planedAppointments.Count; i++)
            {
                Assert.AreEqual(planedAppointments[i].IdAppointment, planedAppointmentsRes[i].IdAppointment);
            }
        }
        // Тест метода получения истории записей
        [TestMethod]
        public void TestGetHistoryAppointments()
        {
            // Arrange
            int idUser = 1;
            List<Appointments> historyAppointments = db.context.Appointments.Where(x => x.IdPatient == idUser && x.Date < DateTime.Now).ToList();
            AppoitmentsController appoitmentsController = new AppoitmentsController();
            // Act
            List<Appointments> historyAppointmentssRes = appoitmentsController.GetHistoryAppointments(idUser);

            // Assert
            Assert.AreEqual(historyAppointments.Count, historyAppointmentssRes.Count);
            for (int i = 0; i < historyAppointments.Count; i++)
            {
                Assert.AreEqual(historyAppointments[i].IdAppointment, historyAppointmentssRes[i].IdAppointment);
            }
        }
        // Тест метода получения истории записей для врача
        [TestMethod]
        public void TestGetHistoryAppointmentsForDoctor()
        {
            // Arrange
            int idDoctor = 1;
            List<Appointments> historyAppointmentsForDoctor = db.context.Appointments.Where(x => x.IdDoctor == idDoctor && x.Date < DateTime.Now).ToList();
            AppoitmentsController appoitmentsController = new AppoitmentsController();
            // Act
            List<Appointments> historyAppointmentsForDoctorRes = appoitmentsController.GetHistoryAppointmentsForDoctor(idDoctor);

            // Assert
            Assert.AreEqual(historyAppointmentsForDoctor.Count, historyAppointmentsForDoctorRes.Count);
            for (int i = 0; i < historyAppointmentsForDoctor.Count; i++)
            {
                Assert.AreEqual(historyAppointmentsForDoctor[i].IdAppointment, historyAppointmentsForDoctorRes[i].IdAppointment);
            }
        }
        // Тест метода получения запланированных записей для врача
        [TestMethod]
        public void TestGetPlanedAppointmentsForDoctor()
        {
            // Arrange
            int idDoctor = 1;
            List<Appointments> planedAppointmentsForDoctor = db.context.Appointments.Where(x => x.IdDoctor == idDoctor && x.Date >= DateTime.Now).ToList();
            AppoitmentsController appoitmentsController = new AppoitmentsController();
            // Act
            List<Appointments> planedAppointmentsForDoctorRes = appoitmentsController.GetPlanedAppointmentsForDoctor(idDoctor);

            // Assert
            Assert.AreEqual(planedAppointmentsForDoctor.Count, planedAppointmentsForDoctorRes.Count);
            for (int i = 0; i < planedAppointmentsForDoctor.Count; i++)
            {
                Assert.AreEqual(planedAppointmentsForDoctor[i].IdAppointment, planedAppointmentsForDoctorRes[i].IdAppointment);
            }
        }
        // Тест метода получения записи
        [TestMethod]
        public void TestGetAppointment()
        {
            // Arrange
            int idAppointment = 10;
            AppoitmentsController appoitmentsController = new AppoitmentsController();
            // Act
            Appointments appointment = appoitmentsController.GetAppointment(idAppointment);

            // Assert
            Assert.AreEqual(idAppointment, appointment.IdAppointment);
        }
        // Тест метода удаления записи
        [TestMethod]
        public void TestDeleteAppoitment()
        {
            // Arrange
            Appointments newAppoitment = new Appointments()
            { 
                Date = DateTime.Now,
                ReferralText = "sdf",
                IdPatient = 1,
                IdDoctor = 1,
                IdheadsDepartment = 1
            };
            db.context.Appointments.Add(newAppoitment);
            db.context.SaveChanges();

            int idAppointment = newAppoitment.IdAppointment;
            AppoitmentsController appoitmentsController = new AppoitmentsController();

            // Act
            appoitmentsController.DeleteAppoitments(idAppointment);

            // Assert
            Assert.AreEqual(null, db.context.Appointments.FirstOrDefault(x => x.IdAppointment == idAppointment));
        }
        // Тест метода получения пользователя
        [TestMethod]
        public void TestGetUser()
        {
            // Arrange
            int idUser = 1;
            UsersController usersController = new UsersController();
            // Act
            Users user = usersController.GetUser(idUser);

            // Assert
            Assert.AreEqual(idUser, user.idUser);
        }
        // Тест метода проверки авторизации
        [TestMethod]
        public void TestCheckAuth()
        {
            // Arrange
            string login = "user";
            string password = "5252ASD";
            UsersController usersController = new UsersController();
            // Act
            bool res = usersController.CheckAuth(login, password);

            // Assert
            Assert.AreEqual(true, res);
        }
        // Тест метода проверки, что пользователь является доктором
        [TestMethod]
        public void TestUserIsDoctor()
        {
            // Arrange
            int idUser = 1;
            UsersController usersController = new UsersController();
            // Act
            bool res = usersController.UserIsDoctor(idUser);

            // Assert
            Assert.AreEqual(false, res);
        }
    }
}
