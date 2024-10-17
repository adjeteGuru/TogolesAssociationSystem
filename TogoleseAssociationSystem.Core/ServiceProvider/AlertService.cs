using TogoleseAssociationSystem.Core.ServiceProvider.Interfaces;

namespace TogoleseAssociationSystem.Core.ServiceProvider
{
    public class AlertService : IAlertService
    {
        public event Action<string> OnAlert;

        public void ShowAlert(string message)
        {
            OnAlert?.Invoke(message);
        }      
    }
}
