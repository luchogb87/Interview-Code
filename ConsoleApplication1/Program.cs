using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    class Program
    {
        static void Main(string[] args)
        {
            JobLogger log = new JobLogger(true, false, false);
    
            try
            {
                // Getting an error to invoke log.LogMessage.
                int a = 1;
                int b = 0;
                int c = a / b;
            }
            catch (Exception ex)
            {
                log.LogMessage(ex.Message, false, false, true); //Este es una prueba de error.
            }
        }

  

    }
}
