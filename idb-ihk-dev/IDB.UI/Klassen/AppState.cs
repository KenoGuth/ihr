namespace IDB.UI
{
  
    public class AppState
    {
        public enum Umgebung { ENTW, TEST, FOBI, PROD }
        public string URLbase { get; private set; }
        public string APIurl { get; private set; }
        public string AUTHurl { get; private set; }
        public string URL_PePo { get; private set; }
        public string URL_Fobi { get; private set; }
        public string URL_ClickOnce { get; private set; }
        public bool Testmode { get; private set; }
        public string PNR { get; set; }
        public string VersionsNummer { get; set; }
        public int ServiceID { get; set; }
        public string Color { get; set; }
        public string? Color2 { get; set; }
        public string IdentString { get; private set; }
        public Umgebung Version { get; private set; }
        public bool onlineLoginControlle { get; set; }
        public bool backdoorPermission { get; set; }
        public AppState()
        {
            //################################
            Version = Umgebung.ENTW;
            //################################

            URLbase = "";
            PNR = "";
            ServiceID = 1;
            VersionsNummer = "In Entwicklung 0.1";

            backdoorPermission = false;

            switch (Version)
            {
                case Umgebung.ENTW:
                    APIurl = "https://pepowebapi-entw.stadt-frankfurt.de/idb/";
                    AUTHurl = "https://pepowebapi-entw.stadt-frankfurt.de/Autorisierungsservice/";
                    URL_PePo = "https://pepo-entw.stadt-frankfurt.de";
                    URL_Fobi = "https://fobibroschuere-online-entw.stadt-frankfurt.de/";
                    URL_ClickOnce = "https://pepo.stadt-frankfurt.de/pepowebstart/PePoWebStart.application?DeployApp=PePo-Entw";
                    Testmode = true;
                    Color = "d68922";
                        //"#128417";
                    Color2 = "#77e677";
                    IdentString = "entw";
                    break;
                case Umgebung.TEST:
                    APIurl = "https://pepowebapi-test.stadt-frankfurt.de/idb/";
                    AUTHurl = "https://pepowebapi-test.stadt-frankfurt.de/Autorisierungsservice/";
                    URL_PePo = "https://pepo-test.stadt-frankfurt.de";
                    URL_Fobi = "https://fobibroschuere-online-fobi.stadt-frankfurt.de/";
                    URL_ClickOnce = "https://pepo.stadt-frankfurt.de/pepowebstart/PePoWebStart.application?DeployApp=PePo-Test";
                    Testmode = true;
                    Color = "#d68922";
                    Color2 = "#9d9d1c";
                    IdentString = "test";
                    break;
                case Umgebung.FOBI:
                    APIurl = "https://pepowebapi-fobi.stadt-frankfurt.de/idb/";
                    AUTHurl = "https://pepowebapi-fobi.stadt-frankfurt.de/Autorisierungsservice/";
                    URL_PePo = "https://pepo-fobi.stadt-frankfurt.de";
                    URL_Fobi = "https://fobibroschuere-online-test.stadt-frankfurt.de/";
                    URL_ClickOnce = "https://pepo.stadt-frankfurt.de/pepowebstart/PePoWebStart.application?DeployApp=PePo-Fobi";
                    Testmode = true;
                    Color = "#7f33c8";
                    Color2 = "#c486e4";
                    IdentString = "fobi";
                    break;
                case Umgebung.PROD:
                    APIurl = "https://pepowebapi.stadt-frankfurt.de/idb/";
                    AUTHurl = "https://pepowebapi.stadt-frankfurt.de/Autorisierungsservice/";
                    URL_PePo = "https://pepo.stadt-frankfurt.de";
                    URL_Fobi = "https://fobibroschuere-online.stadt-frankfurt.de/";
                    URL_ClickOnce = "https://pepo.stadt-frankfurt.de/pepowebstart/PePoWebStart.application?DeployApp=PePo-Prod";
                    Testmode = false;
                    Color = "#00b1eb";
                    Color2 = "#00b1eb";
                    IdentString = "";
                    break;
                default:
                    APIurl = string.Empty;
                    AUTHurl = string.Empty;
                    URL_PePo = string.Empty;
                    URL_Fobi = string.Empty;
                    URL_ClickOnce = string.Empty;
                    Testmode = false;
                    Color = string.Empty;
                    IdentString = "";
                    break;
            }

            onlineLoginControlle = false;
        }
    }
}
