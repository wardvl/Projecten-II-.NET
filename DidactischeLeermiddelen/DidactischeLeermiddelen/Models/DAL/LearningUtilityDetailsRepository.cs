﻿using System;
using System.Data.Entity;
using System.Linq;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class LearningUtilityDetailsRepository : ILearningUtilityDetailsRepository
    {
        private readonly LeermiddelenContext context;
        private readonly DbSet<LearningUtilityDetails> learningUtilityDetailsList;

        public LearningUtilityDetailsRepository(LeermiddelenContext context)
        {
            this.context = context;
            learningUtilityDetailsList = context.LearningUtilityDetailsList;
        }
        public IQueryable<LearningUtilityDetails> FindAll()
        {
            return learningUtilityDetailsList;
        }

        public LearningUtilityDetails FindBy(int id)
        {
            return learningUtilityDetailsList.Find(id);
        }
    }
}