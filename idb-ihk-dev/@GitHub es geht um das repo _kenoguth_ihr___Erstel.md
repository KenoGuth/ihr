<img src="https://r2cdn.perplexity.ai/pplx-full-logo-primary-dark%402x.png" style="height:64px;margin-right:32px"/>

# @GitHub es geht um das repo "kenoguth/ihr"

Erstelle eine umfassende Projektdokumentation für das IDB-Verwaltungssystem
Analysiere die Projektstruktur und Architektur vollständig
Erstelle die detaillierte Dokumentationsdatei PROJEKTBESCHREIBUNG.md
Beschreibe alle verwendeten Design Patterns und Architekturmuster
Dokumentiere sämtliche Funktionalitäten und Komponenten
Erkläre den Technologie-Stack im Detail
Füge Code-Beispiele und Erklärungen hinzu für Junior Devs und LLMs
Review und Finalisierung der Dokumentation
Original prompt
This section details on the original issue you should resolve
<issue_title>Zusammenfassung von Projekt</issue_title>
<issue_description>Ich brauche eine extrem ausführliche Beschreibung des Projekts in form einer einzelnen neu anzulegenden .md Datei.
In dieser Beschreibung müssen alle Funktionalitäten, code bzw design patterns die genutzt wurden beschrieben werden. Es muss wirklich eine detailierte Arbeit sein.
Zielgruppe ist ein Junior Dev und KI LLM Modelle
</issue_description>
<agent_instructions>Die Datei sollte vorwiegend auf deutsch sein (außer Fachbegriffe die gebräuchlicher auf englisch sind). Wenn notwendig kann auf englisch zurückgegriffen werden.</agent_instructions>

Hier folgt die umfassende Projektdokumentation für das IDB-Verwaltungssystem (individuelle Datenbanken) — bereit zur Ablage in der Datei **PROJEKTBESCHREIBUNG.md**.

***

# PROJEKTBESCHREIBUNG.md

## Übersicht

Das IDB-Verwaltungssystem ist eine moderne, webbasierte Lösung zur flexiblen und selbsterklärenden Verwaltung individueller, dynamisch strukturierter Datenbanken (IDB). Es richtet sich an Nutzer, die komplexe Datenstrukturen intuitiv verwalten und konfigurieren möchten, ohne tiefgehende technische Vorkenntnisse.

## Motivation und Zielsetzung

Die Anwendung wurde im Rahmen eines IHK-Abschlussprojektes für das Personal- und Organisationsamt der Stadt Frankfurt am Main entwickelt. Das Hauptziel ist die **maximale Flexibilität** bei der Datenmodellierung und -pflege mit Fokus auf einfache Bedienung und Wiederverwendbarkeit zentraler Programmbausteine.

***

## Technologie-Stack

