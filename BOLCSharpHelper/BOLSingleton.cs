using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BugsonlineLIB
{
    public class BOLSingleton
    {
        private static BOLSingleton instance;
        
        private static string username = "";
        private static string password = "";

        private BOLSingleton()
        {

        }

        public static BOLSingleton getInstance()
        {
            return BOLSingleton.getInstance(BOLSingleton.username, BOLSingleton.password);
        }

        public static BOLSingleton getInstance(string username, string password)
        {
            if(!username.Equals("") && !password.Equals(""))
            {
                BOLSingleton.username = username;
                BOLSingleton.password = password;
                return BOLSingleton.Instance();
            }
            else
            {
                throw new Exception("Username and Password cannot be '' or null!");
            }
            
        }

        private static BOLSingleton Instance()
        {
            if (instance == null)
            {
                instance = new BOLSingleton();
            }
            return instance;
        }

        public async void Send(Exception ex)
        {
            BOLBug bug = new BOLBug();
            bug.AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            bug.AppVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            bug.MachineName = Environment.MachineName;
            var vector = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim().Split(" ", StringSplitOptions.None);
            List<string> list = new List<string>(vector);
            bug.OS = string.Join(" ", list.GetRange(0, list.Count - 1));
            bug.OSVersion = list[list.Count - 1];

            bug.BugDate = DateTime.Now;
            bug.Description = ex.Message;
            bug.StackTrace = ex.StackTrace;
            bug.ErrNumber = ex.HResult;

            HttpResponseMessage response = null;
            HttpClient client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://api.bugsonline.biz/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.ConnectionClose = true;

                //Set Basic Auth
                var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

                //Se dà errore che non trova PostAsJsonAsync
                //Installare da Nuget: Microsoft.AspNet.WebApi.Client
                response =  await client.PostAsJsonAsync<BOLBug>("api/send", bug);
                Console.WriteLine("Server response: " + response.ReasonPhrase);
                string text = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Console.WriteLine("Server content: " + text);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception exception)
            {
                Console.WriteLine("ERROR: " + exception.Message);
            }
        }
    }

    public class BOLBug
    {
        private string _AppName = "";
        private string _AppVersion = "";
        private string _MachineName = "";
        private string _Token = "";

        private int _ErrNumber = 0;
        private string _Description = "";
        private string _StackTrace = "";
        private DateTime _BugDate;

        private string _Routine = "";
        private string _OS = "";
        private string _OSVersion = "";

        public string AppName { get => _AppName; set => _AppName = value; }
        public string AppVersion { get => _AppVersion; set => _AppVersion = value; }
        public string MachineName { get => _MachineName; set => _MachineName = value; }
        public string Token { get => _Token; set => _Token = value; }
        public int ErrNumber { get => _ErrNumber; set => _ErrNumber = value; }
        public string Description { get => _Description; set => _Description = value; }
        public string StackTrace { get => _StackTrace; set => _StackTrace = value; }
        public DateTime BugDate { get => _BugDate; set => _BugDate = value; }
        public string Routine { get => _Routine; set => _Routine = value; }
        public string OS { get => _OS; set => _OS = value; }
        public string OSVersion { get => _OSVersion; set => _OSVersion = value; }
    }
}
