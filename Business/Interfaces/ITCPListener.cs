namespace Business.Interfaces
{
    public interface ITCPListener
    {
        void ListenToPort();

        void AcceptSocket();
    }
}