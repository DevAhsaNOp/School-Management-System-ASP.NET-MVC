using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatabaseAccess;
using Magnum;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repository;
using SchoolManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class ResumeController : Controller
    {
        private readonly IResumeRepository _resumeRepository;
        public ResumeController(IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }

        // GET: Resume
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckCV(int? id)
        {
            var employeeid = 0;
            if (id == null || id == 0)
            {
                int.TryParse(Convert.ToString(Session["EmployeeResumeID"]), out employeeid);
            }else
            {
                employeeid = Convert.ToInt32(id);
            }    
            using (SchoolMgtDbEntities db = new SchoolMgtDbEntities())
            {
                var people = db.EmployeeResumeTables.Where(p => p.EmployeeID == employeeid);
                if (people != null)
                {
                    if (people.Count() > 0)
                    {
                        return RedirectToAction("CV", new { id = employeeid });
                    }
                    else
                    {
                        return RedirectToAction("PersonnalInformtion");
                    }
                }
                else
                {
                    return RedirectToAction("PersonnalInformtion");
                }
            }
        }

        public ActionResult ViewCV(int? id)
        {
            return RedirectToAction("CV", new { id = id });
        }


        public ActionResult PersonnalInformtion()
        {
            return View();
        }


        [HttpGet]
        public ActionResult PersonnalInformtion(PersonVM model)
        {
            //Nationality
            List<SelectListItem> nationality = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Pakistan", Value = "Pakistan", Selected = true},
            };


            //Educational Level
            List<SelectListItem> educationalLevel = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Hight School", Value = "Hight School", Selected = true},
                new SelectListItem { Text = "Diploma", Value = "Diploma"},
                new SelectListItem { Text = "Bachelor's degree", Value = "Bachelor's degree"},
                new SelectListItem { Text = "Master's degree", Value = "Master's degree"},
                new SelectListItem { Text = "Doctorate", Value = "Doctorate"},
            };

            model.ListOfNationality = nationality;
            model.ListOfEduLevel = educationalLevel;

            return View(model);
        }

        [HttpPost]
        [ActionName("PersonnalInformtion")]
        public ActionResult AddPersonnalInformtion(PersonVM person)
        {
            var employeeid = 0;
            int.TryParse(Convert.ToString(Session["EmployeeResumeID"]), out employeeid);

            person.DOB = DateTime.Now;

            if (ModelState.IsValid)
            {
                //Creating Mapping
                Mapper.Initialize(cfg => cfg.CreateMap<PersonVM, Person>());

                EmployeeResumeTable personEntity = Mapper.Map<EmployeeResumeTable>(person);
                personEntity.EmployeeID = employeeid;
                HttpPostedFileBase file = Request.Files["ImageProfil"];

                bool result = _resumeRepository.AddPersonnalInformation(personEntity, file);

                if (result)
                {
                    Session["EmployeeResumeID"] = _resumeRepository.GetIdPerson(employeeid);
                    return RedirectToAction("Education");
                }
                else
                {
                    ViewBag.Message = "Something Wrong !";
                    return View(person);
                }

            }

            ViewBag.MessageForm = "Please Check your form before submit !";
            return View(person);

        }

        [HttpGet]
        public ActionResult Education(EducationVM education)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrUpdateEducation(EducationVM education)
        {
            try
            {


                string msg = string.Empty;

                if (education != null)
                {
                    //Creating Mapping
                    Mapper.Reset();
                    Mapper.Initialize(cfg => cfg.CreateMap<EducationVM, Education>());
                    EmployeeEducationTable educationEntity = Mapper.Map<EmployeeEducationTable>(education);

                    int idPer = (int)Session["EmployeeResumeID"];

                    msg = _resumeRepository.AddOrUpdateEducation(educationEntity, idPer);

                }
                else
                {
                    msg = "Please re try the operation";
                }

                return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { data = "Undefined! please Try Again!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public PartialViewResult EducationPartial(EducationVM education)
        {

            education.ListOfCountry = GetCountries();

            return PartialView("~/Views/Shared/_MyEducation.cshtml", education);
        }

        [HttpGet]
        public ActionResult WorkExperience()
        {
            return View();
        }

        public PartialViewResult WorkExperiencePartial(WorkExperienceVM workExperience)
        {
            workExperience.ListOfCountry = GetCountries();

            return PartialView("~/Views/Shared/_MyWorkExperience.cshtml", workExperience);
        }

        public ActionResult AddOrUpdateExperience(WorkExperienceVM workExperience)
        {

            string msg = string.Empty;

            if (workExperience != null)
            {
                //Creating Mapping
                Mapper.Reset();
                Mapper.Initialize(cfg => cfg.CreateMap<WorkExperienceVM, WorkExperience>());
                EmployeeWorkExperienceTable workExperienceEntity = Mapper.Map<EmployeeWorkExperienceTable>(workExperience);

                int idPer = (int)Session["EmployeeResumeID"];


                msg = _resumeRepository.AddOrUpdateExperience(workExperienceEntity, idPer);

            }
            else
            {
                msg = "Please re try the operation";
            }

            return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SkiCerfLang()
        {
            return View();
        }

        public PartialViewResult SkillsPartial()
        {
            return PartialView("~/Views/Shared/_MySkills.cshtml");
        }

        public ActionResult AddSkill(SkillVM skill)
        {
            int idPer = (int)Session["EmployeeResumeID"];
            string msg = string.Empty;

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<SkillVM, Skill>());
            EmployeeSkillTable skillEntity = Mapper.Map<EmployeeSkillTable>(skill);

            if (_resumeRepository.AddSkill(skillEntity, idPer))
            {
                msg = "skill added successfully";
            }
            else
            {
                msg = "something Wrong";
            }

            return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult CertificationsPartial(CertificationVM certification)
        {
            List<SelectListItem> certificationLevel = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Beginner", Value = "Beginner", Selected = true},
                new SelectListItem { Text = "Intermediate", Value = "Intermediate"},
                new SelectListItem { Text = "Advanced", Value = "Advanced"}
            };

            certification.ListOfLevel = certificationLevel;

            return PartialView("~/Views/Shared/_MyCertifications.cshtml", certification);
        }

        public ActionResult AddCertification(CertificationVM certification)
        {
            int idPer = (int)Session["EmployeeResumeID"];
            string msg = string.Empty;

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<CertificationVM, Certification>());
            EmployeeCertificationTable certificationEntity = Mapper.Map<EmployeeCertificationTable>(certification);

            if (_resumeRepository.AddCertification(certificationEntity, idPer))
            {
                msg = "Certification added successfully";
            }
            else
            {
                msg = "something Wrong";
            }

            return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult LanguagePartial(LanguageVM language)
        {
            List<SelectListItem> languageLevel = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Elementary Proficiency", Value = "Elementary Proficiency", Selected = true},
                new SelectListItem { Text = "LimitedWorking Proficiency", Value = "LimitedWorking Proficiency"},
                new SelectListItem { Text = "Professional working Proficiency", Value = "Professional working Proficiency"},
                new SelectListItem { Text = "Full Professional Proficiency", Value = "Full Professional Proficiency"},
                new SelectListItem { Text = "Native Or Bilingual Proficiency", Value = "Native Or Bilingual Proficiency"}
            };

            language.ListOfProficiency = languageLevel;

            return PartialView("~/Views/Shared/_MyLanguage.cshtml", language);
        }

        public ActionResult AddLanguage(LanguageVM language)
        {
            int idPer = (int)Session["EmployeeResumeID"];
            string msg = string.Empty;

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<LanguageVM, Language>());
            EmployeeLanguageTable languageEntity = Mapper.Map<EmployeeLanguageTable>(language);

            if (_resumeRepository.AddLanguage(languageEntity, idPer))
            {
                msg = "Language added successfully";
            }
            else
            {
                msg = "something Wrong";
            }

            return Json(new { data = msg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CV(int? id)
        {
            using (SchoolMgtDbEntities db = new SchoolMgtDbEntities())
            {
                var person = db.EmployeeResumeTables.Where(p => p.EmployeeID == id).FirstOrDefault();
                Session["EmployeeResumeID"] = person.EmployeeID;
                return View();
            }
        }

        public PartialViewResult GetPersonnalInfoPartial()
        {
            int idPer = (int)Session["EmployeeResumeID"];
            EmployeeResumeTable person = _resumeRepository.GetPersonnalInfo(idPer);

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeResumeTable, PersonVM>());
            PersonVM personVM = Mapper.Map<PersonVM>(person);

            return PartialView("~/Views/Shared/_MyPersonnalInfo.cshtml", personVM);
        }

        public PartialViewResult GetEducationCVPartial()
        {
            int idPer = (int)Session["EmployeeResumeID"];

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeEducationTable, EducationVM>());
            IQueryable<EducationVM> educationList = _resumeRepository.GetEducationById(idPer).ProjectTo<EducationVM>().AsQueryable();

            return PartialView("~/Views/Shared/_MyEducationCV.cshtml", educationList);
        }

        public PartialViewResult WorkExperienceCVPartial()
        {
            int idPer = (int)Session["EmployeeResumeID"];

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeWorkExperienceTable, WorkExperienceVM>());
            IQueryable<WorkExperienceVM> workExperienceList = _resumeRepository.GetWorkExperienceById(idPer).ProjectTo<WorkExperienceVM>().AsQueryable();


            return PartialView("~/Views/Shared/_WorkExperienceCV.cshtml", workExperienceList);
        }

        public PartialViewResult SkillsCVPartial()
        {
            int idPer = (int)Session["EmployeeResumeID"];

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeSkillTable, SkillVM>());
            IQueryable<SkillVM> skillsList = _resumeRepository.GetSkillsById(idPer).ProjectTo<SkillVM>().AsQueryable();


            return PartialView("~/Views/Shared/_MySkillsCV.cshtml", skillsList);
        }

        public PartialViewResult CertificationsCVPartial()
        {
            int idPer = (int)Session["EmployeeResumeID"];

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeCertificationTable, CertificationVM>());
            IQueryable<CertificationVM> certificationList = _resumeRepository.GetCertificationsById(idPer).ProjectTo<CertificationVM>().AsQueryable();


            return PartialView("~/Views/Shared/_MyCertificationCV.cshtml", certificationList);
        }

        public PartialViewResult LanguageCVPartial()
        {
            int idPer = (int)Session["EmployeeResumeID"];

            //Creating Mapping
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeLanguageTable, LanguageVM>());
            IQueryable<LanguageVM> languageList = _resumeRepository.GetLanguageById(idPer).ProjectTo<LanguageVM>().AsQueryable();


            return PartialView("~/Views/Shared/_MyLanguageCV.cshtml", languageList);
        }

        public ActionResult GetProfilImage(int id)
        {
            byte[] image = _resumeRepository.GetPersonnalInfo(id).Profil;
            if (image != null)
            {
                return File(image, "image/png");
            }
            else
            {
                return null;
            }

        }

        public ActionResult GetCities(string country)
        {
            List<SelectListItem> listOfCities = new List<SelectListItem>();


            switch (country)
            {
                case "Pakistan":
                    listOfCities.Add(new SelectListItem() { Text = "KPK", Value = "KPK", Selected = true });
                    listOfCities.Add(new SelectListItem() { Text = "Punjab", Value = "Punjab" });
                    listOfCities.Add(new SelectListItem() { Text = "Sindh", Value = "Sindh" });
                    listOfCities.Add(new SelectListItem() { Text = "Balochistan", Value = "Balochistan" });
                    break;

                case "India":
                    listOfCities.Add(new SelectListItem() { Text = "Bombay", Value = "Bombay", Selected = true });
                    listOfCities.Add(new SelectListItem() { Text = "Bangalore", Value = "Bangalore" });
                    listOfCities.Add(new SelectListItem() { Text = "Chennai", Value = "Chennai" });
                    listOfCities.Add(new SelectListItem() { Text = "Hyderabad", Value = "Hyderabad" });
                    break;

                case "Spain":
                    listOfCities.Add(new SelectListItem() { Text = "Barcelone", Value = "Barcelone", Selected = true });
                    listOfCities.Add(new SelectListItem() { Text = "Madrid", Value = "Madrid" });
                    listOfCities.Add(new SelectListItem() { Text = "Valence", Value = "Valence" });
                    listOfCities.Add(new SelectListItem() { Text = "Malaga", Value = "Malaga" });
                    break;

                case "USA":
                    listOfCities.Add(new SelectListItem() { Text = "New York", Value = "New York", Selected = true });
                    listOfCities.Add(new SelectListItem() { Text = "Los Angeles", Value = "Los Angeles" });
                    listOfCities.Add(new SelectListItem() { Text = "San Francisco", Value = "San Francisco" });
                    listOfCities.Add(new SelectListItem() { Text = "Chicago", Value = "Chicago" });
                    break;
            }

            return Json(new { data = listOfCities }, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> GetCountries()
        {
            List<SelectListItem> listOfCountry = new List<SelectListItem>()
            {
                 new SelectListItem() { Text = "Pakistan", Value = "Pakistan", Selected = true},
                new SelectListItem() { Text = "Morocco", Value = "Morocco" },
                new SelectListItem() { Text = "India", Value = "India"},
                new SelectListItem() { Text = "Spain", Value = "Spain"},
                new SelectListItem() { Text = "USA", Value = "USA"}
            };

            return listOfCountry;
        }
    }
}