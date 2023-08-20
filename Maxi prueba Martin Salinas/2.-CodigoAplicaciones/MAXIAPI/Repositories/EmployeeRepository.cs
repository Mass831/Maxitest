using ENTITIES;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MAXIAPI.Interfaces
{
    public class EmployeeRepository: IEmployee
    {

        Employee _employee = new Employee();
        List<Employee> _employees = new List<Employee>();
        public async Task<Employee> Add(Employee employee)
        {
            _employee = new Employee();
            try
            {
                using (IDbConnection con = new SqlConnection(Utils.Utils.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@FirstName", employee.FirstName);
                    parameters.Add("@LastName", employee.LastName);
                    parameters.Add("@DateOfBirth", employee.DateOfBirth);
                    parameters.Add("@EmployeeNumber", employee.EmployeeNumber);
                    parameters.Add("@Curp", employee.Curp);
                    parameters.Add("@Ssn", employee.Ssn);
                    parameters.Add("@PhoneNumber", employee.PhoneNumber);
                    parameters.Add("@Nationality", employee.Nationality);
                    var _employees = await con.QueryAsync<Employee>("dbo.spr_AddEmployee", parameters, commandType: CommandType.StoredProcedure);
                    if (_employees != null && _employees.Count() > 0)
                    {
                        _employee = _employees.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                _employee.Message = ex.Message;
            }
            return _employee;
        }

        public async Task<string> Delete(int employeeId)
        {
            string message = string.Empty;
            try
            {
                using (IDbConnection con = new SqlConnection(Utils.Utils.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@EmployeeId", employeeId);
                    var _employees = await con.QueryAsync<Employee>("dbo.spr_DeleteEmployee", parameters, commandType: CommandType.StoredProcedure);
                    if (_employees != null && _employees.Count() > 0)
                    {
                        _employee = _employees.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public async Task<Employee> Get(int employeeId)
        {
            _employee = new Employee();
            using (IDbConnection con = new SqlConnection(Utils.Utils.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EmployeeId", employeeId);
                var _employees = await con.QueryAsync<Employee>("dbo.spr_GetEmployee", parameters, commandType: CommandType.StoredProcedure);
                if (_employees != null && _employees.Count() > 0)
                {
                    _employee = _employees.FirstOrDefault();
                }
            }
            return _employee;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            _employees = new List<Employee>();
            using (IDbConnection con = new SqlConnection(Utils.Utils.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var _employeesList = (await con.QueryAsync<Employee>("dbo.spr_GetEmployees", commandType: CommandType.StoredProcedure)).ToList();
                if (_employeesList != null && _employeesList.Count() > 0)
                {
                    _employees = _employeesList;
                }
            }
            return _employees;
        }

        public async Task<Employee> Update(Employee employee)
        {
            _employee = new Employee();
            try
            {
                using (IDbConnection con = new SqlConnection(Utils.Utils.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@EmployeeId", employee.EmployeeId);
                    parameters.Add("@FirstName", employee.FirstName);
                    parameters.Add("@LastName", employee.LastName);
                    parameters.Add("@DateOfBirth", employee.DateOfBirth);
                    parameters.Add("@EmployeeNumber", employee.EmployeeNumber);
                    parameters.Add("@Curp", employee.Curp);
                    parameters.Add("@Ssn", employee.Ssn);
                    parameters.Add("@PhoneNumber", employee.PhoneNumber);
                    parameters.Add("@Nationality", employee.Nationality);
                    var _employees = await con.QueryAsync<Employee>("dbo.spr_UpdateEmployee", parameters, commandType: CommandType.StoredProcedure);
                    if (_employees != null && _employees.Count() > 0)
                    {
                        _employee = _employees.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                _employee.Message = ex.Message;
            }
            return _employee;
        }
    }
}
