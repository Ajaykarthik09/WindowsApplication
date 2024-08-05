using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ReportService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Exitcode = HostFactory.Run(x =>
            {
                x.Service<Report>(s =>
                {
                    s.ConstructUsing(Report => new Report());
                    s.WhenStarted(Report => Report.start());
                    s.WhenStopped(Report => Report.stop());

                });
                x.RunAsLocalSystem();
                x.SetServiceName("Reportgeneration");
                x.SetDisplayName("Report Generation");
                x.SetDescription("This will generate the reports");

            });
            int Exitcodeval = (int)Convert.ChangeType(Exitcode,Exitcode.GetType());
            Environment.ExitCode = Exitcodeval;
                
        }
    }
}
