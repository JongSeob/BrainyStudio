﻿using api.Helpers;
using api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace api.Controllers
{
    public class RepositoryController : ApiController
    {
        /// Global helpers for Database configuration, connection and data serialization
        private static DatabaseHelper _DBconfig = new DatabaseHelper();
        private SqlConnection _myConnection = new SqlConnection(_DBconfig.ConnString());
        private SerializationHelper _serializer = new SerializationHelper();

        // GET ALL
        // GET: api/Repository
        public IEnumerable<Repository> Get()
        {
            _myConnection.Open();
            string strSQL = "SELECT * FROM Repository";
            List<Repository> result = new List<Repository>();

            using (_myConnection)
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, _myConnection))
                {
                    SqlDataAdapter parser = new SqlDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    parser.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                        result.Add(new Repository(
                            row.Field<int>(0),
                            row.Field<string>(1),
                            row.Field<string>(2),
                            row.Field<string>(3)
                            ));
                }
                return result;
            }
        }

        // GET BY ID
        // GET: api/Repository/5
        public Repository Get(int id)
        {
            _myConnection.Open();
            Repository result = new Repository();
            string strSQL = "SELECT * FROM Repository WHERE Id = @userID";

            using (_myConnection)
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, _myConnection))
                {
                    cmd.Parameters.AddWithValue("@userID", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = new Repository(Int32.Parse(reader["Id"].ToString()),
                                reader["Name"].ToString(), reader["Description"].ToString(),
                                reader["Picture"].ToString());
                        }
                    }
                }
                return result;
            }
        }

        // POST NEW 
        // POST: api/Repository
        public void Post([FromBody]Repository value)
        {
            _myConnection.Open();
            String strSQL = "INSERT INTO Repository (Name,Description,Picture) VALUES (@name,@description,@picture)";

            using (_myConnection)
            {
                using (SqlCommand sql_command = new SqlCommand(strSQL, _myConnection))
                {
                    sql_command.Parameters.Add("@name", value.Name);
                    sql_command.Parameters.Add("@description", value.Description);
                    sql_command.Parameters.Add("@picture", value.Picture);
                    sql_command.ExecuteNonQuery();
                }
            }
        }

        // MODIFY BY ID
        // PUT: api/Repository/5
        public void Put(int id, [FromBody]Repository value)
        {
            _myConnection.Open();
            String strSQL = "UPDATE Repository SET Name = @name, Description = @description , Picture = @picture WHERE Id = @id;";

            using (_myConnection)
            {
                using (SqlCommand sql_command = new SqlCommand(strSQL, _myConnection))
                {
                    sql_command.Parameters.Add("@id", id);
                    sql_command.Parameters.Add("@name", value.Name);
                    sql_command.Parameters.Add("@description", value.Description);
                    sql_command.Parameters.Add("@picture", value.Picture);
                    sql_command.ExecuteNonQuery();
                }
            }
        }

        // DELETE BY ID
        // DELETE: api/Repository/5
        public void Delete(int id)
        {
            _myConnection.Open();
            String strSQL = "DELETE FROM Repository WHERE Id = @id;";

            using (_myConnection)
            {
                using (SqlCommand sql_command = new SqlCommand(strSQL, _myConnection))
                {
                    sql_command.Parameters.Add("@id", id);
                    sql_command.ExecuteNonQuery();
                }
            }
        }
    }
}