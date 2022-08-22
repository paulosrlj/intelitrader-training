using Newtonsoft.Json;
using RegisterUserXamarinApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RegisterUserXamarinApp.Services
{
    public class UserService
    {
        const string URL = "http://10.0.2.2:5000/api/users";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "Application/json");
            client.DefaultRequestHeaders.Add("Connection", "close");
            return client;
        }

        public async Task CreateUser(User user)
        {
            string dados = URL;
            string json = JsonConvert.SerializeObject(user);
            HttpClient client = GetClient();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(dados, content);

        }
    }
}