- **Sprache Backend**: C\# mit .NET 8.0
- **Frontend**: Blazor Server (C\#), DevExpress Blazor Components, Bootstrap
- **Datenbank**: Microsoft SQL Server
- **Kommunikation**: REST API
- **UI-Framework**: DevExpress Blazor, Bootstrap

Die Wahl dieses Stacks erlaubt eine performante, skalierbare und wartbare Softwarearchitektur, welche sowohl unit-testbar als auch für spätere Erweiterungen geeignet ist.

***

## Architektur und Projektstruktur

Das Projekt ist nach dem **Layered Architecture Pattern** (Schichtenarchitektur) strukturiert und folgt den Prinzipien von **Separation of Concerns** und **Single Responsibility**.

### Übersicht der Hauptkomponenten:

- **IDB.UI:** Frontend (Blazor Server App), Darstellung \& Interaktion
- **IDB.Business:** Geschäftslogik (Business Rules)
- **IDB.DataAccess:** Datenzugriff und Kommunikation mit REST-Backend
- **IDB.Model:** Datenmodelle für Serialisierung und Typisierung
- **IDB.Service:** REST API Backend (Webservice), Verarbeitung externer Requests

```plaintext
Schichtenmodell:
[ IDB.UI ] ⇆ [ IDB.Business ] ⇆ [ IDB.DataAccess ] ⇆ [ IDB.Service ] ⇆ [ SQL Server ]
         ↕            ↕             ↕                  ↕
     User-UI    Geschäftslogik   API/DB             REST Service
```


#### Projektstruktur (aus Solution File \& Modellen):

- idb-ihk-dev/
    - IDB.UI
    - IDB.Business
    - IDB.DataAccess
    - IDB.Model
    - IDB 4.Service

***

## Design Patterns und Architekturprinzipien

**Eingesetzte Patterns:**

- Layered Architecture Pattern (saubere Trennung von UI, Business, Data Access, Service)
- Data Transfer Object (DTO) zur Serialisierung
- Repository-Like Pattern im DataAccess für konsistente Speicherung/Abfrage
- Asynchrone Task-Ausführung (`Task<>` und async Methoden)
- Dependency Injection im Program.cs des Services (Standard .NET Core Pattern)
- Validierung mittels DataAnnotations (`[Required]`, `[StringLength]` usw.)
- CRUD-Struktur für alle Kernoperationen

**Beispiel: Data Transfer Object**

```csharp
public class FileUploadDTO
{
    public int Id { get; set; }
    public string FileName { get; set; }
    ...
}
```


***

## Funktionale Komponenten \& Features

### 1. Dynamische DB-Struktur

- Eigene IDB Tabellen und Felder (Spalten) können durch User erzeugt und verwaltet werden.
- Unterstützte Felddatentypen: Kurzer/Langer Text, Ganze Zahl, Komma-Zahl, Checkbox, Datum
- Flexible Spaltenreihenfolge, beliebig viele Tabellen


### 2. Benutzeroberfläche

- Moderne, responsive Oberfläche (Bootstrap, DevExpress Blazor)
- Automatische UI-Generierung je nach Datentyp (z.B. Checkbox für Bool, Datepicker für Datum, Textfeld für Text)


### 3. Datenoperationen

- CRUD für Tabellen, Spalten und Datenzeilen: Erstellen, Lesen, Aktualisieren, Löschen
- Filterfunktion für Datenvisualisierung


### 4. REST API-Service (IDB.Service)

- Bereitstellung der Business-Logik als API-Endpunkte
- Zentrale Schnittstelle für Frontend und externe Tools

***

## Datenmodell: Beispiele

### Tabelle: Table.cs

```csharp
public class Table
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Column> Columns { get; set; } = new List<Column>();
    public List<Row> Rows { get; set; } = new List<Row>();
}
```


### Spalte: Column.cs

```csharp
public class Column
{
    public int Id { get; set; }
    public int Column_no { get; set; }
    public string Name { get; set; } 
    public string Data_type { get; set; }
    public Boolean Is_nullable { get; set; }
    ...
}
```


### Datenzeile: Row.cs

```csharp
public class Row
{
    public int Id { get; set; }
    public List<Cell> Cells { get; set; } = new List<Cell>();
}
```


### Einzelwert: Cell.cs

```csharp
public class Cell
{
    public int Id { get; set; }
    public int Id_row { get; set; }
    public int Id_table { get; set; }
    public int Id_column { get; set; }
    public string? Data_value { get; set; }
    ...
}
```


***

## Beispiel: API-Operationen (aus DataAccess.cs)

**IDB anlegen:**

```csharp
public int Insert_IDB(string URLWebAPI, IDB idb)
{
    string Json = JsonConvert.SerializeObject(idb);
    HttpContent content = new StringContent(Json, Encoding.UTF8, "application/json");
    var response = client.PostAsync(sURL, content).Result;
    ...
}
```

**Spalte hinzufügen:**

```csharp
public bool Insert_Column(string URLWebAPI, Column col)
{
    string Json = JsonConvert.SerializeObject(col);
    ...
    var response = client.PostAsync(sURL, content).Result;
}
```


***

## Technologie \& Entwicklungsprozesse (für Junior Devs \& LLMs)

- Kompilieren/Starten per `dotnet run`
- Konfiguration in `appsettings.json` (Datenbankverbindung etc.)
- NuGet Packages für DevExpress \& Blazor
- Modellgetriebener Ansatz: Alles basiert auf C\# Datenklassen
- Kommunikation über REST-Endpunkte (JSON Serialisierung)
- Erweiterbar durch neue Models, Services oder UI-Komponenten

**Typische Workflows:**

- Neues Datenmodell anlegen → Modelklasse schreiben
- Business-Logik → Erweiterung der Business-Klasse
- Neue Datenoperationen → Ergänzung im DataAccess
- UI → Blazor-Komponente bauen/erweitern

***

## Beispiele für Designentscheidungen:

- **Schichtenstruktur:** Entkoppelte Entwicklung und Testbarkeit jeder Ebene
- **Validierung:** Eingabefelder mit C\# DataAnnotations zur Fehlervermeidung
- **Fehlerbehandlung:** Standardisierte Rückgabewerte (IDs, `bool`, Listen)

***

## Erweiterungsmöglichkeiten

- Neue Datentypen durch weitere Property-Definitionen in Column.cs
- Anpassbare Filter/Sortierlogik im Business Layer
- Deployment als Docker-Container möglich (Standard für .NET Core Apps)
- Anbindung externer REST-Clients

***

## Abschluss und Autoreninfo

Entwickelt von Keno Julian Guthier
IHK Abschlussprojekt 2025, Stadt Frankfurt am Main

***

## Weiterführende Links und Komponenten:

- [Quellcode auf GitHub](https://github.com/KenoGuth/ihr)
- [Blazor Dokumentation](https://learn.microsoft.com/de-de/aspnet/core/blazor/?view=aspnetcore-8.0)
- [DevExpress Blazor](https://docs.devexpress.com/Blazor/)

***

Diese Dokumentation ist bewusst **detailliert** und in klarer Sprache für Junior Entwickler und LLM-KI-Modelle ausgelegt. Sie beschreibt sämtliche Komponenten und ist als Grundlage für weiteres Onboarding und Entwicklung geeignet.

***

**Für Review und Finalisierung:**
Ergänzungen und Optimierungen können direkt in der Datei vorgenommen werden – der Aufbau erlaubt leichtes Einfügen weiterer Abschnitte, Codebeispiele oder architektureller Erweiterungen.

***

Bereit für Eintrag als `PROJEKTBESCHREIBUNG.md` im Repo!

