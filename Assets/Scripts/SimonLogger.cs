using System;
using System.IO;

namespace Simon_VR.Assets.Scripts
{
    public sealed class SimonLogger {

        private static readonly Lazy<SimonLogger> _lazy = new Lazy<SimonLogger>(() => new SimonLogger());

        private SimonLogger()
        {
            // Do code when 1st init

            // fill fileName with current date and hour 
            this.checkFileSuffix();
        }

        public static SimonLogger logger { get { return _lazy.Value; }}

        // TODO Change the path to be relative
        private String path = @"D:\";

        public String fileName = "ProjetRecherche";

        public String fileSuffix = "";

        public String extension = ".txt";

        /// <summary>
        /// write
        /// 
        /// This method writes a message into the text file specified before
        /// </summary>
        /// <param name="message"></param>
        public void write(String message)
        {
            // TODO We are using appendAllText but we should consider a fatest function that requires less disk access 
            
            // Check the file suffix to avoid writing a file without suffix
            this.checkFileSuffix();

            String filePath = this.path + this.fileName + this.fileSuffix + this.extension;
            File.AppendAllText(filePath, message + Environment.NewLine);
        }

        /// <summary>
        /// checkFileSuffix
        /// 
        /// This methods check the file suffix and if it is empty then it add the current date time.
        /// </summary>
        private void checkFileSuffix()
        {
            if (this.fileSuffix.Length == 0) {
                this.fileSuffix = DateTime.Now.ToString(@"d-M-yyyy-hh-mm-ss");
            }
        }

        /// <summary>
        /// createNewFile
        /// 
        /// This methods allows to change the file the logger is writing into.
        /// </summary>
        /// <param name="fileName">The new fileName (this parameter is optional)</param>
        public void createNewFile(String fileName = "")
        {
            if (fileName.Length > 0) {
                this.fileName = fileName;
            }
            this.fileSuffix = DateTime.Now.ToString(@"d-M-yyyy-hh-mm-ss");
        }
    }   
}