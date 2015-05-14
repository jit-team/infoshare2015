namespace InfoshareDashboard.Client
{
    interface IHandler
    {
        void Received(InfoshareDashboard.Models.Message message);
    }
}