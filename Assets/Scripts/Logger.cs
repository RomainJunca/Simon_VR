using System.IO;
using UnityEngine;

namespace Simon_VR.Assets.Scripts
{
    public class Logger : MonoBehaviour {

        private Logger logger;

        public void Start()
        {

        }

        public void Update(){

            if(Input.GetKeyDown(KeyCode.L)){
                log();
            }
        }

        private Logger()
        {
            
        }

        /*public Logger getLogger()
        {
            
        }*/

        private void log()
        {
            string path = @"D:\MyTest.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Nik");
                    sw.WriteLine("Ta");
                    sw.WriteLine("Mère");
                }
            }

            // Open the file to read from.
            /*using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }*/
        }
    }   
}