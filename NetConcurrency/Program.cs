using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobQueue;

namespace NetConcurrency
{
    // ConcurrentDictionary<TKey, TValue>
    // ConcurrentQueue<T>
    // ConcurrentStack<T>
    // ConcurrentBag<T> - Unordered collection.


    /// <summary>
    /// No locking is performed and Dequeue is replaced with TryDequeue
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueue<Job> pendingJobs = new ConcurrentQueue<Job>();
            for (int jobId = 0; jobId < 10000; jobId++)
            {
                pendingJobs.Enqueue(new Job(jobId));
            }

            Thread[] processingThreads = new Thread[2];

            ThreadStart runJobs = delegate()
            {
                Job job = null;

                while (pendingJobs.TryDequeue(out job))
                {
                    job.Process();
                }
            };

            for (int i = 0; i < processingThreads.Length; i++)
            {
                processingThreads[i] = new Thread(runJobs);
                processingThreads[i].Start();
            }

            foreach (Thread thread in processingThreads)
            {
                thread.Join();
            }
        }
    }
}
