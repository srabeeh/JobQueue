using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JobQueue
{
    class Job
    {
        private readonly int _jobId;
        private bool _processed;
        private readonly object _synclock = new object();


        public Job(int jobId)
        {
            _jobId = jobId;
        }

        public void Process()
        {
            lock (_synclock)
            {
                if (_processed)
                {
                    throw new InvalidOperationException($"Job {_jobId} executed multiple times");
                }

                Thread.SpinWait(1000); // Execute thread mimick
                _processed = true;
            }
        }
    }
}
