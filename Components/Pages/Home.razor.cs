using Microsoft.AspNetCore.Components;
using SHPEGPCActionItemsDashboard.Models;
using SHPEGPCActionItemsDashboard.DataAccess;
namespace SHPEGPCActionItemsDashboard.Components.Pages
{
    public partial class Home
    {
        [Inject] public DatabaseServices DatabaseServices { get; set; } = default!;
        public List<ActionItems> actionItemsList = new List<ActionItems>();
        public List<BoardMembers> boardMembersList = new List<BoardMembers>();
        private bool showBoardMembersListModal { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            actionItemsList = await DatabaseServices.LoadActionItemsListAsync();
            boardMembersList = await DatabaseServices.LoadBoardMembersListAsync();
        }
        public void OpenBoardMembersListModal()
        {
            Console.WriteLine("Clicked: True");
            showBoardMembersListModal = true;
        }
        public void CloseBoardMembersListModal()
        {
            Console.WriteLine("Clicked: False");
            showBoardMembersListModal = false;
        }
    }
}

