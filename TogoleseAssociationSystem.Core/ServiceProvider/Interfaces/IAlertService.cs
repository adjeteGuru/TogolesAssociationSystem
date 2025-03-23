namespace TogoleseSolidarity.Core.ServiceProvider.Interfaces;

public interface IAlertService
{
    event Action<string> OnAlert;

    void ShowAlert(string message);
}