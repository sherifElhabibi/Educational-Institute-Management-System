using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignemnt.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assignemnt.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ITIContext _context;

        public DepartmentsController(ITIContext context)
        {
            _context = context;
        }

        // GET: Departments
        
        public IActionResult ShowCourses(int Id)
        {
            
           return View(_context.Department.Include(a => a.Courses).Where(a => a.DeptId == Id).ToList());
        }
        public IActionResult ShowDegrees(int DeptId,int CrsId)
        {
           var model = _context.CourseStudents.Include(a=>a.Student).Where(a=>a.CrsId == CrsId && a.Student.Department.DeptId==DeptId).ToList();
            return View(model);
        }
        public IActionResult UpdateStudentDegrees(int DeptId,int CrsId)
        {
            // var students = _context.Student.Where(a => a.Department.DeptId == DeptId).toList();
             
            var dept = _context.Department.Include(a => a.Students).FirstOrDefault(a => a.DeptId == DeptId);
            var course = _context.Course.FirstOrDefault(a => a.CrsId == CrsId);
            ViewBag.dept = dept;
            ViewBag.course = course;
            ViewBag.students = dept.Students;
            return View();

        }
        [HttpPost]
        public IActionResult UpdateStudentDegrees(int DeptId, int CrsId, Dictionary<int, int> std)
        {
            
            foreach (var item in std)
            {
                var stddegrees = _context.CourseStudents.FirstOrDefault(a => a.StdId == item.Key && a.CrsId == CrsId);
                if (stddegrees != null)
                {
                _context.CourseStudents.Add(new CourseStudent()
                {
                    CrsId = CrsId,
                    StdId = item.Key,
                    Degrees = item.Value
                });
                _context.SaveChanges();
                }
                else
                {
                    stddegrees.Degrees = item.Value;
                }
            }
                return RedirectToAction("Index");

        }
        public IActionResult UpdateCourses(int Id)
        {
            var allCourses = _context.Course.ToList();
            var selectedDept = _context.Department.Include(a => a.Courses).FirstOrDefault(a => a.DeptId == Id);
            var coursesInDept = selectedDept.Courses;
            var coursesNotInDept = allCourses.Except(coursesInDept).ToList(); 
            ViewBag.coursesInDept = new SelectList(coursesInDept,"CrsId","CrsName"); 
            ViewBag.coursesNotInDept = new SelectList(coursesNotInDept,"CrsId","CrsName");
            return View();
        }
        [HttpPost]
        public IActionResult UpdateCourses(int Id, int[] courseToAdd, int[] coursesToRemove)
        {
            var selectedDept = _context.Department.Include(a => a.Courses).FirstOrDefault(a => a.DeptId == Id);
            foreach (var item in courseToAdd)
            {
                var course = _context.Course.FirstOrDefault(a => a.CrsId == item);
                selectedDept.Courses.Add(course);
            }
            foreach (var item in coursesToRemove)
            {
                var course = _context.Course.FirstOrDefault(a => a.CrsId == item);
                selectedDept.Courses.Remove(course);
            }
            _context.SaveChanges();
            return RedirectToAction("updateCourses",new {id=Id});
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.Department != null ? 
                          View(await _context.Department.ToListAsync()) :
                          Problem("Entity set 'ITIContext.Department'  is null.");
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Department == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.DeptId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Capacity")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Department == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Capacity")] Department department)
        {
            if (id != department.DeptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DeptId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Department == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.DeptId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Department == null)
            {
                return Problem("Entity set 'ITIContext.Department'  is null.");
            }
            var department = await _context.Department.FindAsync(id);
            if (department != null)
            {
                _context.Department.Remove(department);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
          return (_context.Department?.Any(e => e.DeptId == id)).GetValueOrDefault();
        }
    }
}
