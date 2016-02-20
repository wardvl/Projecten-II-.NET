﻿using System;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities.LearningUtilityStates
{
    [TestClass]
    public class LearningUtilityUnavailableStateTests
    {
        private LearningUtility learningUtility;
        private User student;
        private User lector;

        [TestInitialize]
        public void InitializeTest()
        {
            learningUtility = new LearningUtility();
            learningUtility.ToState(StateFactory.CreateState(StateType.Unavailable, learningUtility));
            student = UserFactory.CreateUser(UserType.Student);
            lector = UserFactory.CreateUser(UserType.Lector);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void CallToMakeAvailableThrowsException()
        {
            //Act
            learningUtility.MakeAvailable();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CallToReserveThrowsException()
        {
            //Act
            learningUtility.Reserve(student);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CallToBlockThrowsException()
        {
            //Act
            learningUtility.Block(lector);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CallToLateThrowsException()
        {
            //Act
            learningUtility.Late();
        }
    }
}
