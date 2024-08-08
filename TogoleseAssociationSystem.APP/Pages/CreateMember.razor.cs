using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.ServiceProvider;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class CreateMemberComponent : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IMemberService MemberService { get; set; }
        public EditContext EditContext { get; set; }

        public MemberToAdd  Member { get; set; }
        protected override void OnInitialized()
        {
            Member = new MemberToAdd();
            EditContext = new EditContext(Member);
        }

        protected void NavigateToHome()
        {
            Navigation.NavigateTo("/memberlist");
        }

        protected async Task Submit()
        {
            var result = await MemberService.CreateMemberAsync(Member);
            if (result == null)
            {
                return;
            }
            Navigation.NavigateTo("/memberlist");
        }

        protected async Task OnInputFileChanged(InputFileChangeEventArgs args)
        {
            var memoryStream = new MemoryStream();
            await args.File.OpenReadStream().CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            var imageFile = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            //Member.PhotoUrl = bytes;

            //InputFileMessage = args.File.Name;
            //await OnFileChanged.InvokeAsync(args);
            //if (File.ContentType == "image/png")
            //{
            //    File = await File.RequestImageFileAsync("image/png", 400, 400);
            //}

            //ErrorMessage = "Please choose the right format.";
        }
    }
}
