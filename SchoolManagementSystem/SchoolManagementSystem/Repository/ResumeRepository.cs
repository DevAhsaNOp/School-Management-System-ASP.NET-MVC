using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolManagementSystem.Models;
using System.IO;
using System.Data.Entity.Validation;
using System.Data.Entity;
using SchoolManagementSystem.ViewModels;
using SchoolManagementSystem.Repository;
using DatabaseAccess;

namespace SchoolManagementSystem.Repository
{
    public class ResumeRepository : IResumeRepository
    {
        //Db Context
        private readonly SchoolMgtDbEntities _dbContext = new SchoolMgtDbEntities();

        public bool AddCertification(EmployeeCertificationTable certification, int idPer)
        {
            try
            {
                int countRecords = 0;
                EmployeeResumeTable personEntity = _dbContext.EmployeeResumeTables.Where(p => p.EmployeeID == idPer).FirstOrDefault();

                if (personEntity != null && certification != null)
                {
                    //personEntity.EmployeeCertificationTables.Add(certification);
                    countRecords = _dbContext.SaveChanges();
                }

                return countRecords > 0 ? true : false;
            }
            catch (DbEntityValidationException dbEx)
            {

                throw;
            }

        }

        public bool AddLanguage(EmployeeLanguageTable language, int idPer)
        {
            int countRecords = 0;
            EmployeeResumeTable personEntity = _dbContext.EmployeeResumeTables.Where(p => p.EmployeeID == idPer).FirstOrDefault();

            if (personEntity != null && language != null)
            {
                personEntity.EmployeeLanguageTables.Add(language);
                countRecords = _dbContext.SaveChanges();
            }

            return countRecords > 0 ? true : false;
        }

        public string AddOrUpdateEducation(EmployeeEducationTable education, int idPer)
        {
            string msg = string.Empty;

            EmployeeResumeTable personEntity = _dbContext.EmployeeResumeTables.Where(p => p.EmployeeID == idPer).FirstOrDefault();

            if (personEntity != null)
            {
                if (education.EmployeeEducationID > 0)
                {
                    //we will update education entity
                    _dbContext.Entry(education).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    msg = "Education entity has been updated successfully";
                }
                else
                {
                    // we will add new education entity
                    personEntity.EmployeeEducationTables.Add(education);
                    _dbContext.SaveChanges();

                    msg = "Education entity has been Added successfully";
                }
            }

            return msg;
        }

        public string AddOrUpdateExperience(EmployeeWorkExperienceTable workExperience, int idPer)
        {
            string msg = string.Empty;

            EmployeeResumeTable personEntity = _dbContext.EmployeeResumeTables.Where(p => p.EmployeeID == idPer).FirstOrDefault();

            if (personEntity != null)
            {
                if (workExperience.EmployeeWorkExperienceID > 0)
                {
                    //we will update work experience entity
                    _dbContext.Entry(workExperience).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    msg = "Work Experience entity has been updated successfully";
                }
                else
                {
                    // we will add new work experience entity
                    personEntity.EmployeeWorkExperienceTables.Add(workExperience);
                    _dbContext.SaveChanges();

                    msg = "Work Experience entity has been Added successfully";
                }
            }

            return msg;
        }

        public bool AddPersonnalInformation(EmployeeResumeTable person, HttpPostedFileBase file)
        {
            try
            {
                int nbRecords = 0;

                if (person != null)
                {
                    if (file != null)
                    {
                        person.Profil = ConvertToBytes(file);
                    }

                    _dbContext.EmployeeResumeTables.Add(person);
                    nbRecords = _dbContext.SaveChanges();
                }

                return nbRecords > 0 ? true : false;
            }
            catch (DbEntityValidationException dbEx)
            {

                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }

        public bool AddSkill(EmployeeSkillTable skill, int idPer)
        {
            int countRecords = 0;
            EmployeeResumeTable personEntity = _dbContext.EmployeeResumeTables.Where(p => p.EmployeeID == idPer).FirstOrDefault();

            if (personEntity != null && skill != null)
            {
                personEntity.EmployeeSkillTables.Add(skill);
                countRecords = _dbContext.SaveChanges();
            }

            return countRecords > 0 ? true : false;

        }

        public IQueryable<EmployeeCertificationTable> GetCertificationsById(int idPer)
        {
            var certificationList = _dbContext.EmployeeCertificationTables.Where(w => w.EmployeeResumeID == idPer);
            return certificationList;
        }

        public IQueryable<EmployeeEducationTable> GetEducationById(int idPer)
        {
            var educationList = _dbContext.EmployeeEducationTables.Where(e => e.EmployeeResumeID == idPer);
            return educationList;
        }

        public int GetIdPerson(int idPer)
        {
            int employeeResumeID = _dbContext.EmployeeResumeTables.Where(p => p.EmployeeID == idPer)
                                              .Select(p => p.EmployeeResumeID).FirstOrDefault();

            return employeeResumeID;
        }

        public IQueryable<EmployeeLanguageTable> GetLanguageById(int idPer)
        {
            var languageList = _dbContext.EmployeeLanguageTables.Where(w => w.EmployeeResumeID == idPer);
            return languageList;
        }

        public EmployeeResumeTable GetPersonnalInfo(int idPer)
        {
            return _dbContext.EmployeeResumeTables.Find(idPer);

        }

        public IQueryable<EmployeeSkillTable> GetSkillsById(int idPer)
        {
            var skillsList = _dbContext.EmployeeSkillTables.Where(w => w.EmployeeResumeID == idPer);
            return skillsList;
        }

        public IQueryable<EmployeeWorkExperienceTable> GetWorkExperienceById(int idPer)
        {
            var workExperienceList = _dbContext.EmployeeWorkExperienceTables.Where(w => w.EmployeeResumeID == idPer);
            return workExperienceList;
        }

        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

    }

}
