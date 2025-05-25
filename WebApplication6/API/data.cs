using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;

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
            string query = @"INSERT INTO Users (UUID, Email, Username, PasswordHash, FirstName, LastName, Bio, Score, AvatarURL, Token, CreatedAt)
                         VALUES (@UUID, @Email, @Username, @PasswordHash, @FirstName, @LastName, @Bio, @Score, @AvatarURL, @Token, @CreatedAt)";
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
            cmd.Parameters.AddWithValue("@Token", user.token);
            cmd.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);

            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
        }
        public User GetUser(Guid uuid)
        {
            var parameters = new Dictionary<string, object>
    {
        { "@UUID", uuid.ToByteArray() }
    };

            User[] results = GetUsers("UUID = @UUID", parameters);
            return results.Length > 0 ? results[0] : null;
        }

        public User GetUser(string email)
        {
            var parameters = new Dictionary<string, object>
    {
        { "@Email", email }
    };

            User[] results = GetUsers("Email = @Email", parameters);
            return results.Length > 0 ? results[0] : null;
        }

        public User GetUserByUsername(string username)
        {
            var parameters = new Dictionary<string, object>
    {
        { "@Username", username }
    };

            User[] results = GetUsers("Username = @Username", parameters);
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

            User[] results = GetUsers("Token = @Token", parameters);
            return results.Length > 0 ? results[0] : null;
        }

        public User[] GetUsers()
        {
            return GetUsers(null, null);
        }
        private User[] GetUsers(string whereClause, Dictionary<string, object> parameters)
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
                            new Guid((byte[])(row[userTable.Columns[0]])), //uuid
                            (string)(row[userTable.Columns[1]]), //email
                            (string)(row[userTable.Columns[2]]), //username
                            (byte[])(row[userTable.Columns[3]]), //passwordhash
                            (string)(row[userTable.Columns[4]]), //firstname
                            (string)(row[userTable.Columns[5]]), //lastname
                            (string)(row[userTable.Columns[6]]), //bio
                            (string)(row[userTable.Columns[8]]), //avatarurl
                            (byte[])(row[userTable.Columns[9]]), //token
                            (DateTime)(row[userTable.Columns[10]]), //createdat
                            (double)(row[userTable.Columns[7]]) //score
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

        public void InsertComment(Comment comment)
        {
            SqlConnection sql = new SqlConnection(_connectionString);
            string query = @"INSERT INTO Comments (UUID, SenderUUID, Content, CreatedAt, PostUUID)
                         VALUES (@UUID, @SenderUUID, @Content, @CreatedAt, @PostUUID)";
            SqlCommand cmd = new SqlCommand(query, sql);

            cmd.Parameters.AddWithValue("@UUID", comment.UUID.ToByteArray());
            cmd.Parameters.AddWithValue("@SenderUUID", comment.SenderUUID.ToByteArray());
            cmd.Parameters.AddWithValue("@Content", comment.Content);
            cmd.Parameters.AddWithValue("@CreatedAt", comment.CreatedAt);
            cmd.Parameters.AddWithValue("@PostUUID", comment.PostUUID.ToByteArray());

            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
        }
      
        public void InsertPost(Post post)
        {
            SqlConnection sql = new SqlConnection(_connectionString);
            string query = @"INSERT INTO Posts (UUID, PosterUUID, Content, CreatedAt, Title, Pinned)
                         VALUES (@UUID, @PosterUUID, @Content, @CreatedAt, @Title, @Pinned)";
            SqlCommand cmd = new SqlCommand(query, sql);

            cmd.Parameters.AddWithValue("@UUID", post.UUID.ToByteArray());
            cmd.Parameters.AddWithValue("@PosterUUID", post.PosterUUID.ToByteArray());
            cmd.Parameters.AddWithValue("@Content", post.Content);
            cmd.Parameters.AddWithValue("@CreatedAt", post.CreatedAt);
            cmd.Parameters.AddWithValue("@Title", post.Title);
            cmd.Parameters.AddWithValue("@Pinned", post.Pinned);

            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
        }

        public void InsertTablature(Tablature tab)
        {
            SqlConnection sql = new SqlConnection(_connectionString);
            string query = @"INSERT INTO Tablatures (UUID, PosterUUID, SongName, ArtistName, AlbumName, Content, TuningType, Difficulty, CreatedAt, Score, ScoreCount, ReleaseYear, Capo)
                         VALUES (@UUID, @PosterUUID, @SongName, @ArtistName, @AlbumName, @Content, @TuningType, @Difficulty, @CreatedAt, @Score, @ScoreCount, @ReleaseYear, @Capo)";
            SqlCommand cmd = new SqlCommand(query, sql);

            cmd.Parameters.AddWithValue("@UUID", tab.UUID.ToByteArray());
            cmd.Parameters.AddWithValue("@PosterUUID", tab.PosterUUID.ToByteArray());
            cmd.Parameters.AddWithValue("@SongName", tab.SongName);
            cmd.Parameters.AddWithValue("@ArtistName", tab.ArtistName);
            cmd.Parameters.AddWithValue("@AlbumName", tab.AlbumName);
            cmd.Parameters.AddWithValue("@Content", tab.Content);
            cmd.Parameters.AddWithValue("@TuningType", tab.TuningType);
            cmd.Parameters.AddWithValue("@Difficulty", tab.Difficulty);
            cmd.Parameters.AddWithValue("@CreatedAt", tab.CreatedAt);
            cmd.Parameters.AddWithValue("@Score", tab.Score);
            cmd.Parameters.AddWithValue("@ScoreCount", tab.ScoreCount);
            cmd.Parameters.AddWithValue("@ReleaseYear", tab.ReleaseYear);
            cmd.Parameters.AddWithValue("@Capo", tab.Capo);

            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
        }

        public Post GetPost(string uuid)
        {
            try
            {
                return GetPost(Guid.Parse(uuid));
            }
            catch
            {
                return null;
            }
        }
        public Post GetPost(Guid uuid)
        {
            var parameters = new Dictionary<string, object>
    {
        { "@UUID", uuid.ToByteArray() }
    };

            Post[] results = GetPosts("UUID = @UUID", parameters);
            return results.Length > 0 ? results[0] : null;
        }
        public Post[] GetPosts(User owner)
        {
            return GetPosts(owner.UUID);
        }
        public Post[] GetPosts(Guid poster)
        {
            var parameters = new Dictionary<string, object>
            {
        { "@PosterUUID", poster.ToByteArray() }
    };

            return GetPosts("PosterUUID = @PosterUUID", parameters);
        }

        public Post[] GetPosts(string query)
        {
            var parameters = new Dictionary<string, object>
            {
        { "@QUERY", $"%{query}%" }
    };

            return GetPosts("Title LIKE @QUERY OR Content LIKE @QUERY", parameters);
        }

        public Post[] GetPosts()
        {
            return GetPosts(null, null);
        }
        private Post[] GetPosts(string whereClause, Dictionary<string, object> parameters)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM posts";

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
                    adapter.Fill(dataSet, "Posts");

                    DataTable userTable = dataSet.Tables[0];

                    Post[] posts = new Post[userTable.Rows.Count];

                    for (int i = 0; i < posts.Length; i++)
                    {
                        DataRow row = userTable.Rows[i];

                        posts[i] = new Post(
                            new Guid((byte[])(row[userTable.Columns[0]])), //uuid
                            new Guid((byte[])(row[userTable.Columns[1]])), //posteruuid
                            (string)(row[userTable.Columns[2]]), //content
                            (DateTime)(row[userTable.Columns[3]]), //createdat
                            (string)(row[userTable.Columns[4]]), //title
                            (bool)(row[userTable.Columns[5]]) //pinned
                        );
                    }

                    return posts;
                }
            }

        }

        public Tablature GetTablature(string uuid)
        {
            try
            {
                return GetTablature(Guid.Parse(uuid));
            } catch
            {
                return null;
            }
        }

        public Tablature GetTablature(Guid uuid)
        {
            var parameters = new Dictionary<string, object>
    {
        { "@UUID", uuid.ToByteArray() }
    };

            Tablature[] results = GetTablatures("UUID = @UUID", parameters);
            return results.Length > 0 ? results[0] : null;
        }

        public Tablature[] GetTablatures(string query)
        {
            var parameters = new Dictionary<string, object>
            {
        { "@QUERY", $"%{query}%" }
    };

            return GetTablatures("SongName LIKE @QUERY OR ArtistName LIKE @QUERY OR AlbumName LIKE @QUERY OR Content LIKE @QUERY", parameters);
        }

        public Tablature[] GetTablatures(User poster)
        {
            return GetTablatures(poster.UUID);
        }

        public Tablature[] GetTablatures(Guid poster)
        {
            var parameters = new Dictionary<string, object>
            {
        { "@PosterUUID", poster.ToByteArray() }
    };

            return GetTablatures("PosterUUID = @PosterUUID", parameters);
        }

        public Tablature[] GetTablatures()
        {
            return GetTablatures(null, null);
        }
        private Tablature[] GetTablatures(string whereClause, Dictionary<string, object> parameters)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM tablatures";

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
                    adapter.Fill(dataSet, "Tablatures");

                    DataTable userTable = dataSet.Tables[0];

                    Tablature[] tablatures = new Tablature[userTable.Rows.Count];

                    for (int i = 0; i < tablatures.Length; i++)
                    {
                        DataRow row = userTable.Rows[i];

                        tablatures[i] = new Tablature(
                            new Guid((byte[])(row[userTable.Columns[0]])), //uuid
                            new Guid((byte[])(row[userTable.Columns[1]])), //posteruuid
                            (string)(row[userTable.Columns[2]]), //songname
                            (string)(row[userTable.Columns[3]]), //artistname
                            (string)(row[userTable.Columns[4]]), //albumname
                            (int)(row[userTable.Columns[11]]), // releaseyear
                            (string)(row[userTable.Columns[5]]), //content
                            (string)(row[userTable.Columns[6]]), //tuningtype
                            (string)(row[userTable.Columns[7]]), //difficulty
                            (DateTime)(row[userTable.Columns[8]]), //createdat
                            (double)(row[userTable.Columns[9]]), //score
                            (int)(row[userTable.Columns[10]]), //scorecount
                            (int)(row[userTable.Columns[12]]) //capo
                        );
                    }

                    return tablatures;
                }
            }

        }

        public Comment[] GetComments(Post post)
        {
            return GetComments(post.UUID);
        }

        public Comment[] GetComments(Tablature tab)
        {
            return GetComments(tab.UUID);
        }

        public Comment[] GetComments(Guid postUUID)
        {
            var parameters = new Dictionary<string, object>
            {
        { "@PostUUID", postUUID.ToByteArray() }
    };

            return GetComments("PostUUID = @PostUUID", parameters);
        }

        private Comment[] GetComments(string whereClause, Dictionary<string, object> parameters)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM comments";

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
                    adapter.Fill(dataSet, "Comments");

                    DataTable userTable = dataSet.Tables[0];

                    Comment[] comments = new Comment[userTable.Rows.Count];

                    for (int i = 0; i < comments.Length; i++)
                    {
                        DataRow row = userTable.Rows[i];

                        comments[i] = new Comment(
                            new Guid((byte[])(row[userTable.Columns[0]])), //uuid
                            new Guid((byte[])(row[userTable.Columns[1]])), //senderuuid
                            (string)(row[userTable.Columns[2]]), //content
                            (DateTime)(row[userTable.Columns[3]]), //createdat
                            new Guid((byte[])(row[userTable.Columns[4]])) //postuuid
                        );
                    }

                    return comments;
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

        public readonly DateTime CreatedAt;

        public readonly byte[] token;

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

            CreatedAt = DateTime.Now;

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
            DateTime createdAt,
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
            CreatedAt = createdAt;
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

        public void UpdateScore(Comment comment)
        {
            if (comment == null || !comment.SenderUUID.Equals(UUID))
            {
                return;
            }

            double i = (double)comment.Content.Length / 20;
            i *= ((double)(new Random()).Next(0, 101)) / 100;

            score += (score * i) + i;
        }
    }

    public class Comment
    {
        public readonly Guid UUID;
        public readonly Guid SenderUUID;
        public readonly string Content;
        public readonly DateTime CreatedAt;
        public readonly Guid PostUUID;

        public Comment(
            Guid uuid, 
            Guid senderUUID, 
            string content, 
            DateTime createdAt, 
            Guid postUUID
        )
        {
            UUID = uuid;
            SenderUUID = senderUUID;
            Content = content;
            CreatedAt = createdAt;
            PostUUID = postUUID;
        }

        public Comment(
            Guid senderUUID,
            string content,
            Guid postUUID
        )
        {
            UUID = Guid.NewGuid();
            SenderUUID = senderUUID;
            Content = content;
            CreatedAt = DateTime.Now;
            PostUUID = postUUID;
        }

        public Comment(
            User sender,
            string content,
            Post post
        )
        {
            UUID = Guid.NewGuid();
            SenderUUID = sender.UUID;
            Content = content;
            CreatedAt = DateTime.Now;
            PostUUID = post.UUID;
        }
        public Comment(
            User sender,
            string content,
            Tablature tablature
        )
        {
            UUID = Guid.NewGuid();
            SenderUUID = sender.UUID;
            Content = content;
            CreatedAt = DateTime.Now;
            PostUUID = tablature.UUID;
        }
    }

    public class Post
    {
        public readonly Guid UUID;
        public readonly Guid PosterUUID;
        public readonly string Content;
        public readonly DateTime CreatedAt;
        public readonly string Title;
        public readonly bool Pinned;

        public Post(
            Guid uuid, 
            Guid posterUUID, 
            string content, 
            DateTime createdAt, 
            string title, 
            bool pinned
        )
        {
            UUID = uuid;
            PosterUUID = posterUUID;
            Content = content;
            CreatedAt = createdAt;
            Title = title;
            Pinned = pinned;
        }

        public Post(
            Guid posterUUID,
            string content,
            string title
        )
        {
            UUID = Guid.NewGuid();
            PosterUUID = posterUUID;
            Content = content;
            CreatedAt = DateTime.Now;
            Title = title;
            Pinned = false;
        }

        public Post(
            User poster,
            string content,
            string title
        )
        {
            UUID = Guid.NewGuid();
            PosterUUID = poster.UUID;
            Content = content;
            CreatedAt = DateTime.Now;
            Title = title;
            Pinned = false;
        }
    }
    public class Tablature
    {
        public readonly Guid UUID;
        public readonly Guid PosterUUID;
        public readonly string SongName;
        public readonly string ArtistName;
        public readonly string AlbumName;
        public readonly int ReleaseYear;
        public readonly string Content;
        public readonly string TuningType;
        public readonly string Difficulty;
        public readonly DateTime CreatedAt;
        public readonly double Score;
        public readonly int ScoreCount;
        public readonly int Capo;

        public Tablature(
            Guid uuid,
            Guid posterUUID,
            string songName,
            string artistName,
            string albumName,
            int releaseYear,
            string content,
            string tuningType,
            string difficulty,
            DateTime createdAt,
            double score,
            int scoreCount,
            int capo
        )
        {
            UUID = uuid;
            PosterUUID = posterUUID;
            SongName = songName;
            ArtistName = artistName;
            AlbumName = albumName;
            ReleaseYear = releaseYear;
            Content = content;
            TuningType = tuningType;
            Difficulty = difficulty;
            CreatedAt = createdAt;
            Score = score;
            ScoreCount = scoreCount;
            Capo = capo;
        }

        public Tablature(
            Guid posterUUID,
            string songName,
            string artistName,
            string albumName,
            int releaseYear,
            string content,
            string tuningType,
            string difficulty,
            int capo
        )
        {
            UUID = Guid.NewGuid();
            PosterUUID = posterUUID;
            SongName = songName;
            ArtistName = artistName;
            AlbumName = albumName;
            ReleaseYear = releaseYear;
            Content = content;
            TuningType = tuningType;
            Difficulty = difficulty;
            CreatedAt = DateTime.Now;
            Score = 0;
            ScoreCount = 0;
            Capo = capo;
        }

        public Tablature(
            User poster,
            string songName,
            string artistName,
            string albumName,
            int releaseYear,
            string content,
            string tuningType,
            string difficulty,
            int capo
        )
        {
            UUID = Guid.NewGuid();
            PosterUUID = poster.UUID;
            SongName = songName;
            ArtistName = artistName;
            AlbumName = albumName;
            ReleaseYear = releaseYear;
            Content = content;
            TuningType = tuningType;
            Difficulty = difficulty;
            CreatedAt = DateTime.Now;
            Score = 0;
            ScoreCount = 0;
            Capo = capo;
        }
    }
}