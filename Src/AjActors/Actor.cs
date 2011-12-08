namespace AjActors
{
    using System;
    using System.Threading;

    public class Actor<T> : BaseActor
    {
        private T instance;

        public Actor(T instance)
        {
            this.instance = instance;
        }

        public void Send(Action<T> action)
        {
            this.Send(new ActionTask<T>(this.instance, action));
        }
    }
}
