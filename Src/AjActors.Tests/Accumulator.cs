using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AjActors.Tests
{
    public class Accumulator : IMessageProcessor<int>
    {
        private int trigger;
        private EventWaitHandle handle;

        public Accumulator(int trigger, EventWaitHandle handle)
        {
            this.trigger = trigger;
            this.handle = handle;
        }

        public int Result { get; set; }

        public void Process(int message)
        {
            this.Result = this.Result + message;

            if (this.Result >= trigger)
                handle.Set();
        }
    }
}

