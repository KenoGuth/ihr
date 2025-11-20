# IDB Verwaltungssystem

## Projektbeschreibung

Webbasiertes Datenverwaltungssystem zur dynamischen Erstellung und Bearbeitung von IDBs (Individuellen Datenbanken).

## Technologie-Stack

- **Backend**: C# / .NET 8.0 mit Blazor Server
- **Frontend**: Blazor Components
- **Datenbank**: Microsoft SQL Server
- **UI Framework**: DevExpress Blazor, Bootstrap

## Kernfunktionen

- Selbstständiges Erstellen von IDBs und Tabellenstrukturen
- Moderne, intuitive Benutzeroberfläche
- Unterstützung verschiedener Datentypen (Kurzer Text, Langer Text, Ganze Zahl, Komma Zahl, Checkbox, Datum)
- Automatische Frontend-Konfiguration basierend auf Datentypen
- Flexible Spaltenreihenfolge
- CRUD-Operationen (Erstellen, Lesen, Aktualisieren, Löschen)
- Leistungsfähige Filterfunktion

## Projektstruktur

- **IDB.UI**: Blazor Server Web-Anwendung (Frontend)
- **IDB.Business**: Geschäftslogik-Schicht
- **IDB.DataAccess**: Datenzugriffsschicht (API-Kommunikation)
- **IDB.Model**: Datenmodelle
- **IDB.Service**: REST API Backend

## Installation

1. Repository klonen
2. .NET 8.0 SDK installieren
3. DevExpress NuGet Feed konfigurieren
4. `dotnet restore` ausführen
5. Datenbankverbindung in `appsettings.json` konfigurieren
6. Anwendung starten mit `dotnet run`

## Entwickelt von

Keno Julian Guthier  
Stadt Frankfurt am Main - Personal- und Organisationsamt  
IHK Abschlussprojekt 2025
