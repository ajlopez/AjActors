using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjActors.Tests
{
    public class Counter
    {
        public int Count { get; set; }

        public void Increment()
        {
            this.Count = this.Count + 1;
        }
    }
}
