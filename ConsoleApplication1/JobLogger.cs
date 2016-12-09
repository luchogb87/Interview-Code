using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    
    public class JobLogger
    {
        #region Variables
        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logToDatabase;
        //private bool _initialized;
        #endregion

        #region Constructor
        public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase)
        {
            _logToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
        }

        #endregion

        #region Public Method
        public void LogMessage(string message, bool bDefault, bool bWarning, bool bError) // Elegir el tipo de mensaje 
        {
            // First : Choose the destination.
            if (!_logToConsole && !_logToFile && !_logToDatabase)
            {
                throw new Exception("Invalid configuration");
            }

            // Second : Choose the message type.
            if (!bDefault && !bWarning && !bError)
            {
                throw new Exception("Error or Warning or Message must be specified");
            }

            if (string.IsNullOrEmpty(message)) { throw new Exception("Message cannot be empty!"); } else { message.Trim(); }

            int messageType = 0;

            if (bDefault)
            {
                messageType = 1;
            }
            if (bError)
            {
                messageType = 2;
            }
            if (bWarning)
            {
                messageType = 3;
            }

            try
            {
                if (_logToDatabase)
                {
                    SaveLogDB(message, messageType);
                }

                if (_logToFile)
                {
                    SaveLogFile(message, messageType);
                }

                if (_logToConsole)
                {
                    SaveLogConsole(message, messageType);
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion

        #region Private Method
        private void SaveLogConsole(string message, int messageType)
        {
            try
            {
                switch (messageType)
                {
                    case 2: //Error
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case 3: //Warning
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case 1: //Default
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    default:
                        break;
                }

                Console.WriteLine(DateTime.Now.ToShortDateString() + message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SaveLogFile(string message, int messageType)
        {
            try
            {
                string fileContent = string.Empty;
                string logPath = System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + 
                    "LogFile" + DateTime.Now.ToString("ddMMyyyy") + ".txt";

                if (System.IO.File.Exists(logPath))
                {
                    fileContent = System.IO.File.ReadAllText(logPath);
                    fileContent = fileContent  + Environment.NewLine +  "[" + DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss") + "]" + " " + message ;
                }
                else
                {
                    fileContent = "[" + DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss") + "]" + " " + message;
                }

                System.IO.File.WriteAllText(logPath, fileContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private void SaveLogDB(string message, int messageType)
        {
            string sCadenaConexion = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(sCadenaConexion);
            try
            {
                connection.Open();
                string sqlSentence = "Insert into Log Values('" + message + "', " + messageType.ToString() + ")";
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(sqlSentence, connection);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        #endregion
    }
}
