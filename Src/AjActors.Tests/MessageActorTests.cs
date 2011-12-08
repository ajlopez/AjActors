using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace AjActors.Tests
{
    [TestClass]
    public class MessageActorTests
    {
        [TestMethod]
        public void CreateAndProcessOneMessage()
        {
            ManualResetEvent handle = new ManualResetEvent(false);

            Accumulator accumulator = new Accumulator(1, handle);
            MessageActor<int> actor = new MessageActor<int>(accumulator);

            actor.Send(1);

            handle.WaitOne();

            Assert.AreEqual(1, accumulator.Result);
        }

        [TestMethod]
        public void CreateAndProcessThreeMessages()
        {
            ManualResetEvent handle = new ManualResetEvent(false);

            Accumulator accumulator = new Accumulator(6, handle);
            MessageActor<int> actor = new MessageActor<int>(accumulator);

            actor.Send(1);
            actor.Send(2);
            actor.Send(3);

            handle.WaitOne();

            Assert.AreEqual(6, accumulator.Result);
        }
    }
}
