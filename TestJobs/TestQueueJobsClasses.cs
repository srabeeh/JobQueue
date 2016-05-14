﻿using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobQueue;

namespace TestJobs
{
    [TestClass]
    public class TestJobsandQueue
    {

        [TestMethod]
        public void SimpleJobQueueTest()
        {
           
            QueueOfJobs pendingJobQueue = SetupTestJobQueue();

            // Refactor the thread setups outside the test...
            var queueProcessingThreads = new Thread[2];

            // Create a delegate to be called when a thread is created and started
            ThreadStart runJobs = delegate
            {
                while (pendingJobQueue.JobsQueue.Count > 0)
                {
                    pendingJobQueue.JobsQueue.Dequeue().Process();
                }
            };


            // create the threads and assign the delegate and start
            for (int i = 0; i < queueProcessingThreads.Length; i++)
            {
                queueProcessingThreads[i] = new Thread(runJobs);
                queueProcessingThreads[i].Start();
            }

            // Join blocks a calling thread until a thread is completed
            foreach (Thread thread in queueProcessingThreads)
            {
                thread.Join();
            }

            Assert.IsFalse(queueProcessingThreads.Length == 0);
        }


        public QueueOfJobs SetupTestJobQueue ()
        {
            QueueOfJobs Q = new QueueOfJobs();

            for (int jobId = 0; jobId < 10000; jobId++)
            {
                Q.JobsQueue.Enqueue(new Job(jobId));
            }

            return Q;
        }
    }
}