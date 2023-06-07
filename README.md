# HTL Spengergasse POS

Eigenständige Aufgabe Sommersemester 22/23

## Über das Projekt

Dieses Projekt ist eine eigenständige Webanwendung, die im Rahmen des Sommersemesters entwickelt wurde. Es wurde eine Razor Pages-Anwendung implementiert, die alle CRUD-Operationen (Create, Read, Update, Delete) unterstützt. Die Anwendung erfüllt alle Mindestanforderungen und bietet zusätzliche Erweiterungen für eine bessere Beurteilung.

Die Hauptfunktionen der Webanwendung umfassen:

- Eine übersichtliche Indexseite, die alle in der Datenbank gespeicherten Objekte in Tabellenform anzeigt. Es werden Links zu Detail-, Bearbeitungs- und Löschseiten für jeden Eintrag bereitgestellt.
- Eine Detailseite, die die Daten eines ausgewählten Objekts sowie eine Liste aller verbundenen Objekte anzeigt.
- Eine Bearbeitungsseite, auf der Benutzer die Daten eines Objekts ändern können. Es gibt Validierungen für die Eingabefelder und die Möglichkeit, mehrere Objekte gleichzeitig zu bearbeiten (Bulk Edit).
- Eine Löschseite, auf der Benutzer die Löschung eines Objekts bestätigen oder abbrechen können.
- Eine Seite zum Hinzufügen neuer Objekte zur Datenbank mit entsprechenden Validierungen für die Eingabefelder.
- Eine Login-Seite mit Authentifizierung und Autorisierung, um den Zugriff auf bestimmte Seiten zu beschränken.

Zusätzlich zu den Mindestanforderungen wurden folgende Erweiterungen implementiert:

- CRUD-Operationen für weitere Modelklassen.
- Optimierung der Abfragen, um die Anzahl der verbundenen Objekte effizient zu ermitteln.
- Formatierungserweiterungen, um bestimmte Zeilen in Tabellen basierend auf definierten Bedingungen hervorzuheben.
- Verwendung mehrerer SelectLists und DTO-Klassen in Verbindung mit dem AutoMapper.
- Validierungen der Eingabefelder basierend auf Werten in der Datenbank.
- Anzeige von Bearbeitungs- oder Löschlinks abhängig vom eingeloggten Benutzer.

## Lizenz

Veröffentlicht unter der MIT-Lizenz. Weitere Informationen findest du in der [LICENSE](https://github.com/stoyboy/LibraryManagement/blob/master/LICENSE)-Datei.

## Autor

* **Jovan Stojimirovic** - [Jovan Stojimirovic](https://github.com/stoyboy)
