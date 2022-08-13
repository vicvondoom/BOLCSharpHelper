using System;
using System.Globalization;
using System.Threading.Tasks;
using Bugsonline;

namespace BOLTestAPI
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("Press a key to send bug..");
            Console.ReadKey();

            //First time setup istance
            BOLHelper bol_init = BOLHelper.initInstance(
                "demo@demo.com",
                "cicci0CICCI0_",
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                AppType.Console,
                Languages.CSharp);

            //It's a singleton, next times you can obtain without login e pwd.
            BOLHelper bol = BOLHelper.getInstance();
            int c = 0;
            try
            {
                int g = 5 / c;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Sending bug at " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
                await bol.Send(ex);
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
