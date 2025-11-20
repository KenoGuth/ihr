using IDB.Model;

namespace IDB.UI.Components.Pages
{
    using IDB.Business;

    public partial class CreateAusfuellhilfe
    {

        private int selectedTableId = 0;
        private bool IsLoading = true;
        private bool ShowDelayedMessage = false;

        // Verwendung deiner echten Klassen
        private Model.Ausfuellhilfe ausfuellhilfe = new();
        private List<AusfuellhilfeItem> ausfuellhilfeItems = new();
        private Dictionary<string, string> validationErrors = new();
        private string responseMessage = string.Empty;
        private bool isSuccess = false;
        private bool isProcessing = false;
        private bool isInserting = false;
        private int newID = 0;
        Business obj = new Business();


        protected override async Task OnInitializedAsync()
        {
            // Initialisiere mit einem leeren Item
            ausfuellhilfeItems.Add(new AusfuellhilfeItem());
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

            

            IsLoading = false;
        }

        //TODO: Logik für die Order implementieren
        private void MoveColumnUp() { }
        private void MoveColumnToBottom() { }
        private void MoveColumnToTop() { }
        private void MoveColumnDown() { }

        private void AddNewItem()
        {
            ausfuellhilfeItems.Add(new AusfuellhilfeItem());
            StateHasChanged();
        }

        private void RemoveItem(int index)
        {
            if (ausfuellhilfeItems.Count > 1)
            {
                ausfuellhilfeItems.RemoveAt(index);
                StateHasChanged();
            }
        }

        private async Task SaveAusfuellhilfe()
        {
            validationErrors.Clear();

            if (!ValidateForm())
            {
                return;
            }

            isProcessing = true;
            try
            {

                Business obj = new Business();
                newID = obj.Insert_AusfuellhilfeName(appState.APIurl, ausfuellhilfe);

                foreach (var item in ausfuellhilfeItems)
                {
                    item.Id_ausfuellhilfe = newID;
                    isInserting = obj.Insert_AusfuellhilfeItem(appState.APIurl, item);
                    if (!isInserting)
                    {
                        isSuccess = false; break;
                    }

                    await Task.Delay(1000);
                    if (newID > 0)
                    {
                        isSuccess = true;
                        responseMessage = $"Ausfüllhilfe wurde erfolgreich erstellt! Mit der ID: {newID}";
                    }
                    else
                    {
                        isSuccess = false;
                        responseMessage = $"Ausfüllhilfe wurde nicht erstellt! Mit der ID: {newID}";
                    }


                }

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

        private bool ValidateForm()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(ausfuellhilfe.Titel))
            {
                validationErrors["titel"] = "Titel ist erforderlich.";
                isValid = false;
            }

            // Prüfe, ob mindestens ein Element mit Text vorhanden ist
            if (!ausfuellhilfeItems.Any(item => !string.IsNullOrWhiteSpace(item.Text)))
            {
                responseMessage = "Mindestens ein Element mit Text muss vorhanden sein.";
                isSuccess = false;
                isValid = false;
            }

            return isValid;
        }

        private string GetValidationClass(string fieldName)
        {
            return validationErrors.ContainsKey(fieldName) ? "is-invalid" : "";
        }

        private void ResetForm()
        {
            ausfuellhilfe = new Model.Ausfuellhilfe();
            ausfuellhilfeItems.Clear();
            ausfuellhilfeItems.Add(new AusfuellhilfeItem());
            validationErrors.Clear();
            ClearMessage();
        }

        private void NavigateBack()
        {
            navi.NavigateTo("/ausfuellhilfen");
        }

        private void ClearMessage()
        {
            responseMessage = string.Empty;
        }
    }
}