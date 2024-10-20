using System;


class SlottetParking
{
    static Fordon[] garagePlatser = new Fordon[100]; // Array som representerar 100 platser

    static void Main(string[] args)
    {
        bool körProgrammet = true;

        while (körProgrammet)
        {
            VisaMeny();

            string val = Console.ReadLine();

            switch (val)
            {
                case "1":
                    HanteraParkering();
                    break;
                case "2":
                    HanteraFlytt();
                    break;
                case "3":
                    HanteraHämtning();
                    break;
                case "4":
                    HanteraSökning();
                    break;
                case "5":
                    VisaParkeringsStatus();
                    break;
                case "6":
                    VisaTommaPlatser();
                    break;
                case "7":
                    körProgrammet = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Programmet avslutas... Tack för besöket!");
                    Console.ResetColor();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltigt val, försök med ett alternativ mellan 1 och 7.");
                    Console.ResetColor();
                    break;
            }

            Pausa();
        }
    }

    // Visar huvudmenyn
    static void VisaMeny()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Välkommen till Slottets Parkeringsservice!");
        Console.ResetColor();
        Console.WriteLine("1. Parkera ditt fordon");
        Console.WriteLine("2. Flytta ditt fordon");
        Console.WriteLine("3. Hämta ut fordon");
        Console.WriteLine("4. Leta upp ett fordon");
        Console.WriteLine("5. Visa parkeringsstatus");
        Console.WriteLine("6. Visa tomma platser");
        Console.WriteLine("7. Avsluta");
        Console.Write("Ange ditt val (1-7): ");
    }

    // Pausar programmet så att användaren kan läsa texten
    static void Pausa()
    {
        Console.WriteLine("Tryck på en knapp för att fortsätta...");
        Console.ReadKey();
    }

    // Hanterar parkeringen av fordon
    static void HanteraParkering()
    {
        string typAvFordon = HämtaFordonstyp();
        if (typAvFordon == null) return;

        string regnummer = HämtaRegNummer();
        ParkeraFordon(typAvFordon, regnummer);
    }

    // Hämtar fordonstyp från användaren
    static string HämtaFordonstyp()
    {
        Console.Write("Ange typ av fordon (BIL/MC): ");
        string typAvFordon = Console.ReadLine().ToUpper();
        if (typAvFordon != "BIL" && typAvFordon != "MC")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltig fordonstyp. Försök igen.");
            Console.ResetColor();
            return null;
        }

        return typAvFordon;
    }

    // Hämtar registreringsnummer 
    static string HämtaRegNummer()
    {
        Console.Write("Ange registreringsnummer: ");
        return Console.ReadLine().ToUpper();
    }

    // Parkera ett fordon// 
    static void ParkeraFordon(string typAvFordon, string regnummer)
    {
        for (int i = 0; i < garagePlatser.Length; i++)
        {
            if (garagePlatser[i] == null)
            {
                garagePlatser[i] = new Fordon(typAvFordon, regnummer);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Fordon parkerat på plats {i + 1}.");
                Console.ResetColor();
                return;
            }
            else if (typAvFordon == "MC" && garagePlatser[i].Fordonstyp == "MC" && !garagePlatser[i].Registreringsnummer.Contains("|"))
            {
                garagePlatser[i].Registreringsnummer += "|" + regnummer;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Motorcykel parkerad tillsammans på plats {i + 1}.");
                Console.ResetColor();
                return;
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Tyvärr, inga lediga platser just nu.");
        Console.ResetColor();
    }
// Hanterar flytt av fordon
    static void HanteraFlytt()
    {
        int nuvarandePlats = HämtaPlatsNummer("Ange nuvarande platsnummer (1-100): ");
        if (!ÄrPlatsGiltig(nuvarandePlats)) return;

        int nyPlats = HämtaPlatsNummer("Ange ny platsnummer (1-100): ");
        if (ÄrFlyttGiltig(nyPlats))
        {
            FlyttaFordon(nuvarandePlats, nyPlats);
        }
    }

    // Hämtar platsnummer från användaren
    static int HämtaPlatsNummer(string fråga)
    {
        Console.Write(fråga);
        return int.Parse(Console.ReadLine()) - 1;  // Justera för 0-index
    }

    // Kontroll om plats är giltig
    static bool ÄrPlatsGiltig(int plats)
    {
        if (plats < 0 || plats >= garagePlatser.Length || garagePlatser[plats] == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltig plats eller ingen bil på platsen.");
            Console.ResetColor();
            return false;
        }
        return true;
    }

    // Kontroll om en flyttning är giltig
    static bool ÄrFlyttGiltig(int nyPlats)
    {
        if (nyPlats < 0 || nyPlats >= garagePlatser.Length || garagePlatser[nyPlats] != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltig ny plats eller platsen är redan upptagen.");
            Console.ResetColor();
            return false;
        }
        return true;
    }
 // Flytta ett fordon
    static void FlyttaFordon(int nuvarandePlats, int nyPlats)
    {
        garagePlatser[nyPlats] = garagePlatser[nuvarandePlats];
        garagePlatser[nuvarandePlats] = null;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Fordonet har flyttats från plats {nuvarandePlats + 1} till plats {nyPlats + 1}.");
        Console.ResetColor();
    }

    // Hanterar hämtning av fordon
    static void HanteraHämtning()
    {
        int plats = HämtaPlatsNummer("Ange platsnummer (1-100) för hämtning: ");
        if (ÄrPlatsGiltig(plats))
        {
            HämtaFordon(plats);
        }
    }
    
    // Hämtar ett fordon
    static void HämtaFordon(int plats)
    {
        var fordon = garagePlatser[plats];
        TimeSpan tidParkering = DateTime.Now - fordon.ParkeradTid; // Beräknar tiden som fordonet har stått parkerat
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Fordonet med regnr {fordon.Registreringsnummer} har stått parkerat i {tidParkering.Hours} timmar och {tidParkering.Minutes} minuter.");
        garagePlatser[plats] = null; // Ta bort fordonet från parkeringen
        Console.ResetColor();
    }

    // Hanterar sökning av fordon
    static void HanteraSökning()
    {
        string regnummer = HämtaRegNummer();
        SökFordon(regnummer);
    }

    // Söker efter ett fordon baserat på registreringsnummer
    static void SökFordon(string regnummer)
    {
        for (int i = 0; i < garagePlatser.Length; i++)
        {
            if (garagePlatser[i] != null && garagePlatser[i].Registreringsnummer.Contains(regnummer))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Fordonet med registreringsnummer {regnummer} finns på plats {i + 1}.");
                Console.ResetColor();
                return;
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Fordonet kunde inte hittas.");
        Console.ResetColor();
    }

    // Visar parkeringsstatus
    static void VisaParkeringsStatus()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Parkeringsstatus:");
        Console.ResetColor();
        for (int i = 0; i < garagePlatser.Length; i++)
        {
            if (garagePlatser[i] == null)
            {
                Console.ForegroundColor = ConsoleColor.Green; // Ledig plats
                Console.WriteLine($"Plats {i + 1}: Ledig");
            }
            else if (garagePlatser[i].Fordonstyp == "MC" && garagePlatser[i].Registreringsnummer.Contains("|")) // Halvfull plats
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Plats {i + 1}: Halvfull - {garagePlatser[i]}");
            }
            else // Full plats
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Plats {i + 1}: Fylld - {garagePlatser[i]}");
            }
        }
        Console.ResetColor();
    }

    // Visar tomma platser
    static void VisaTommaPlatser()
    {
        Console.WriteLine("Tomma platser:");
        for (int i = 0; i < garagePlatser.Length; i++)
        {
            if (garagePlatser[i] == null)
            {
                Console.WriteLine($"Plats {i + 1} är ledig.");
            }
        }
    }
}

// Klass som representerar ett fordon
class Fordon
{
    public string Fordonstyp { get; set; }
    public string Registreringsnummer { get; set; }
    public DateTime ParkeradTid { get; set; } // Tidsstämpel när fordonet parkerades

    public Fordon(string typAvFordon, string regnummer)
    {
        Fordonstyp = typAvFordon;
        Registreringsnummer = regnummer;
        ParkeradTid = DateTime.Now; // Automatisk tidsstämpel vid parkering
    }

    public override string ToString()
    {
        return $"{Fordonstyp} #{Registreringsnummer} (parkerad {ParkeradTid})";
    }
}
