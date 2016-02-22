﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DidactischeLeermiddelen.Models.DAL;
using DidactischeLeermiddelen.Models.Domain;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;

namespace DidactischeLeermiddelen.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILearningUtilityDetailsRepository learningUtilityDetailsRepository;

        public CatalogController(ILearningUtilityDetailsRepository learningUtilityDetailsRepository)
        {
            this.learningUtilityDetailsRepository = learningUtilityDetailsRepository;

        }


        // GET: Catalog
        public ActionResult Index(User user)
        {
            IEnumerable<LearningUtilityDetails> catalog = null;

            if (User.Identity.IsAuthenticated)
            {
                catalog = user.GetLearningUtilities(learningUtilityDetailsRepository);
            }
            else
            {
                catalog = learningUtilityDetailsRepository.FindAll().Where(learningUtilityDetails => learningUtilityDetails.Loanable == true);
            }

            return View(catalog);
        }

        // GET: Catalog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearningUtilityDetails learningUtilityDetails = learningUtilityDetailsRepository.FindBy((int)id);
            if (learningUtilityDetails == null)
            {
                return HttpNotFound();
            }
            return View(learningUtilityDetails);
        }
    }
}