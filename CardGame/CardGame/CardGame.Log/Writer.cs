using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace CardGame.Log
{
    public class Writer
    {
        static readonly string _baseDirectory = null;

        #region WRITER
        static Writer()
        {
            _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }
        #endregion

        #region LOG ERROR
        public static void LogError(Exception e)
        {
            Trace.WriteLine("ERROR");
            Trace.WriteLine("\t" + e.Message);

            WriteToFile(e);
        }
        #endregion

        #region LOG INFO
        public static void LogInfo(string message)
        {
            Trace.WriteLine("Info");
            Trace.WriteLine("\t" + message);

            WriteToFile(message);
        }
        #endregion

        #region WRITE TO FILE
        private static void WriteToFile(string message)
        {
            try
            {
                string fileName = _baseDirectory + GetFileNameYYYMMDD();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true);
                XElement xel = new XElement("Info",
                                    new XElement("Time", DateTime.Now.ToString()),
                                        new XElement("Info", message)
                               );
                sw.WriteLine(xel);
                sw.Close();
            }
            catch (Exception e)
            {
                LogError(e);
            }
        }
        #endregion

        #region WRITE TO FILE II
        private static void WriteToFile(Exception e)
        {
            try
            {
                string fileName = _baseDirectory + GetFileNameYYYMMDD();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true);
                XElement xel = new XElement("Xcpt",
                                    new XElement("Time", DateTime.Now.ToString()),
                                    new XElement("Exception",
                                        new XElement("Source", e.Source),
                                        new XElement("Message", e.Message),
                                        new XElement("Stack", e.StackTrace)
                                    )
                               );
                if (e.InnerException != null)
                {
                    xel.Element("Exception").Add(
                        new XElement("InnerException",
                            new XElement("Source", e.InnerException.Source),
                            new XElement("Message", e.InnerException.Message),
                            new XElement("Stack", e.InnerException.StackTrace))
                        );
                }
                sw.WriteLine(xel);
                sw.Close();
            }
            catch (Exception we)
            {
                Trace.WriteLine("!!!LOGERROR!!!");
                Trace.WriteLine("\t" + we.Message);
            }

        }
        #endregion

        #region GET FILE NAME YYYYMMDD
        private static string GetFileNameYYYMMDD()
        {
            return System.DateTime.Now.ToString("yyyyMMdd") + "_LOG.xml";
        }
        #endregion
    }
}
