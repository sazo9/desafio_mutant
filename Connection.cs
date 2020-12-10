using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Serialization.Json;
using MySql.Data.MySqlClient;

namespace DesafioMutant
{
    public class Connection
    {
        private MySqlConnection conn;
        public void insertUser(Models.User u) {
            var sql = "INSERT INTO desafio.user (id,id_address,id_company,name,username,email,phone,website) VALUES (@id,@id_address,@id_company,@name,@username,@email,@phone,@website)";
            try {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", u.id);
                cmd.Parameters.AddWithValue("@id_address", u.id);
                cmd.Parameters.AddWithValue("@id_company", u.id);
                cmd.Parameters.AddWithValue("@name", u.name);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@phone", u.phone);
                cmd.Parameters.AddWithValue("@website", u.website);
                var result = cmd.ExecuteNonQuery();
                Console.WriteLine("GRAVADO: " + cmd.CommandText);
            } catch (Exception e) {
                Console.WriteLine("ERROR: "+sql+"\n"+e.ToString());
            }
        }
        public void insertAddres(int id, Models.Address a) {
           var sql = "INSERT INTO desafio.address (id,street,suite,city,zipecode) VALUES (@id, @street, @suite, @city, @zipecode)";
            try {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@street", a.street);
                cmd.Parameters.AddWithValue("@suite", a.suite);
                cmd.Parameters.AddWithValue("@city", a.city);
                cmd.Parameters.AddWithValue("@zipecode", a.zipcode);
                var result = cmd.ExecuteNonQuery();
                Console.WriteLine("GRAVADO: " + cmd.CommandText);
            } catch (Exception e) {
                Console.WriteLine("ERROR: "+sql+"\n"+e.ToString());
            }
        }
        
       public void insertCompany(int id, Models.Company c) {
           var sql = "INSERT INTO desafio.company (id,name,catchPhrase,bs) VALUES (@id,@name,@catchPhrase,@bs)";
            try {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", c.name);
                cmd.Parameters.AddWithValue("@catchPhrase", c.catchPhrase);
                cmd.Parameters.AddWithValue("@bs", c.bs);
                var result = cmd.ExecuteNonQuery();
                Console.WriteLine("GRAVADO: " + cmd.CommandText);
            } catch (Exception e) {
                Console.WriteLine("ERROR: "+sql+"\n"+e.ToString());
            }
        }
        public string save()
        {
            
            string cs = @"server=localhost;userid=root;password=root;database=desafio";
            conn = new MySqlConnection(cs);
            conn.Open();

            List<Models.User> lstUsers = getListUser();
            foreach (Models.User u in lstUsers)
            {
                if(u.address.suite.Contains("Suite"))
                {
                    insertUser(u);
                    insertAddres(u.id, u.address);
                    insertCompany(u.id, u.company);
                    
                }
            }
            conn.Close();
            return "DADOS GRAVADOS COM SUCESSO!";
        }

        public List<Models.User> getListUser()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com");
            var request = new RestRequest("users", DataFormat.Json);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            JsonDeserializer jds = new JsonDeserializer();
            List<Models.User> user = jds.Deserialize<List<Models.User>> (response);

            return user;

        }

    }
}
