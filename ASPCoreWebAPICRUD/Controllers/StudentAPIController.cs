using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]//Attribute routing 
    [ApiController]
 //ControllerBase is a base class which sends a http request to server and generates http response. 
    public class StudentAPIController : ControllerBase
    {
        // MyDBContext kha object for only reading table data.
        private readonly MyDbContext db;
      // creating a contructor then pass parameter of MyDbContext class ka object;

        public StudentAPIController(MyDbContext db)//select db + ctrl+.
        {
            this.db = db;
        }
        [HttpGet]//hit with get request 
        //web api data return kore ba response return kore view kore na
        // view()-> MVC te kaj kore
        public async Task<ActionResult<List<Student>>> GetStudents()
        {

            var data = await db.Students.ToListAsync();
            return Ok(data);//status code 200 generates kore dei
        }

        [HttpGet("{id}")]//hit with id of student sends as a request to server
        public async Task<ActionResult<object>>GetStudentDataById(int id)
        {
            /*
             * Another process
             * var dataraw= await context.Students.FindAsync(id);
             * if(dataraw==null)
             * {
             *  return NotFound();//status code 404
             * }
             * return dataraw;
             * 
             */
            var data = await db.Students.ToListAsync();
            if (id<0|| data.Count <= id)
            {
                return BadRequest();
            }
           
            foreach(var stu in data)
            {
                Console.WriteLine(stu.StudentName);
                if(stu.Id == id)
                {
                    return stu;
                }
            }
            return BadRequest(); //status code 400
        }

        [HttpPost]//hit with  a request to server and create new student object
        public async Task<ActionResult<Student>> CreateStudent(Student stu)//accept Student class ka object;
        {

            //Another process
            await db.Students.AddAsync(stu);//save  in DbSet <Student> named Students..->save student data
            await db.SaveChangesAsync();//permanently save data
            return Ok(stu);//status code 200


        }

        
        [HttpPut("{id}")]//hit with  a request to server and create new student object
        public async Task<ActionResult<Student>> UpdateStudent(int id ,Student stu)
        {


           if(id<0)
            {
                return BadRequest();
            }
            //var dataR = await db.Students.FindAsync(id);//check if id if exixt or not!
            if(id!=stu.Id)//It is a primary key it should not be changed.
            {
                return BadRequest();
            }
            db.Entry(stu).State = EntityState.Modified;//modified data 
            await db.SaveChangesAsync();//permanently save data
            return Ok(stu);


        }



        [HttpDelete("{id}")]//hit with  a request to server and create new student object
        public async Task<ActionResult<Student>> DeleteStudentByID(int id)
        {


            if (id < 0)
            {
                return BadRequest();
            }
           var dataR = await db.Students.FindAsync(id);//check if id if exixt or not!
            if(dataR==null)//It is a primary key it should not be changed.
            {
                return NotFound();
            }
            db.Students.Remove(dataR);//removing this Students data 
            await db.SaveChangesAsync();//permanently save data
            return Ok();


        }
    }
}
