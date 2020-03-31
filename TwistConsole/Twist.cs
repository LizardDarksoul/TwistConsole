using System;
using System.IO;
using System.Linq;

namespace TwistConsole
{
    class Twist
    {

        public static String WortlistePfad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\C#TestOrdner\\woerterliste.txt";

        public static void ConsoleMenu()
        {
            //Starttext schreiben
            Console.WriteLine("Was möchtest du machen?");
            //Warte auf Eingabe
            String Command = Console.ReadLine();
            //Konsole bereinigen & entsprechende Befehle auf Basis der Eingabe ausführen
            Console.Clear();
            switch (Command)
            {
                //Twisten wird gestartet
                case "Twist":
                    Console.Write("Twist\nEingabe: ");
                    Console.WriteLine("Ausgabe: " + Twist.Twisten(Console.ReadLine()));
                    Console.ReadKey();
                    break;
                //Enttwisten wird gestartet
                case "Enttwist":
                    Console.Write("Enttwist\nEingabe: ");
                    Console.WriteLine("Ausgabe: " + Twist.Enttwisten(Console.ReadLine()));
                    Console.ReadKey();
                    break;
                //Legt den Pfad der Wortliste fest (Startwert: Dokumente\C#TestOrdner\woerterliste.txt)
                case "Wortliste":
                    Console.Write("Legen sie den Pfad für die Wortliste fest.\nDer aktuelle Wert ist: " + WortlistePfad + "\nNeuer Wert: ");
                    WortlistePfad = Console.ReadLine();
                    Console.ReadKey();
                    break;
                //"help" gibt alle gültigen Kommandos aus
                case "help":
                    Console.WriteLine("Gültige Kommandos sind:\nTwist\t\tZum twisten eines Wortes oder Satzes(ohne Sonderzeichen)\nEnttwist\tZum enttwisten eines Wortes\nWortliste\tFestlegen des Pfads der Wortliste\nhelp\t\tgültige Kommandos\nexit\t\tVerlassen der Anwendung");
                    Console.ReadKey();
                    break;
                //"exit" beendet die Anwendung
                case "exit":
                    System.Environment.Exit(1);
                    break;
                //kein gültiges Kommando & Ausgabe gültiger Kommandos
                default:
                    Console.WriteLine("\"" + Command + "\" ist kein gültiges Kommando.\n\nGültige Kommandos sind:\nTwist\t\tZum twisten eines Wortes oder Satzes(ohne Sonderzeichen)\nEnttwist\tZum enttwisten eines Wortes\nWortliste\tFestlegen des Pfads der Wortliste\nhelp\t\tgültige Kommandos\nexit\t\tVerlassen der Anwendung");
                    Console.ReadKey();
                    break;
            }
            //Konsole bereinigen & erneutes Ausführen der Methode
            Console.Clear();
            ConsoleMenu();
        }

        public static String Twisten(String EG)
        {
            //String Array erstellen
            String[] twist;

            //aufspliten des Satzes in einzelne Wörter & pro Wort einmal ausführen
            twist = EG.Split(' ');
            for (int i = 0; i < twist.Length; i++)
            {
                //Char Array aus Wort erstellen & länge festhalten
                Char[] twisten = twist[i].ToCharArray();
                int l = twisten.Length;
                //pro Char in Array ausführen
                for (int j = 1; j < (l - 1); j++)
                {
                    //erstellen des Random Wertes
                    int k = j + new Random().Next((l - 1) - j);
                    //austauschen der beiden Chars im Array
                    Char t = twisten[k];
                    twisten[k] = twisten[j];
                    twisten[j] = t;
                }
                //String Array mit neuen getwisteteten Wörtern überschreiben
                twist[i] = string.Join("", twisten);
            }
            //String Array zu einzelnen String & zurückgeben des getwisteten Wortes
            return string.Join(" ", twist);
        }

        public static String Enttwisten(String EG)
        {
            //String Array der Wortliste holen & abfangen falls nicht erfolgreich
            String[] WortArray = Streamreader(WortlistePfad);
            if (WortArray.Length == 0) return "";

            //Eingabe zu CharArray & alles klein machen (z.B. F->f)
            char[] EGCharArray = EG.ToLower().ToCharArray();
            //Array nach Alphabetischer Ordnung sortieren
            Array.Sort(EGCharArray);

            //jedes Wort im WortArray prüfen
            foreach (string s in WortArray)
            {
                //Wenn erster Buchstabe, letzter Buchstabe & die Länge der Wörter übereinstimmen (hier wird der Eingabe String genutzt nicht das Eingabe CharArray)
                if (s.ToLower()[0] == EG.ToLower()[0] && s.ToLower()[s.Length - 1] == EG.ToLower()[EG.Length - 1] && s.Length == EG.Length)
                {
                    //Wenn Übereinstimmung & dann Wort aus WortArray zu CharArray
                    char[] WortCharArray = s.ToLower().ToCharArray();
                    //Array nach Alphabetischer Ordnung sortieren
                    Array.Sort(WortCharArray);

                    //wenn Wort übersitimmt dann Wort zurückgeben
                    if (WortCharArray.SequenceEqual(EGCharArray))
                    {
                        return s;
                    }

                }
            }
            //Wenn keine Übereinstimmung dann Fehlschlag zurückgeben
            return "Wort konnte nicht gefunden werden";
        }

        public static String[] Streamreader(String FileDocPath)
        {
            //Return StringArray initialiesieren mit Länge 0
            String[] RStringA = new String[0];
            try
            {
                //StreamReader ertellen
                using (StreamReader SR = new StreamReader(FileDocPath))
                {
                    //Return StringArray mit Daten aus Streamreader gleichsetzen
                    RStringA = SR.ReadToEnd().Split('\n');
                    //Streamreader schließen
                    SR.Close();
                }
            }
            //Exceptions abfangen (FileNotFound, DirectoryNotFound & allgemein alle anderen Exceptions)
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Datei konnte nicht gefunden werden");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Ordner konnte nicht gefunden werden");
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler beim lesen der Datei und erstellen des WortListenArrays");
            }
            //Zurückgeben des StringArray
            return RStringA;
        }

    }
}