using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EmpAPI.Controllers
{
  /*  public ValuesController()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        _dbContext = new database_Access_Layer.db(connectionString);
    }
   */
   

    public class ValuesController : ApiController
    {
        private readonly database_Access_Layer.db _dbContext;

       
        public ValuesController()
        {
            var x = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            _dbContext = new database_Access_Layer.db(x);
            //_dbContext = new database_Access_Layer.db(".;Initial Catalog=EmployeeDB;Integrated Security=True");
        }
        /*
        public DataSet Getrecord(int id)
        {
            DataSet ds = _dbContext.GetRecordById(id);
            return ds;
        }*/

        public IHttpActionResult Post([FromBody] string value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _dbContext.AddUser(value);
            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        public IHttpActionResult Put(int id, [FromBody] string value)
        {
            var user = _dbContext.GetRecordById(id);
            if (user == null)
            {
                return NotFound();
            }
            _dbContext.UpdateUser(id, value);
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var x = _dbContext;
            var user = _dbContext.GetRecordById(id);
            if (user == null)
            {
                return NotFound();
            }
            _dbContext.DeleteUser(id);
            return Ok();
        }
    }
}
