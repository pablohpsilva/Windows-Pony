using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWindowsPony.Core.Control
{
    public class MyBackgroundWorker
    {
        BackgroundWorker bw = new BackgroundWorker();
        ShellToast toast = new ShellToast();

        public MyBackgroundWorker()
        {
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string content = "hello: ";

            for (int i = 1; (i <= 10); i++)
            {
                if ((worker.CancellationPending == true))
                {
                    content = "CancellationPending";
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(500);
                    worker.ReportProgress((i * 10));
                }
                content += i;
            }
            toast.Title = "Background Agent Sample";
            toast.Content = content;
            toast.Show();
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toast.Title = "ProgressChanged";
            toast.Content = (e.ProgressPercentage.ToString() + "%");
            toast.Show();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toast.Title = "RunWorkedCompleted";
            if ((e.Cancelled == true))
                toast.Content = "Canceled!";
            else if (!(e.Error == null))
                toast.Content = ("Error: " + e.Error.Message);
            else
                toast.Content = "Done!";
            toast.Show();
            bw.RunWorkerAsync();
        }
    }
}
