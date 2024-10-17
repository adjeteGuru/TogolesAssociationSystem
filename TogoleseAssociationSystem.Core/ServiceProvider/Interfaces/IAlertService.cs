using Abp.Web.Mvc.Alerts;

namespace TogoleseAssociationSystem.Core.ServiceProvider.Interfaces
{
    public interface IAlertService
    {
        event Action<string> OnAlert;

        void ShowAlert(string message);      
    }
}