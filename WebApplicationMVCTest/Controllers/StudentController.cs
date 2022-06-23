using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationMVCTest.Data;
using WebApplicationMVCTest.Models;

namespace WebApplicationMVCTest.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Student> objStudentList = _dbContext.Students;
            return View(objStudentList);
        }

        public IActionResult GetDetails(long? id)
        {
             var objStudentList = _dbContext.Students
            .Include(e => e.Enrollments)
            .ThenInclude(e=>e.Course)           
            .Where(m => m.Id == id);
            return View(objStudentList);
        }

        //GET
        public IActionResult AddStudent()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudent(Student _student)
        {
            if (_dbContext.Students.Any(x => x.Email == _student.Email))
            {
                ModelState.AddModelError("Email", "Email address already exists");
                return View(_student);
            }
            if (_dbContext.Students.Any(x => x.PhoneNo == _student.PhoneNo))
            {
                ModelState.AddModelError("PhoneNo", "Phone no. already exists");
                return View(_student);
            }

            if (ModelState.IsValid)
            {
                try
                { 
                _dbContext.Students.Add(_student);
                _dbContext.SaveChanges();
                TempData["success"] = "Student Added successfully!!";
                return RedirectToAction("Index");
                }
                catch(Exception )
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error while connecting to the database");
                }
            }
            return View(_student);
           
                
            
        }


        //GET
        public IActionResult EditStudent(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var studentFromDb = _dbContext.Students.Find(id);
            
            if (studentFromDb == null)
            {
                return NotFound();
            }
            return View(studentFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStudent(Student _student)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Students.Update(_student);
                _dbContext.SaveChanges();
                TempData["success"] = "Student Updated successfully!!";
                return RedirectToAction("Index");
            }
            return View(_student);
        }


        //GET
        public IActionResult DeleteStudent(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var studentFromDb = _dbContext.Students.Find(id);

            if (studentFromDb == null)
            {
                return NotFound();
            }
            return View(studentFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(long? id)
        {
            var obj = _dbContext.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dbContext.Students.Remove(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Student Deleted successfully!!";
            return RedirectToAction("Index");
        }
    }
}
