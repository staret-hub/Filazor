using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filazor.Core.Data
{
    public class FileDeleteEventNotifyService
    {
        public event EventHandler<EventArgs> FileDeleteCompletedEvent;


        public void Notify(object name, params dynamic[] args)
        {
            Console.WriteLine("Notify");
            if (FileDeleteCompletedEvent != null)
            {
                Console.WriteLine("FileDeleteCompletedEvent?.Invoke");
                FileDeleteCompletedEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
