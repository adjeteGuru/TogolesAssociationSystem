using TogoleseSolidarity.Core.ServiceProvider.Interfaces;

namespace TogoleseSolidarity.Core.ServiceProvider;

public class AlertService : IAlertService
{
    public event Action<string> OnAlert;

    public void ShowAlert(string message)
    {
        OnAlert?.Invoke(message);
    }      
}
