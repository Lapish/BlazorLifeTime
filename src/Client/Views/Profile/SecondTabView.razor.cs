namespace Client.Views.Profile
{
    public partial class SecondTabView
    {
        protected override void OnInitialized()
        {
            ViewModel.ConnectionChanged += () => StateHasChanged();
        }
    }
}