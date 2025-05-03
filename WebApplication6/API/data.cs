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
            return getUsers();
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

        // inserts the message into the database (unsafe so private [there can be another channel with the same uuid])
        private void InsertMessage(Message message)
        {
            SqlConnection sql = new SqlConnection(_connectionString);
            string query = @"INSERT INTO Messages (UUID, SenderUUID, Content, CreatedAt, ChannelUUID)
                         VALUES (@UUID, @SenderUUID, @Content, @CreatedAt, @ChannelUUID)";
            SqlCommand cmd = new SqlCommand(query, sql);

            cmd.Parameters.AddWithValue("@UUID", message.UUID.ToByteArray());
            cmd.Parameters.AddWithValue("@SenderUUID", message.SenderUUID.ToByteArray());
            cmd.Parameters.AddWithValue("@Content", message.Content);
            cmd.Parameters.AddWithValue("@CreatedAt", message.CreatedAt);
            cmd.Parameters.AddWithValue("@ChannelUUID", message.ChannelUUID.ToByteArray());

            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
        }
        
        // inserts the channel into the database (unsafe too)
        private void InsertChannel(Channel channel)
        {
            SqlConnection sql = new SqlConnection(_connectionString);
            string query = @"INSERT INTO Channels (UUID, OwnerUUID, Name)
                         VALUES (@UUID, @OwnerUUID, @Name)";
            SqlCommand cmd = new SqlCommand(query, sql);

            cmd.Parameters.AddWithValue("@UUID", channel.UUID.ToByteArray());
            cmd.Parameters.AddWithValue("@OwnerUUID", channel.OwnerUUID.ToByteArray());
            cmd.Parameters.AddWithValue("@Name", channel.Name);

            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
        }

        // creates a new channel from the provided data and inserts it into the databse
        public void CreateChannel(User owner, string name)
        {
            InsertChannel(new Channel(Guid.NewGuid(), owner.UUID, name));
        }

        // creates a new message from the provided data and inserts it into the database
        public void PostMessage(Channel channel, User sender, string content)
        {
            InsertMessage(new Message(Guid.NewGuid(), sender.UUID, content, DateTime.Now, channel.UUID));
        }
        public Channel[] GetChannels(User owner)
        {
            return GetChannels(owner.UUID);
        }
        public Channel[] GetChannels(Guid owner)
        {
            var parameters = new Dictionary<string, object>
            {
        { "@OwnerUUID", owner.ToByteArray() }
    };

            return getChannels("OwnerUUID = @OwnerUUID", parameters);
        }

        public Channel[] GetChannels()
        {
            return getChannels();
        }
        private Channel[] getChannels(string whereClause = null, Dictionary<string, object> parameters = null)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM channels";

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
                    adapter.Fill(dataSet, "Channels");

                    DataTable userTable = dataSet.Tables[0];

                    Channel[] channels = new Channel[userTable.Rows.Count];

                    for (int i = 0; i < channels.Length; i++)
                    {
                        DataRow row = userTable.Rows[i];

                        channels[i] = new Channel(
                            new Guid((byte[])(row[userTable.Columns[0]])),
                            new Guid((byte[])(row[userTable.Columns[1]])),
                            (string)(row[userTable.Columns[2]])
                        );
                    }

                    return channels;
                }
            }

        }

        public int MessageCount(Channel channel)
        {
            return MessageCount(channel.UUID);
        }

        public int MessageCount(Guid channel)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                string query = "SELECT 1 FROM Messages WHERE ChannelUUID = @ChannelUUID";

                using (SqlCommand cmd = new SqlCommand(query, sql))
                {
                    cmd.Parameters.AddWithValue("@ChannelUUID", channel);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Messages");

                    DataTable messageTable = dataSet.Tables[0];

                    return messageTable.Rows.Count;
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
        public readonly Guid ChannelUUID;

        public Message(Guid uuid, Guid senderUUID, string content, DateTime createdAt, Guid channelUUID)
        {
            UUID = uuid;
            SenderUUID = senderUUID;
            Content = content;
            CreatedAt = createdAt;
            ChannelUUID = channelUUID;
        }
    }

    public class Channel
    {
        public readonly Guid UUID;
        public readonly Guid OwnerUUID;
        public readonly string Name;

        public Channel(Guid uUID, Guid ownerUUID, string name)
        {
            UUID = uUID;
            OwnerUUID = ownerUUID;
            Name = name;
        }
    }
}