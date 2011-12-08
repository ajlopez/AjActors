namespace AjActors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public class BaseActor
    {
        private BlockingQueue<ITask> queue;
        private bool running;

        public void Send(ITask task)
        {
            if (!this.running)
                this.Start();

            this.queue.Enqueue(task);
        }

        private void Start()
        {
            lock (this)
            {
                if (this.running)
                    return;

                this.queue = new BlockingQueue<ITask>();

                Thread thread = new Thread(new ThreadStart(this.Run));
                thread.IsBackground = true;
                thread.Start();

                this.running = true;
            }
        }

        private void Run()
        {
            while (true)
            {
                try
                {
                    ITask task = this.queue.Dequeue();
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
