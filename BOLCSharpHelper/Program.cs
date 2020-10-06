using BugsonlineLIB;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BOLTestAPI
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Press a key to send bug..");
            Console.ReadKey();

            //First time setup istance
            //Replace with your username and password!
            //BOLSingleton ben = BOLSingleton.getInstance("YOUR@USERNAME", "YOURPASSWORD");
            BOLSingleton ben = BOLSingleton.getInstance("demo@demo.com", "cicci0CICCI0_");

            //It's a singleton, next times you can obtain without login e pwd.
            BOLSingleton ben2 = BOLSingleton.getInstance();
            int c = 0;
            try
            {
                int g = 5 / c;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Sending bug at " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
                ben2.Send(ex);
                Console.WriteLine("Bug sent at " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
            }
            finally
            {
                Console.WriteLine("Done!");
                Console.ReadKey();
            }
            
        }
    }
}
