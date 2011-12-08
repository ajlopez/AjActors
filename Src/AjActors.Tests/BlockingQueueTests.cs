using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace AjActors.Tests
{
    [TestClass]
    public class BlockingQueueTests
    {
        [TestMethod]
        public void CreateAndUseBlockingQueue()
        {
            BlockingQueue<int> queue = new BlockingQueue<int>(1);

            Thread thread = new Thread(new ThreadStart(delegate() { queue.Enqueue(1); }));
            thread.Start();

            int element = queue.Dequeue();
            Assert.AreEqual(1, element);
        }

        [TestMethod]
        public void CreateAndUseBlockingQueueTenTimes()
        {
            BlockingQueue<int> queue = new BlockingQueue<int>(5);

            Thread thread = new Thread(new ThreadStart(delegate() { for (int k=1; k<=10; k++) queue.Enqueue(k); }));
            thread.Start();

            for (int j = 1; j <= 10; j++)
            {
                int element = queue.Dequeue();
                Assert.AreEqual(element, j);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfZeroInConstructor()
        {
            new BlockingQueue<string>(0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfNegativeNumberInConstructor()
        {
            new BlockingQueue<string>(-1);
        }
    }
}

