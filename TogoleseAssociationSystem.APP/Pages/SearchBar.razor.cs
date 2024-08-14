using Microsoft.AspNetCore.Components;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class SearchBarComponent : ComponentBase
    {
        protected string Filter;

        [Parameter]
        public EventCallback<string> OnSearch { get; set; }

        public void HandleSearch()
        {
            OnSearch.InvokeAsync(Filter);
        }
    }
}
