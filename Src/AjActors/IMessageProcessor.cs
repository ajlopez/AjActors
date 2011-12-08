namespace AjActors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IMessageProcessor<T>
    {
        void Process(T message);
    }
}
