using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace PneuMalik.Services
{
    public class PneuB2bService
    {

        public PneuB2bService()
        {

            _statusFilePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "/pneub2b/status.txt");
        }

        public string Status()
        {
            return GetStatus();
        }

        public void ImportStock()
        {

            SetStatus("Začal import skladu a cen");

            Thread.Sleep(20000);

            SetStatus("Import skladu a cen byl ukončen");
        }

        public void ImportAll()
        {


        }

        private string GetStatus()
        {

            return File.ReadAllText(_statusFilePath);
        }

        private void SetStatus(string message)
        {

            File.WriteAllText(_statusFilePath, message);
        }

        private readonly string _statusFilePath;
    }
}