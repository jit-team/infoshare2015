namespace InfoshareDashboard.Client
{
    public interface IHandler
    {
        void Received(InfoshareDashboard.Models.Message message);
    }
}