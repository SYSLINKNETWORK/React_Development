using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Api_Project.DatabaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        SqlDataAdapter daweb = new SqlDataAdapter ();
        SqlCommand cmd = new SqlCommand ();
        SqlConnection con = new SqlConnection ("Data Source=ADEEL\\ADEEL;Initial Catalog=Text;Integrated Security=true;");
        SqlTransaction transweb = null;
        SqlParameter paramweb = new SqlParameter ();
        DataSet dsweb = new DataSet ();
        string cmdtxt = "";
        System.Data.DataTable dttblusrgp_ins = new System.Data.DataTable();

        private readonly ApplicationDbContext _logger;

        public UsersController(ApplicationDbContext logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //[Route("GetUser")]
        public string Get()
        {
            System.Data.DataTable dttblpo_sel = new System.Data.DataTable();
            dttblpo_sel.Columns.Add("Remarks", typeof(string));
            dttblpo_sel.Columns.Add("status", typeof(int));
            dttblpo_sel.Columns.Add("Result", typeof(object));
           
            var UserTable =  _logger.User.ToList();

            if (UserTable.Count() > 0)
            {
                dttblpo_sel.Rows.Add("OK", 1, UserTable);
            }
            else
            {
                dttblpo_sel.Rows.Add("Record not found", 3, null);
            }
            return JsonConvert.SerializeObject(dttblpo_sel);
            
        }

        [HttpGet]
        [Route("GetByID")]
        public string GetByID([FromBody] int _id)
        {
            System.Data.DataTable dttblpo_sel = new System.Data.DataTable();
            dttblpo_sel.Columns.Add("Remarks", typeof(string));
            dttblpo_sel.Columns.Add("status", typeof(int));
            dttblpo_sel.Columns.Add("Result", typeof(object));
           
            var UserTable =  _logger.User.Where(x => x.ID == _id ).FirstOrDefault();

            if (UserTable != null)
            {
                dttblpo_sel.Rows.Add("OK", 1, UserTable);
            }
            else
            {
                dttblpo_sel.Rows.Add("Record not found", 3, null);
            }
            return JsonConvert.SerializeObject(dttblpo_sel);
            
        }

        //Create Start
        [HttpPost]
        [Route ("~/User/create")]
        public string create ([FromBody] dynamic dataResult) {
            System.Data.DataTable dttblusrgp_ins = new System.Data.DataTable ();
            dttblusrgp_ins.Columns.Add ("Id", typeof (string));
            dttblusrgp_ins.Columns.Add ("Remarks", typeof (string));
            dttblusrgp_ins.Columns.Add ("status", typeof (int));

           
            string _com_nam = Convert.ToString (dataResult.Name);
            string _com_Address = Convert.ToString (dataResult.Address);

            cmd = new SqlCommand("insert into [User](Name,Address) values(@name,@Address)", con);  
            con.Open();  
            cmd.Parameters.Clear ();
            cmd.Parameters.AddWithValue("@name", _com_nam);  
            cmd.Parameters.AddWithValue("@Address", _com_Address);  
            int i = cmd.ExecuteNonQuery ();
            if (i > 0) {
                con.Close ();
                dttblusrgp_ins.Rows.Add (_com_nam.ToString (), "Record # " + _com_nam + " Saved", 1);
            }
            return JsonConvert.SerializeObject (dttblusrgp_ins);
        }
        //Create End

        //Update Start
        [HttpPut]
        [Route ("~/User/Update")]
        public string Update ([FromBody] dynamic dataResult) {
            System.Data.DataTable dttblusrgp_ins = new System.Data.DataTable ();
            dttblusrgp_ins.Columns.Add ("Id", typeof (string));
            dttblusrgp_ins.Columns.Add ("Remarks", typeof (string));
            dttblusrgp_ins.Columns.Add ("status", typeof (int));

            string _com_id = Convert.ToInt32 (dataResult.ID);
            string _com_nam = Convert.ToString (dataResult.Name);
            string _com_Address = Convert.ToString (dataResult.Address);

            cmd = new SqlCommand("Update [User] set Name=@name,Address=@Address where id=@id", con);  
            con.Open();  
            cmd.Parameters.Clear ();
            cmd.Parameters.AddWithValue("@id", _com_id); 
            cmd.Parameters.AddWithValue("@name", _com_nam);  
            cmd.Parameters.AddWithValue("@Address", _com_Address);  
            int i = cmd.ExecuteNonQuery ();
            if (i > 0) {
                con.Close ();
                dttblusrgp_ins.Rows.Add (_com_id.ToString (), "Record # " + _com_nam + " Update", 1);
            }
            return JsonConvert.SerializeObject (dttblusrgp_ins);
        }
        //Update End

        //Update Start
        [HttpPut]
        [Route ("~/User/Delete")]
        public string Delete ([FromBody] dynamic dataResult) {
            System.Data.DataTable dttblusrgp_ins = new System.Data.DataTable ();
            dttblusrgp_ins.Columns.Add ("Id", typeof (string));
            dttblusrgp_ins.Columns.Add ("Remarks", typeof (string));
            dttblusrgp_ins.Columns.Add ("status", typeof (int));

            string _com_id = Convert.ToInt32 (dataResult.ID);
            cmd = new SqlCommand("Delete from [User] where id=@id", con);  
            con.Open();  
            cmd.Parameters.Clear ();
            cmd.Parameters.AddWithValue("@id", _com_id);  
            int i = cmd.ExecuteNonQuery ();
            if (i > 0) {
                con.Close ();
                dttblusrgp_ins.Rows.Add (_com_id.ToString (), "Record # " + _com_id + " Deleted", 1);
            }
            return JsonConvert.SerializeObject (dttblusrgp_ins);
        }
        //Update End
    }
}
