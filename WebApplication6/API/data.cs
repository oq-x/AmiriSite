using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
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
            string query = @"INSERT INTO Users (UUID, Email, Username, PasswordHash, FirstName, LastName, Bio, Score, AvatarURL, Token)
                         VALUES (@UUID, @Email, @Username, @PasswordHash, @FirstName, @LastName, @Bio, @Score, @AvatarURL, @Token)";
            SqlCommand cmd = new SqlCommand(query, sql);

            cmd.Parameters.AddWithValue("@UUID", user.UUID.ToByteArray());
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash());
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Bio", user.Bio());
            cmd.Parameters.AddWithValue("@Score", user.Score());
            cmd.Parameters.AddWithValue("@AvatarURL", user.AvatarURL());
            cmd.Parameters.AddWithValue("@Token", user.Token());

            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
        }

        public User GetUser(string email)
        {
            var parameters = new Dictionary<string, object>
    {
        { "@Email", email }
    };

            User[] results = getUsers("Email = @Email", parameters);
            return results.Length > 0 ? results[0] : null;
        }

        public User GetUserByUsername(string username)
        {
            var parameters = new Dictionary<string, object>
    {
        { "@Username", username }
    };

            User[] results = getUsers("Username = @Username", parameters);
            return results.Length > 0 ? results[0] : null;
        }

        public User Authenticate(string token)
        {
            byte[] tokenBytes = new byte[token.Length / 2];
            for (int i = 0; i < tokenBytes.Length; i++)
            {
                tokenBytes[i] = Convert.ToByte(token.Substring(i * 2, 2), 16);
            }

            var parameters = new Dictionary<string, object>
    {
        { "@Token", tokenBytes }
    };

            User[] results = getUsers("Token = @Token", parameters);
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
                            new Guid((byte[])(row[userTable.Columns[0]])),
                            (string)(row[userTable.Columns[1]]),
                            (string)(row[userTable.Columns[2]]),
                            (byte[])(row[userTable.Columns[3]]),
                            (string)(row[userTable.Columns[4]]),
                            (string)(row[userTable.Columns[5]]),
                            (string)(row[userTable.Columns[6]]),
                            (string)(row[userTable.Columns[8]]),
                            (byte[])(row[userTable.Columns[9]]),
                            (double)(row[userTable.Columns[7]])
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
                string query = "SELECT 1 FROM Users WHERE Email = @Email";

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
                string query = "SELECT 1 FROM Users WHERE Username = @Username";

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
        public readonly Guid UUID;
        public readonly string Email;
        public readonly string Username;
        private byte[] passwordHash;
        private string bio;
        public readonly string FirstName;
        public readonly string LastName;

        private string avatarURL;
        private double score;

        private readonly byte[] token;

        public User(
            string email, 
            string username, 
            string password, 
            string firstName, 
            string lastName
            )
        {
            UUID = Guid.NewGuid();
            Email = email;
            Username = username;
            SetPassword(password);
            bio = "";
            LastName = lastName;
            FirstName = firstName;

            avatarURL = "";
            score = 0;

            byte[] token = SHA256.Create().ComputeHash(UUID.ToByteArray());

            // combine both the uuid and password (XOR)
            for (int i = 0; i < token.Length; i++)
            {
                token[i] ^= passwordHash[i];
            }

            this.token = token;
        }

        public User(
            Guid UUID,
            string email,
            string username,
            byte[] passwordHash,

            string firstName,
            string lastName,

            string bio,
            string avatarURL,
            byte[] token,
            double score = 0
        )
        {
            this.UUID = UUID;
            Email = email;
            Username = username;
            this.passwordHash = passwordHash;
            this.bio = bio;
            FirstName = firstName;
            LastName = lastName;

            this.avatarURL = avatarURL;
            this.token = token;
            this.score = score;
        }

        // this sets the password hash to the hash of the new password
        public void SetPassword(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] rawBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(rawBytes);

            passwordHash = hashBytes;
        }

        public void SetBio(string bio)
        {
            this.bio = bio;
        }

        public void SetAvatarURL(string avatarURL)
        {
            this.avatarURL = avatarURL;
        }
        public string Bio() { return bio; }
        public double Score() { return score; }
        public string AvatarURL() { return avatarURL; }

        public byte[] PasswordHash() { return passwordHash; }

        public bool IsPasswordEqual(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] rawBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(rawBytes);

            return passwordHash.SequenceEqual(hashBytes);
        }

        public bool IsTokenEqual(string token)
        {
            return Token() == token;
        }

        public string Token() {
            var sb = new StringBuilder(token.Length * 2);
            foreach (byte b in token)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        // Update the user's score
        public void UpdateScore(Message message)
        {
            if (message == null || !message.SenderUUID.Equals(UUID))
            {
                // message not from user
                return;
            }

            double i = (double)message.Content.Length / 20;
            i *= ((double)(new Random()).Next(0, 101)) / 100;

            score += (score * i) + i;
        }
    }

    public class Message
    {
        public readonly Guid UUID;
        public readonly Guid SenderUUID;
        public readonly string Content;
        public readonly DateTime CreatedAt;

        public Message(Guid uuid, Guid senderUUID, string content, DateTime createdAt)
        {
            UUID = uuid;
            SenderUUID = senderUUID;
            Content = content;
            CreatedAt = createdAt;
        }
    }
}