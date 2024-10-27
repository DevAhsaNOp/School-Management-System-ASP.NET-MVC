
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DatabaseAccess;

namespace SchoolManagementSystem.Repository
{
    public interface IResumeRepository
    {
        bool AddPersonnalInformation(EmployeeResumeTable person, HttpPostedFileBase file);
        string AddOrUpdateEducation(EmployeeEducationTable education, int idPer);
        int GetIdPerson(int idPer);
        string AddOrUpdateExperience(EmployeeWorkExperienceTable workExperience, int idPer);
        bool AddSkill(EmployeeSkillTable skill, int idPer);
        bool AddCertification(EmployeeCertificationTable certification, int idPer);
        bool AddLanguage(EmployeeLanguageTable language, int idPer);
        EmployeeResumeTable GetPersonnalInfo(int idPer);
        IQueryable<EmployeeEducationTable> GetEducationById(int idPer);
        IQueryable<EmployeeWorkExperienceTable> GetWorkExperienceById(int idPer);
        IQueryable<EmployeeSkillTable> GetSkillsById(int idPer);
        IQueryable<EmployeeCertificationTable> GetCertificationsById(int idPer);
        IQueryable<EmployeeLanguageTable> GetLanguageById(int idPer);


    }
}
