﻿using System;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using System.Collections.Generic;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    /// <summary>
    /// Subclass of the User class
    /// </summary>
    public class Lector : User
    {
        #region Constructor
        /// <summary>
        /// Constructor without parameters, calls the base constructor of User (superclass)
        /// </summary>
        public Lector() : base()
        {
            
        }
        /// <summary>
        /// Constructor with 3 parameters, calls the base constructor of User (superclass)
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        public Lector(string firstName, string lastName, string emailAddress) : base(firstName, lastName, emailAddress)
        {
        }

        /// <summary>		
        /// Blocks material for Lector. If there is not enough available material, but there is enough blockable material,		
        /// the reservations for students get adjusted. If the amount required by the lector surpasses the amount in a student's		
        /// reservation this reservation get deleted.		
        /// </summary>		
        /// <param name="learningUtility"></param>	
        public override void AddReservation(DateTime dateWanted, int amount, LearningUtility learningUtility)
        {

            int amountAvailable = learningUtility.AmountAvailableForWeek(dateWanted);
            
            if (amount <= amountAvailable)
            {
                Reservation reservation = new Reservation
                {
                    User = this,
                    DateWanted = dateWanted,
                    Amount = amount,
                    ReservationDate = DateTime.Now,
                    LearningUtility = learningUtility

                };
                Reservations.Add(reservation);
                learningUtility.Reservations.Add(reservation);

            }


            else
            {
                if (amount <= learningUtility.AmountReservedForWeek(dateWanted) + amountAvailable)
                {
                    var studentReservations = learningUtility.Reservations.Where(res => res.User.GetType() == typeof(Student));
                    studentReservations.OrderBy(res => res.ReservationDate);

                    var amountNeededFromReservations = amount - amountAvailable;
                    while (amountNeededFromReservations > 0)
                    { 

                        foreach( Reservation res in studentReservations)
                        {
                            if( amountNeededFromReservations > res.Amount)
                            {
                                var reservationAmount = res.Amount;
                                res.Amount =- amountNeededFromReservations;
                                if(res.Amount == 0)
                                {
                                    this.Reservations.Remove(res);
                                }
                                amountNeededFromReservations =-reservationAmount;

                            }

                        }
                            

                    }
                    Reservation reservation = new Reservation
                    {
                        User = this,
                        DateWanted = dateWanted,
                        Amount = amount,
                        ReservationDate = DateTime.Now,
                        LearningUtility = learningUtility

                    };
                    this.Reservations.Add(reservation);

                }
                else
                {
                        throw new ArgumentOutOfRangeException();
                }

                

                throw new ArgumentNullException("Meer dan 1 item nodig om te reserveren");
            }

        }

    }
    #endregion
}
