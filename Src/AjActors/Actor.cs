namespace AjActors
{
    using System;
    using System.Threading;

    public class Actor<T>
    {
        private T instance;
        private BlockingQueue<ActionTask<T>> queue;
        private bool running;

        public Actor(T instance)
        {
            this.instance = instance;
        }

        public void Send(Action<T> action)
        {
            if (!this.running)
                this.Start();

            this.queue.Enqueue(new ActionTask<T>(this.instance, action));
        }

        private void Start()
        {
            lock (this)
            {
                if (this.running)
                    return;

                this.queue = new BlockingQueue<ActionTask<T>>();

                Thread thread = new Thread(new ThreadStart(this.Execute));
                thread.IsBackground = true;
                thread.Start();

                this.running = true;
            }
        }

        private void Execute()
        {
            while (true)
            {
                try
                {
                    ActionTask<T> task = this.queue.Dequeue();
                    task.Execute();
                }
                catch (Exception ex)
                {
                    // TODO review output, maybe raise an event
                    Console.Error.WriteLine(ex.Message);
                    Console.Error.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
