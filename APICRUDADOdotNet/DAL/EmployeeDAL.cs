using APICRUDADOdotNet.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APICRUDADOdotNet.DAL
{
    public class EmployeeDAL
    {
        SqlConnection _conn=null;
        SqlCommand _cmd=null;
        public IConfiguration Configuration { get; set; }
        private string GetConnectionString()
        {
            var builder=new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("AppConn");
        }

        public List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();
            using (_conn=new SqlConnection(GetConnectionString()))
            {
                _cmd=_conn.CreateCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "[GET_EMPLOYEES]";
                _conn.Open();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(reader["Id"]);
                    employee.FirstName = reader["FirstName"].ToString();
                    employee.LastName = reader["LastName"].ToString();
                    employee.DateofBirth = Convert.ToDateTime(reader["DateofBirth"]).Date;
                    employee.Email = reader["Email"].ToString();
                    employee.Salary = Convert.ToDouble(reader["Salary"]);
                    list.Add(employee);
                }
                _conn.Close();

            }
            return list;
        }

        public bool Insert (Employee emp)
        {
            int id = 0;
            using (_conn=new SqlConnection(GetConnectionString()))
            {
                _cmd=_conn.CreateCommand();
                _cmd.CommandType=CommandType.StoredProcedure;
                _cmd.CommandText = "[INSERT_EMPLOYEE]";

                _cmd.Parameters.AddWithValue("@firstName", emp.FirstName);
                _cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                _cmd.Parameters.AddWithValue("@DateofBirth", emp.DateofBirth);
                _cmd.Parameters.AddWithValue("@Email", emp.Email);
                _cmd.Parameters.AddWithValue("@Salary", emp.Salary);

                _conn.Open();
                id = _cmd.ExecuteNonQuery();
                _conn.Close();

            }
            return id>0 ? true : false;
        }
        public Employee GetById(int id)
        {
            Employee emp= new Employee();
            using (_conn=new SqlConnection(GetConnectionString()))
            {
                _cmd=_conn.CreateCommand();
                _cmd.CommandType= CommandType.StoredProcedure;
                _cmd.CommandText = "[GET_EMPLOYEEBYID]";
                _cmd.Parameters.AddWithValue("@Id", id);
                _conn.Open();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    emp.Id = Convert.ToInt32(reader["Id"]);
                    emp.FirstName = reader["FirstName"].ToString();
                    emp.LastName = reader["LastName"].ToString();
                    emp.DateofBirth = Convert.ToDateTime(reader["DateofBirth"]).Date;
                    emp.Email = reader["Email"].ToString();
                    emp.Salary = Convert.ToDouble(reader["Salary"]);
                }
                _conn.Close();
            }
            return emp;
        }
        public bool Update(Employee emp)
        {
            int id = 0;
            using (_conn=new SqlConnection(GetConnectionString()))
            {
                _cmd=_conn.CreateCommand();
                _cmd.CommandType=CommandType.StoredProcedure;
                _cmd.CommandText = "[UPDATE_EMPLOYEE]";

                _cmd.Parameters.AddWithValue("@Id", emp.Id);
                _cmd.Parameters.AddWithValue("@firstName", emp.FirstName);
                _cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                _cmd.Parameters.AddWithValue("@DateofBirth", emp.DateofBirth);
                _cmd.Parameters.AddWithValue("@Email", emp.Email);
                _cmd.Parameters.AddWithValue("@Salary", emp.Salary);

                _conn.Open();
                id = _cmd.ExecuteNonQuery();
                _conn.Close();
            }
            return id > 0 ? true:false;
        }
        public bool Delete(int id)
        {
            int count = 0;
            using (_conn = new SqlConnection(GetConnectionString()))
            {
                _cmd = _conn.CreateCommand();
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = "[DELETE_EMPLOYEE]";
                _cmd.Parameters.AddWithValue("@Id", id);
                _conn.Open();
                count = _cmd.ExecuteNonQuery();
                _conn.Close();
            }
           return count > 0 ? true : false;
        }
    }
    
}
