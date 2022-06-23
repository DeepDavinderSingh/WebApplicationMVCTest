using Microsoft.AspNetCore.Mvc;
using WebApplicationMVCTest.Data;
using WebApplicationMVCTest.Models;

namespace WebApplicationMVCTest.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
         public CourseController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Course> objCourseList = _dbContext.Courses;
            return View(objCourseList);
        }

        //GET
        public IActionResult AddCourse()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourse(Course _course)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Courses.Add(_course);
                _dbContext.SaveChanges();
                TempData["success"] = "Course Added successfully!!";
                return RedirectToAction("Index");
            }
            return View(_course);
        }

        //GET
        public IActionResult EditCourse(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var courseFromDb = _dbContext.Courses.Find(id);

            if (courseFromDb == null)
            {
                return NotFound();
            }
            return View(courseFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCourse(Course _course)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the name");
            //}
            if (ModelState.IsValid)
            {
                _dbContext.Courses.Update(_course);
                _dbContext.SaveChanges();
                TempData["success"] = "Course Updated successfully!!";
                return RedirectToAction("Index");
            }
            return View(_course);
        }


        //GET
        public IActionResult DeleteCourse(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var courseFromDb = _dbContext.Courses.Find(id);

            if (courseFromDb == null)
            {
                return NotFound();
            }
            return View(courseFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(long? id)
        {
            var obj = _dbContext.Courses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dbContext.Courses.Remove(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Course Deleted successfully!!";
            return RedirectToAction("Index");
        }
    }
}
