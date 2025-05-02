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
        private readonly string _connectionString;

        public DataManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateUser(User user)
        {
            SqlConnection sql = new SqlConnection(_connectionString);
            string query = @"INSERT INTO users (email, username, password, bio, firstName, lastName, score, pfpLink)
                         VALUES (@Email, @Username, @Password, @Bio, @FirstName, @LastName, @Score, @PFPLink)";
            SqlCommand cmd = new SqlCommand(query, sql);

            cmd.Parameters.AddWithValue("@Email", user.Email());
            cmd.Parameters.AddWithValue("@Username", user.Username());
            cmd.Parameters.AddWithValue("@Password", user.PasswordHash());
            cmd.Parameters.AddWithValue("@Bio", user.Bio());
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName());
            cmd.Parameters.AddWithValue("@LastName", user.LastName());
            cmd.Parameters.AddWithValue("@Score", user.Score());
            cmd.Parameters.AddWithValue("@PFPLink", user.AvatarURL());

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
        private User[] getUsers(string whereClause = null, Dictionary<string, object> parameters = null)
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
 
                        users[i] = new User(
                            (string)row[userTable.Columns[0]],
                            (string)row[userTable.Columns[1]],
                            (string)row[userTable.Columns[2]],
                            (string)row[userTable.Columns[6]],
                            (string)row[userTable.Columns[3]],
                            (string)row[userTable.Columns[4]],
                            (string)row[userTable.Columns[7]],
                            (double)row[userTable.Columns[5]]
                        );
                    }

                    return users;
                }
            }

        }
        public bool UserExistByEmail(string email)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                string query = "SELECT 1 FROM Users WHERE email = @Email";

                using (SqlCommand cmd = new SqlCommand(query, sql))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Users");

                    DataTable userTable = dataSet.Tables[0];

                    return userTable.Rows.Count > 0;
                }
            }
        }

        public bool UserExistByUsername(string username)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                string query = "SELECT 1 FROM Users WHERE username = @Username";

                using (SqlCommand cmd = new SqlCommand(query, sql))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Users");

                    DataTable userTable = dataSet.Tables[0];

                    return userTable.Rows.Count > 0;
                }
            }
        }
    }

    public class User
    {
        private readonly string email;
        private readonly string username;
        private string passwordHash;
        private string bio;
        private readonly string firstName;
        private readonly string lastName;

        private string avatarURL;
        private double score;

        public User(string email,
            string username,
            string passwordHash,
            string bio,
            string firstName,
            string lastName,

            string avatarURL,
            double score = 0
        )
        {
            this.email = email;
            this.username = username;
            this.passwordHash = passwordHash;
            this.bio = bio;
            this.firstName = firstName;
            this.lastName = lastName;

            this.avatarURL = avatarURL;
            this.score = score;
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

        public void SetBio(string bio)
        {
            this.bio = bio;
        }

        public void SetAvatarURL(string avatarURL)
        {
            this.avatarURL = avatarURL;
        }

        public string Email() { return email; }
        public string Username() { return username; }
        public string Bio() { return bio; }
        public string FirstName() { return firstName; }
        public string LastName() { return lastName; }
        public double Score() { return score; }
        public string AvatarURL() { return avatarURL; }

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

        // Update the user's score
        public void UpdateScore(Message message)
        {
            if (message == null || message.Sender() != email)
            {
                // message not from user
                return;
            }

            double i = (double)message.Content().Length / 20;
            i *= ((double)(new Random()).Next(0, 101)) / 100;

            score += (score * i) + i;
        }
    }

    public class Message
    {
        private readonly string sender;
        private readonly string content;

        public Message(string sender, string content)
        {
            this.sender = sender;
            this.content = content;
        }

        public string Sender() { return sender; }
        public string Content() { return content; }
    }
}