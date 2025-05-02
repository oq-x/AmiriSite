using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication6
{
    public class DataManager
    {
        private string _connectionString;

        public DataManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateUser(User user)
        {
            SqlConnection sql = new SqlConnection(_connectionString);
            string query = @"INSERT INTO users (email, username, password, bio, firstName, lastName)
                         VALUES (@Email, @Username, @Password, @Bio, @FirstName, @LastName)";
            SqlCommand cmd = new SqlCommand(query, sql);

            cmd.Parameters.AddWithValue("@Email", user.Email());
            cmd.Parameters.AddWithValue("@Username", user.Username());
            cmd.Parameters.AddWithValue("@Password", user.PasswordHash());
            cmd.Parameters.AddWithValue("@Bio", user.Bio());
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName());
            cmd.Parameters.AddWithValue("@LastName", user.LastName());

            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
        }

        public User GetUser(string email)
        {
            var parameters = new Dictionary<string, object>
    {
        { "@email", email }
    };

            User[] results = getUsers("email = @email", parameters);
            return results.Length > 0 ? results[0] : null;
        }

        public User[] GetUsers()
        {
            return getUsers(null);
        }
        public User[] getUsers(string whereClause = null, Dictionary<string, object> parameters = null)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM users";

                if (!string.IsNullOrWhiteSpace(whereClause))
                {
                    query += " WHERE " + whereClause;
                }

                using (SqlCommand cmd = new SqlCommand(query, sql))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Users");

                    DataTable userTable = dataSet.Tables[0];

                    User[] users = new User[userTable.Rows.Count];

                    for (int i = 0; i < users.Length; i++)
                    {
                        DataRow row = userTable.Rows[i];

                        string bio = "";
                        if (!Convert.IsDBNull(row[userTable.Columns[3]]))
                        {
                            bio = (string)row[userTable.Columns[3]];
                        }
 
                        users[i] = new User(
                            (string)row[userTable.Columns[0]],
                            (string)row[userTable.Columns[1]],
                            (string)row[userTable.Columns[2]],
                            bio,
                            (string)row[userTable.Columns[4]],
                            (string)row[userTable.Columns[5]]
                        );
                    }

                    return users;
                }
            }

        }
    }

    public class User
    {
        private string email;
        private string username;
        private string passwordHash;
        private string bio;
        private string firstName;
        private string lastName;

        public User(string email, 
            string username, 
            string passwordHash, 
            string bio, 
            string firstName, 
            string lastName
        )
        {
            this.email = email;
            this.username = username;
            this.passwordHash = passwordHash;
            this.bio = bio;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        // this sets the password hash to the hash of the new password
        public void SetPassword(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] rawBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(rawBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));

            string computedHash = sb.ToString();

            passwordHash = computedHash;
        }

        public string Email() { return email; }
        public string Username() { return username; }
        public string Bio() { return bio; }
        public string FirstName() { return firstName; }
        public string LastName() { return lastName; }

        public string PasswordHash() { return passwordHash; }

        public bool IsPasswordEqual(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] rawBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(rawBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));

            string computedHash = sb.ToString();

            return string.Equals(computedHash, passwordHash, StringComparison.OrdinalIgnoreCase);
        }
    }

    public class Message
    {
        private string sender;
        private string content;

        public Message(string sender, string content)
        {
            this.sender = sender;
            this.content = content;
        }

        public string Sender() { return sender; }
        public string Content() { return content; }
    }
}