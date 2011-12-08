namespace AjActors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public class BlockingQueue<T>
    {
        private Queue<T> queue = new Queue<T>();
        private int maxsize;

        public BlockingQueue()
            : this(100)
        {
        }

        public BlockingQueue(int maxsize)
        {
            if (maxsize <= 0)
                throw new InvalidOperationException("BlockingQueue needs a positive maxsize");

            this.maxsize = maxsize;
        }

        public void Enqueue(T element)
        {
            lock (this)
            {
                while (this.queue.Count >= this.maxsize)
                    Monitor.Wait(this);

                this.queue.Enqueue(element);
                Monitor.PulseAll(this);
            }
        }

        public T Dequeue()
        {
            lock (this)
            {
                while (this.queue.Count == 0)
                    Monitor.Wait(this);

                T element = this.queue.Dequeue();
                Monitor.PulseAll(this);
                return element;
            }
        }
    }
}
