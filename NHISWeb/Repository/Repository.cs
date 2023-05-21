//using Newtonsoft.Json;
//using NHISWeb.Repository.IRepository;
//using System.Text;
////using System.Net.Http;
//namespace NHISWeb.Repository
//{
//    public class Repository<T> : IRepository<T> where T : class
//    {//To make http call
//        private readonly IHttpClientFactory _clientFactory;
//        public Repository(IHttpClientFactory clientFactory)
//        {
//            _clientFactory = clientFactory;
//        }

//        public async Task<bool> CreateAsync(string Url, T objToCreate)
//        {
//            var request = new HttpRequestMessage(HttpMethod.Post, Url);
//            if(objToCreate != null)
//            {
//                request.Content = new StringContent(
//                    JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/Json");
//            }
//            else
//            {
//                return false;
//            }
//            var client = _clientFactory.CreateClient();
//            HttpResponseMessage response = await client.SendAsync(request);
//            if(response.StatusCode == System.Net.HttpStatusCode.Created)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public async Task<bool> DeleteAsync(string Url, int Id)
//        {
//            var request = new HttpRequestMessage(HttpMethod.Delete, Url + Id);
//            var client = _clientFactory.CreateClient();
//            HttpResponseMessage response = await client.SendAsync(request);
//            if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
//            {
//                return true;
//            }
            
//                return false;   
//        }

//        public async Task<IEnumerable<T>> GetAllAsync(string Url)
//        {
//            var request = new HttpRequestMessage(HttpMethod.Get, Url);
//            var client = _clientFactory.CreateClient();
//            HttpResponseMessage response = await client.SendAsync(request);
//            if (response.StatusCode == System.Net.HttpStatusCode.OK)
//            {
//                var jsonString = await response.Content.ReadAsStringAsync();
//                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
//            }

//            return null;
//        }

//        public async Task<T> GetByIdAsync(string Url, int Id)
//        {
//            var request = new HttpRequestMessage(HttpMethod.Get, Url);
//            var client = _clientFactory.CreateClient();
//            HttpResponseMessage response = await client.SendAsync(request);
//            if (response.StatusCode == System.Net.HttpStatusCode.OK)
//            {
//                var jsonString = await response.Content.ReadAsStringAsync();
//                return JsonConvert.DeserializeObject<T>(jsonString);
//            }

//            return null;
//        }

//        public async Task<bool> UpdateAsync(string Url, T objToUpdate)
//        {
//            var request = new HttpRequestMessage(HttpMethod.Patch, Url);
//            if (objToUpdate != null)
//            {
//                request.Content = new StringContent(
//                    JsonConvert.SerializeObject(objToUpdate), Encoding.UTF8, "application/Json");
//            }
//            else
//            {
//                return false;
//            }
//            var client = _clientFactory.CreateClient();
//            HttpResponseMessage response = await client.SendAsync(request);
//            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//    }
//}
