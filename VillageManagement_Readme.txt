Village Management/Kingdom management Readme

Zur Aufgabe "P1 Dorfverwaltung":

Kurz vorweg:
Ich habe angefangen die gestellte Aufgabe als WPF -anwendung umzusetzen. Da ich in WPF noch nicht wirklich bewandert bin, möge man mir einige Design -"Entscheidungen" nicht übel nehmen. 
Hierzu gehört zum Beispiel auch, dass Änderungen bezüglich der Fenstergröße zu recht fehlerhaften Verhalten führen können...
An etlichen Sachen arbeite ich noch und hoffe diese, bis zur Abgabe der "Königreich" -hausaufgabe, in den Griff zu bekommen.

Navigation:
Das Hauptfenster zeigt die gesamten Steuern, sowie auch die Steuern des ausgewählten Stammes an. Der Stamm kann in der Combobox unter dem Label "Stamm" ausgewählt werden. 
Die Mitglieder des gewählten Stammes sind in der ersten Liste zu sehen (Führer sind farblich hervorgehoben). 
Ist ein Stammesmitglied ausgewählt, so erscheinen dessen Waffen in der rechten Liste.

Ein Rechts-Click auf ein Stammesmitglied öffnet ein Menü können später verschiedene Reiter gewählt werden - Zum jetzigen Zeitpunkt existiert nur die Option "Edit" 
Im „Edit“ -fenster können Name und Alter des Mitglieds geändert werden. Ebenso können Ausrüstungsgegenstände hinzugefügt oder entfernt werden. Das Management der Ausrüstungsgegenstände soll später in ein seperates Menü. 
Wenn mindestens einer der Werte „Alter“ oder „Name“ geändert wurde und valide ist, kann gespeichert werden. Der Austausch von Waffen wird bei jeder „Tausche“ -aktion übernommen, muss also nicht gespeichert werden.
Die Liste „Eigene Waffen“ zeigt alle Waffen welche sich im Besitz des Charakters befinden, die Liste „Verfügbare Waffen“ zeigt Waffen an welche noch von keinem Charakters zugeordnet sind.
Über die Schaltfläche „Return“ gelangt man zurück in die Hauptansicht.

Charakter erstellen:
Über die Schaltfläche „Hinzufügen“ gelangt der Nutzer in eine neue Ansicht. Das Speicher eines neuen Stammesmitgliedes ist erst möglich wenn sämtliche Felder beschrieben und validiert sind (maximale/minimale Länge des Inputs / Nummerische Überprüfung usw.). 
Bei der Wahl eines Typen, werden in der Stammesliste alle in Frage kommenden Stämme zur Auswahl angezeigt. Um den Charakters zu speichern muss ein Typ und ein Stamm gewählt worden sein. 
Im Prinzip ist es möglich sämtliche Typen zu erstellen, da über die Grundeinstellungen zur Zeit jedoch nur Zwerg- und Elfenstämme geladen werden, können auch nur jene Typen instantiiert werden.
In der Application soll es später natürlich möglich sein Stämme für alle Typen zu erstellen.

Item Manager:
Über den Item Manager ist es möglich neue Waffen zu erstellen. Hierzu ist unter „Kategorie“ die gewünschte Gattung zu wählen und ein „Power“ -wert  einzutragen. Anschließend ist es möglich zu speichern. Die erstellten Waffen tauchen anschließend rechts in der Liste „Waffen“ auf. 
In der Liste tauchen ebenso sämtliche Waffen auf, welche noch nicht an einen Charakter vergeben sind. Neu erstellte Waffen können einem Charakter über den Edit -funktion, oder einem neuen Charakter in der Ansicht zum erstellen eines jenen, zugewiesen werden. 

Die Höhe der Steuern, die auf den Betroffenen Stamm erhoben werden erhöhen sich mit jeder in den Stamm integrierten Waffe. Die aktuellen Steuern auf einzelne Stämme kann, wie bereits beschrieben, im Hauptfenster begutachtet werden.

Hiermit soll die Einführung auch erst einmal enden. Ich hoffe das Programm, bis zur Abgabe des „Königreichs“, beenden zu können.

Und nun wünsche ich noch recht viel Vergnügen und unzählige spaßige Stunden mit meiner App... 

Ihr allseits verehrter Phidel Castro
