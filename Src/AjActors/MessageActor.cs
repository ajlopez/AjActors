namespace AjActors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MessageActor<T> : BaseActor
    {
        private IMessageProcessor<T> processor;

        public MessageActor(IMessageProcessor<T> processor)
        {
            this.processor = processor;
        }

        public void Send(T message)
        {
            this.Send(new ActionTask<IMessageProcessor<T>>(this.processor, p => p.Process(message)));
        }
    }
}

