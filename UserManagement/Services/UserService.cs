using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Services
{
    public class UserService
    {
  
        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("UsersDb"));
            var database = client.GetDatabase("UsersDb");
            _users = database.GetCollection<User>("Users");
        }

        public List<User> Get()
        {
            var users = _users.Find(k => true).ToList();

            if(users.Count == 0)
            {
                using (WebClient httpClient = new WebClient())
                {
                    var jsonData = httpClient.DownloadString("https://jsonplaceholder.typicode.com/users");
                    var data = JsonConvert.DeserializeObject<IEnumerable<User>>(jsonData);

                    foreach( var user in data)
                    {
                        Create(user);
                    }
                    users = _users.Find(k => true).ToList();
                }
            }

            return users;
        }

        public User Get(string id)
        {
            return _users.Find<User>(k => k.Id == id).FirstOrDefault();
        }

        public User Create(User user)
        {
            user.Id = null;
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn)
        {
            _users.ReplaceOne(k => k.Id == id, userIn);
        }

        public void Remove(User userIn)
        {
            _users.DeleteOne(k => k.Id == userIn.Id);
        }

        public void Remove(string id)
        {
            _users.DeleteOne(k => k.Id == id);
        }
    }
}
