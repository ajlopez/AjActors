using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace AjActors.Tests
{
    [TestClass]
    public class AgentTests
    {
        [TestMethod]
        public void InvokeIncrement()
        {
            ManualResetEvent handle = new ManualResetEvent(false);

            Counter counter = new Counter();
            Actor<Counter> actor = new Actor<Counter>(counter);

            actor.Send(c => { c.Increment(); handle.Set(); });

            handle.WaitOne();

            Assert.AreEqual(1, counter.Count);
        }

        [TestMethod]
        public void InvokeIncrementInAnotherThread()
        {
            ManualResetEvent handle = new ManualResetEvent(false);
            Thread thread = null;

            Counter counter = new Counter();
            Actor<Counter> actor = new Actor<Counter>(counter);

            actor.Send(c => { c.Increment(); thread = Thread.CurrentThread; handle.Set(); });

            handle.WaitOne();

            Assert.AreEqual(1, counter.Count);
            Assert.IsNotNull(thread);
            Assert.AreNotSame(Thread.CurrentThread, thread);
        }

        [TestMethod]
        public void InvokeIncrementTenTimes()
        {
            ManualResetEvent handle = new ManualResetEvent(false);

            Counter counter = new Counter();
            Actor<Counter> actor = new Actor<Counter>(counter);

            actor.Send(c => { for (int k=1; k<=10; k++) c.Increment(); handle.Set(); });

            handle.WaitOne();

            Assert.AreEqual(10, counter.Count);
        }
    }
}
