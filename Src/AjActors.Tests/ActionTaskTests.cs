using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjActors.Tests
{
    [TestClass]
    public class ActionTaskTests
    {
        [TestMethod]
        public void CreateAndExecute()
        {
            Counter counter = new Counter();

            ActionTask<Counter> action = new ActionTask<Counter>(counter, c => { c.Increment(); });

            Assert.AreEqual(0, counter.Count);
            action.Execute();
            Assert.AreEqual(1, counter.Count);
        }
    }
}
