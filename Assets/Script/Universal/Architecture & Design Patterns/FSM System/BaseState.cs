namespace MyStudio.Core.Architecture
{
    
    public abstract class BaseState<T>
    {
        protected T Controller; 

      
        public BaseState(T controller)
        {
            this.Controller = controller;
        }

        public virtual void Enter() { }
        public virtual void Tick() { }
        public virtual void Exit() { }
    }
}