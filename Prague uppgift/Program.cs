﻿
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

    // Hämtar registreringsnummer från användaren
    static string HämtaRegNummer()
    {
        Console.Write("Ange registreringsnummer: ");
        return Console.ReadLine().ToUpper();
    }

    // Parkera ett fordon
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

    