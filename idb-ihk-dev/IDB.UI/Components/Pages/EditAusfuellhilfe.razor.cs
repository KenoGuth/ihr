using IDB.Model;
using Microsoft.AspNetCore.Components;
using IDB.Business;
namespace IDB.UI.Components.Pages
{
    public partial class EditAusfuellhilfe
    {

       
       [Parameter] public int chosenId { get; set; }
        private List<Model.Ausfuellhilfe> ausfuellhilfen = new List<Model.Ausfuellhilfe>();
        private Model.Ausfuellhilfe ausfuellhilfe;
        private List<Model.AusfuellhilfeItem> ausfuellhilfeItems = new ();
        private bool IsLoading = true;
        private bool ShowDelayedMessage = false;
        private bool isProcessing = false;
        private bool isUpdated = false;
        private bool isDeleted = false;
        private bool isInserting = false;
        private string responseMessage = string.Empty;
        private bool isSuccess = false;
        private Dictionary<string, string> validationErrors = new Dictionary<string, string>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
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

            Business.Business obj = new();
            ausfuellhilfen = obj.Get_All_Ausfuellhilfe(appState.APIurl);

            ausfuellhilfe = ausfuellhilfen.FirstOrDefault(x => x.Id == chosenId);
            if (ausfuellhilfe == null) { navi.NavigateTo("/ausfuellhilfe/neu"); }
            ausfuellhilfeItems = obj.Get_All_AusfuellhilfeItems(appState.APIurl);
            ausfuellhilfeItems = ausfuellhilfeItems.Where(x => x.Id_ausfuellhilfe == chosenId).ToList();
            IsLoading = false;
        }

        private async Task UpdateAusfuellhilfe()
        {
            validationErrors.Clear();
      

        isProcessing = true;
            try
            {

                Business.Business obj = new Business.Business();
                ausfuellhilfe.Id = chosenId;
                isUpdated  = obj.Update_Ausfuellhilfe(appState.APIurl, ausfuellhilfe);




                if (isUpdated)
                {
                    foreach (var item in ausfuellhilfeItems)
                    {
                        item.Id_ausfuellhilfe = chosenId;
                        isInserting = obj.Insert_AusfuellhilfeItem(appState.APIurl, item);
                        if (!isInserting)
                        {
                            isSuccess = false; break;
                        }
                    }
                }

                isProcessing = false;
            }
            catch (Exception ex)
            {
                responseMessage = $"Fehler beim Erstellen der Ausfüllhilfe: {ex.Message}";
                isSuccess = false;
            }
            finally
            {
                isProcessing = false;
            }
        }

        private void AddNewItem() => ausfuellhilfeItems.Add(new AusfuellhilfeItem());
        private void RemoveItem(int index)
        {
            if (index >= 0 && index < ausfuellhilfeItems.Count)
            {
                ausfuellhilfeItems.RemoveAt(index);
            }
        }

        private void MoveItemUp(int index)
        {
            if (index > 0)
            {
                var item = ausfuellhilfeItems[index];
                ausfuellhilfeItems.RemoveAt(index);
                ausfuellhilfeItems.Insert(index - 1, item);
            }
        }

        private void MoveItemDown(int index)
        {
            if (index < ausfuellhilfeItems.Count - 1)
            {
                var item = ausfuellhilfeItems[index];
                ausfuellhilfeItems.RemoveAt(index);
                ausfuellhilfeItems.Insert(index + 1, item);
            }
        }

        private void MoveItemToTop(int index)
        {
            if (index > 0)
            {
                var item = ausfuellhilfeItems[index];
                ausfuellhilfeItems.RemoveAt(index);
                ausfuellhilfeItems.Insert(0, item);
            }
        }

        private void MoveItemToBottom(int index)
        {
            if (index < ausfuellhilfeItems.Count - 1)
            {
                var item = ausfuellhilfeItems[index];
                ausfuellhilfeItems.RemoveAt(index);
                ausfuellhilfeItems.Add(item);
            }
        }

        private async void ResetForm()
        {
            ClearMessage();
            await LoadData(); // Lädt die ursprünglichen Daten neu
        }

        private void NavigateBack() => navi.NavigateTo("/ausfuellhilfen");
        private void ClearMessage()
        {
            responseMessage = string.Empty;
            validationErrors.Clear();
        }

        private void Validate()
        {
            validationErrors.Clear();
            if (string.IsNullOrWhiteSpace(ausfuellhilfe.Titel))
            {
                validationErrors["titel"] = "Der Titel darf nicht leer sein.";
            }
        }

        private string GetValidationClass(string fieldName) => validationErrors.ContainsKey(fieldName) ? "is-invalid" : "";
    }
}

