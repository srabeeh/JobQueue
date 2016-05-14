using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobQueue
{
    public class QueueOfJobs
    {
        public Queue<Job> JobsQueue { get; set; }

        public QueueOfJobs()
        {
             JobsQueue = new Queue<Job>();
        }


    }
}
