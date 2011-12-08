namespace AjActors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActionTask<T> : ITask
    {
        T target;
        private Action<T> action;

        public ActionTask(T target, Action<T> action)
        {
            this.target = target;
            this.action = action;
        }

        public void Execute()
        {
            this.action(this.target);
        }
    }
}
