namespace Flush.FSM
{
    public interface IState
    {
        public void Tick();
        public System.Action OnEnter { get; set; }
        public System.Action OnExit { get; set; }
    }    
}
