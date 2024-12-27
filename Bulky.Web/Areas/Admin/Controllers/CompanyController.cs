using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //get all category
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return View(objCompanyList);
        }

        // upsert => updateInsert
        public IActionResult Upsert(int? id)
        {


            if (id == null || id == 0)
            {

                return View(new Company());

            }
            else
            {
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {


            if (ModelState.IsValid)
            {


                if (companyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(companyObj);

                }
                else
                {
                    _unitOfWork.Company.Update(companyObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company Created successfully";
                return RedirectToAction("Index");

            }
            else
            {

                return View(companyObj);
            }

        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDelete = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyToBeDelete == null)
            {
                return Json(new { success = false, message = "Error While deleting" });
            }


            _unitOfWork.Company.Remove(companyToBeDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Company Deleted successfully" });

        }

        #endregion
    }
}
