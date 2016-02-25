﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.Users;
using DidactischeLeermiddelen.Tests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DidactischeLeermiddelen.Tests.Model.Users
{
    [TestClass]
    public class UserTest
    {
        #region Arrange

        private User student;
        private User lector;
        private Mock<ILearningUtilityDetailsRepository> mockLearningUtilityDetailsRepository;
        private DummyDataContext context;
        #endregion

        [TestInitialize]
        public void UserTestInitialize()
        {
            student = UserFactory.CreateUserWithUserType(UserType.Student);
            lector = UserFactory.CreateUserWithUserType(UserType.Lector);

            context = new DummyDataContext();
            mockLearningUtilityDetailsRepository = new Mock<ILearningUtilityDetailsRepository>();
            mockLearningUtilityDetailsRepository.Setup(repo => repo.FindAll())
                .Returns(context.LearningUtilityDetailsList);
        }

        [TestMethod]
        public void UserSetsNamesWhenTheyAreCorrect()
        {
            #region Arrange

            const string firstName = "Benjamin";
            const string lastName = "Vertonghen";

            #endregion

            #region Act

            student.FirstName = firstName;
            student.LastName = lastName;

            #endregion

            #region Assert

            Assert.AreEqual(firstName, student.FirstName);
            Assert.AreEqual(lastName, student.LastName);

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof (ValidationException))]
        public void UserThrowsErrorWhenFirstNameIsNull()
        {
            #region Arrange

            const string name = null;

            #endregion

            #region Act

            student.FirstName = name;

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof (ValidationException))]
        public void UserThrowsErrorWhenLastNameIsNull()
        {
            #region Arrange

            const string name = null;

            #endregion

            #region Act

            student.LastName = name;

            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UserThrowsErrorWhenEmailAddressIsNull()
        {
            #region Arrange

            const string email = null;

            #endregion

            #region Act

            student.EmailAddress = email;

            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UserThrowsErrorWhenFirstNameIsEmpty()
        {
            #region Arrange

            string name = String.Empty;

            #endregion

            #region Act

            student.FirstName = name;

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UserThrowsErrorWhenLastNameIsEmpty()
        {
            #region Arrange

            string name = String.Empty;

            #endregion

            #region Act

            student.LastName = name;

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UserThrowsErrorWhenEmailAddressIsEmpty()
        {
            #region Arrange

            string email = String.Empty;

            #endregion

            #region Act

            student.EmailAddress = email;

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof (ValidationException))]
        public void UserThrowsErrorWhenLastNameHas101Characters()
        {
            #region Arrange

            const string name =
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";

            #endregion

            #region Act

            student.LastName = name;

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof (ValidationException))]
        public void UserThrowsErrorWhenFirstNameHas101Characters()
        {
            #region Arrange

            const string name =
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";

            #endregion

            #region Act

            student.FirstName = name;

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof (ValidationException))]
        public void UserThrowsErrorWhenEmailIsNotFromHoGent()
        {
            #region Arrange

            const string email = "benjamin.vertonghen@victory-be.be";

            #endregion

            #region Act

            student.EmailAddress = email;

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof (ValidationException))]
        public void UserThrowsErrorWhenEmailDoesNotEndWithHogent()
        {
            #region Arrange

            const string email = "benjamin.hogent@victory.be";

            #endregion

            #region Act

            student.EmailAddress = email;

            #endregion
        }

        [TestMethod]
        public void UserSetsEmailAddressForStudent()
        {
            #region Arrange

            const string email = "benjamin.vertonghen@student.hogent.be";

            #endregion

            #region Act

            var userType = UserFactory.DetermineUserTypeByEmailAddress(email);
            var user = UserFactory.CreateUserWithUserType(userType);
            user.EmailAddress = email;

            #endregion

            #region Assert

            Assert.AreEqual(email, user.EmailAddress);

            #endregion
        }

        [TestMethod]
        public void UserSetsEmailAddressForLector()
        {
            #region Arrange

            User tesUser = new Lector();
            const string email = "Lector.WeetIkVeel@hogent.be";

            #endregion

            #region Act

            tesUser.EmailAddress = email;

            #endregion

            #region Assert

            Assert.AreEqual(email, tesUser.EmailAddress);

            #endregion
        }
        
        [TestMethod]
        public void LectorGetLearningUtilityDetails()
        {
            #region Arrange
            var expectedList = context.LearningUtilityDetailsList;
            var expectedAmountFound = expectedList.Count();
            #endregion

            #region Act
            var actualList = lector.GetLearningUtilities(mockLearningUtilityDetailsRepository.Object);
            var actualAmountFound = actualList.Count();
            #endregion

            #region Assert
            Assert.AreEqual(expectedAmountFound, actualAmountFound);
            mockLearningUtilityDetailsRepository.Verify(m => m.FindAll(), Times.Once);
            #endregion
        }
        
        [TestMethod]
        public void StudentGetLearningUtilityDetails()
        {

            #region Arrange

            var expectedList =
                context.LearningUtilityDetailsList.Where(utilityDetails => utilityDetails.Loanable);
            var expectedAmountFound = expectedList.Count();
            #endregion

            #region Act
            var actualList = student.GetLearningUtilities(mockLearningUtilityDetailsRepository.Object);
            var actualAmountFound = actualList.Count();

            #endregion

            #region Assert
            Assert.AreEqual(expectedAmountFound, actualAmountFound);
            mockLearningUtilityDetailsRepository.Verify(m => m.FindAll(), Times.Once);

            #endregion
        }

    }
}