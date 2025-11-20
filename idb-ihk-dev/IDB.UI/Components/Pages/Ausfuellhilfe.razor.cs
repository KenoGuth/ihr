using IDB.Model;

namespace IDB.UI.Components.Pages
{
    using IDB.Business;
    public partial class Ausfuellhilfe
    {
        
        private List<Model.Ausfuellhilfe> ausfuellhilfen = new List<Model.Ausfuellhilfe>();
        private bool IsLoading = true;
        private bool ShowDelayedMessage = false;
        private bool showDeleteModal = false;
        private string? responseMessage;
        private bool isSuccess = false;
        private int toDelete = 0;
        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            ShowDelayedMessage = false;

            // Starte verzögerte Anzeige für den Hinweistext nach 5 Sekunden
            _ = Task.Run(async () =>
            {
                await Task.Delay(5000);
                if (IsLoading)
                {
                    ShowDelayedMessage = true;
                    await InvokeAsync(StateHasChanged);
                }
            });

            await Task.Delay(1);
            
            Business obj = new Business();
            ausfuellhilfen = obj.Get_All_Ausfuellhilfe(appState.APIurl);

            IsLoading = false;
        }
      
        private void NavigateToCreate()
        {
            navi.NavigateTo($"/ausfuellhilfe/neu/");
        }

        private void navigateToEdit(int id)
        {
            navi.NavigateTo($"/ausfuellhilfe/bearbeiten/{id}");
        }
        private void ShowDeleteConfirmation(int id)
        {
            showDeleteModal = true;
            toDelete = id;
        }
        private void HideDeleteConfirmation()
        {
            showDeleteModal = false;
            toDelete = 0;
        }
        private void ClearMessage()
        {
            responseMessage = null;
        }
        private async Task DeleteRecord()
        {
           

            
           
            await InvokeAsync(StateHasChanged);

            try
            {
                Business obj = new Business();

                bool result = obj.Delete_Ausfuellhilfe(appState.APIurl, toDelete);

                if (result)
                {
                    responseMessage = "Datensatz erfolgreich gelöscht.";
                    isSuccess = true;
                   
                }
                else
                {
                    responseMessage = "Fehler beim Löschen des Datensatzes.";
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                responseMessage = $"Fehler beim Löschen: {ex.Message}";
                isSuccess = false;
            }
            finally
            {
                HideDeleteConfirmation();
                await InvokeAsync(StateHasChanged);
            }
        }
    }
}
