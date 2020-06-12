using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filazor.Core.Data
{
    public class FileUploadEventNotifyService
    {
        public event EventHandler<EventArgs> FileUploadCompletedEvent;


        public void Notify(object name, params dynamic[] args)
        {
            //Console.WriteLine("Notify");
            if (FileUploadCompletedEvent != null)
            {
                //Console.WriteLine("FileUploadCompletedEvent?.Invoke");
                FileUploadCompletedEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
