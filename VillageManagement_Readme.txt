Village Management Readme

Zur Aufgabe "P1 Dorfverwaltung":

Kurz vorweg:
Ich habe angefangen die gestellte Aufgabe als WPF -anwendung umzusetzen. Da ich in WPF noch nicht wirklich bewandert bin, m�ge man mir einige Design -"Entscheidungen" nicht �bel nehmen. 
Hierzu geh�rt zum Beispiel auch, dass �nderungen bez�glich der Fenstergr��e zu recht fehlerhaften Verhalten f�hren k�nnen...
An etlichen Sachen arbeite ich noch und hoffe diese, bis zur Abgabe der "K�nigreich" -hausaufgabe, in den Griff zu bekommen.

Navigation:
Das Hauptfenster zeigt die gesamten Steuern, sowie auch die Steuern des ausgew�hlten Stammes an. Der Stamm kann in der Combobox unter dem Label "Stamm" ausgew�hlt werden. 
Die Mitglieder des gew�hlten Stammes sind in der ersten Liste zu sehen (F�hrer sind farblich hervorgehoben). 
Ist ein Stammesmitglied ausgew�hlt, so erscheinen dessen Waffen in der rechten Liste.

Ein Rechts-Click auf ein Stammesmitglied �ffnet ein Men� k�nnen sp�ter verschiedene Reiter gew�hlt werden - Zum jetzigen Zeitpunkt existiert nur die Option "Edit" 
Im �Edit� -fenster k�nnen Name und Alter des Mitglieds ge�ndert werden. Ebenso k�nnen Ausr�stungsgegenst�nde hinzugef�gt oder entfernt werden. Das Management der Ausr�stungsgegenst�nde soll sp�ter in ein seperates Men�. 
Wenn mindestens einer der Werte �Alter� oder �Name� ge�ndert wurde und valide ist, kann gespeichert werden. Der Austausch von Waffen wird bei jeder �Tausche� -aktion �bernommen, muss also nicht gespeichert werden.
Die Liste �Eigene Waffen� zeigt alle Waffen welche sich im Besitz des Charakters befinden, die Liste �Verf�gbare Waffen� zeigt Waffen an welche noch von keinem Charakters zugeordnet sind.
�ber die Schaltfl�che �Return� gelangt man zur�ck in die Hauptansicht.

Charakter erstellen:
�ber die Schaltfl�che �Hinzuf�gen� gelangt der Nutzer in eine neue Ansicht. Das Speicher eines neuen Stammesmitgliedes ist erst m�glich wenn s�mtliche Felder beschrieben und validiert sind (maximale/minimale L�nge des Inputs / Nummerische �berpr�fung usw.). 
Bei der Wahl eines Typen, werden in der Stammesliste alle in Frage kommenden St�mme zur Auswahl angezeigt. Um den Charakters zu speichern muss ein Typ und ein Stamm gew�hlt worden sein. 
Im Prinzip ist es m�glich s�mtliche Typen zu erstellen, da �ber die Grundeinstellungen zur Zeit jedoch nur Zwerg- und Elfenst�mme geladen werden, k�nnen auch nur jene Typen instantiiert werden.
In der Application soll es sp�ter nat�rlich m�glich sein St�mme f�r alle Typen zu erstellen.

Item Manager:
�ber den Item Manager ist es m�glich neue Waffen zu erstellen. Hierzu ist unter �Kategorie� die gew�nschte Gattung zu w�hlen und ein �Power� -wert  einzutragen. Anschlie�end ist es m�glich zu speichern. Die erstellten Waffen tauchen anschlie�end rechts in der Liste �Waffen� auf. 
In der Liste tauchen ebenso s�mtliche Waffen auf, welche noch nicht an einen Charakter vergeben sind. Neu erstellte Waffen k�nnen einem Charakter �ber den Edit -funktion, oder einem neuen Charakter in der Ansicht zum erstellen eines jenen, zugewiesen werden. 

Die H�he der Steuern, die auf den Betroffenen Stamm erhoben werden erh�hen sich mit jeder in den Stamm integrierten Waffe. Die aktuellen Steuern auf einzelne St�mme kann, wie bereits beschrieben, im Hauptfenster begutachtet werden.

Hiermit soll die Einf�hrung auch erst einmal enden. Ich hoffe das Programm, bis zur Abgabe des �K�nigreichs�, beenden zu k�nnen.

Und nun w�nsche ich noch recht viel Vergn�gen und unz�hlige spa�ige Stunden mit meiner App... 

Ihr allseits verehrter Philipp Klauschke
