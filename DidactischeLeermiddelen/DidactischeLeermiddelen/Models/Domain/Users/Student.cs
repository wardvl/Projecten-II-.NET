﻿using System;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public class Student : User
    {
        #region Constructor
        /// <summary>
        /// Constructor without parameters, calls the base constructor of User (superclass)
        /// </summary>
        public Student() : base()
        {
            
        }
        /// <summary>
        /// Constructor with 3 parameters, calls the base constructor of User (superclass)
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        public Student(string firstName, string lastName, string emailAddress) : base(firstName, lastName, emailAddress)
        {
            
        }



        #endregion
        #region Methods
        public override void AddReservation(DateTime dateWanted, int amount, LearningUtility learningUtility)
        {
            Reservation reservation = new Reservation
            {
                User = this,
                DateWanted = dateWanted,
                Amount = amount,
                ReservationDate = DateTime.Now
            };
            learningUtility.AddReservation(reservation);
        }

        public override void RemoveReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}