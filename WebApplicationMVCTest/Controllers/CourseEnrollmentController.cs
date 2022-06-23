using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationMVCTest.Data;
using WebApplicationMVCTest.Models;


namespace StudentManagementSystem.Controllers
{
    public class CourseEnrollmentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseEnrollmentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Enrollments
        public IActionResult Index()
        {
            IEnumerable<Enrollment> objList = _dbContext.Enrollments.Include(e => e.Course).Include(e => e.Student);
            return View(objList);
        }

        //GET: Enrollments/Details/5
        public IActionResult Details(long? id)
        {
            var objStudentList = _dbContext.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .Where(m => m.Id == id);

            return View(objStudentList);
        }

        

        public IActionResult AddEnrollment()
        {
            ViewData["CourseID"] = new SelectList(_dbContext.Courses, "Id", "courseName");
            ViewData["StudentID"] = new SelectList(_dbContext.Students, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEnrollment([Bind("Id,CourseID,StudentID")] Enrollment enrollment)
        {
            foreach (var item in _dbContext.Enrollments)
            {
                if (item.CourseID == enrollment.CourseID && item.StudentID == enrollment.StudentID)
                {
                    ModelState.AddModelError("CourseID", "The student is already enrolled in this course");
                    return View(enrollment);
                }
            }

            _dbContext.Add(enrollment);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
            
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _dbContext.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _dbContext.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            ViewData["CourseID"] = new SelectList(_dbContext.Courses, "Id", "courseName");
            ViewData["StudentID"] = new SelectList(_dbContext.Students, "Id", "Name");
            return View(enrollment);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CourseID,StudentID")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }
            try
            {
                _dbContext.Update(enrollment);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(enrollment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");

        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _dbContext.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _dbContext.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_dbContext.Enrollments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Enrollments'  is null.");
            }
            var enrollment = await _dbContext.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _dbContext.Enrollments.Remove(enrollment);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EnrollmentExists(long id)
        {
            return (_dbContext.Enrollments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}