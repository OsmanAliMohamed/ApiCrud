using Microsoft.AspNetCore.Mvc;
using Api.Model;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Api.Controller
{
    [ApiController]
    [Route("[Controller]")]
    public class StudentController:ControllerBase
    {
        StudentContext db = new StudentContext();
        [HttpGet("GetAll")]
        public ActionResult All()
        {
            return Ok(db.students.ToList());
        }


        [HttpPost("Add")]
        public ActionResult Add(Student s)
        {
            try
            {
                db.students.Add(s);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest("There is a problem while adding this student");
            }
            return Ok("Successfully Added");
        }


        [HttpDelete("Delete")]
        public ActionResult Delete(int id)
        {
            Student match = db.students.Where(x => x.Id == id).FirstOrDefault();
            if (match != null)
            {
                db.students.Remove(match);
                db.SaveChanges();
                return Ok("Deleted");
            }
            return BadRequest("can't  find student with this id ?!!, Enter correct id to delete");
        }


        [HttpPut("Update")]
        public ActionResult Edit(Student s)
        {
            var ans = db.students.Where(std => std.Id == s.Id).FirstOrDefault();
            ans.Name = s.Name;
            ans.Adress = s.Adress;
            ans.Gpa = s.Gpa;
            db.SaveChanges();
            return Ok("Updated");
        }

        [HttpGet("Search")]
        public  ActionResult Search(int id)
        {
            bool found = db.students.Any(std => std.Id == id);
            if(found)
            {
                return Ok("Found");
            }
            return Ok("Not Found");
                
        }

        [HttpGet("GetHighAndLowGpa")]
        public ActionResult GetHighLowGpa()
        {
            var ans1 = db.students.OrderBy(x => x.Gpa).FirstOrDefault();
            var ans2 = db.students.OrderByDescending(x => x.Gpa).FirstOrDefault();
            List<Student> res = new List<Student>();
            res.Add(ans1);
            res.Add(ans2);
            return Ok(res);
        }

        [HttpGet("GetMin_MaxNameLength")]
        public ActionResult GetMin_MaxLength()
        {
            IOrderedQueryable<Student> ans1 = db.students.OrderBy(s => s.Name.Length).ThenBy(s => s.Name);
            if(ans1.Count() < 2)
            {
                return BadRequest("There is found less than two student in the list");
            }
            var first = ans1.FirstOrDefault();
            var last = ans1.LastOrDefault();
            List<Student> res = new List<Student>();
            res.Add(first);
            res.Add(last);
            return Ok(res);
        }

        [HttpGet("GroupStudentsByGpa")]
        public ActionResult GroupStudentsByGpa()
        {
            var list = db.students.ToList().GroupBy(x => x.Gpa).ToList();
            var ans = list.ToHashSet();
            foreach (var item in list)
            {
                return Ok(item.Key);
                
            }
            return Ok("Thats Result"); 
        }
    }
}
