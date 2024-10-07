using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Diagnostics;
using static LINQLabExercice.Timeframes;


namespace LINQLabExercice;


public class Program
{
    static void Main(string[] args)
    {
        //1: Läs in personer från filen
        List<Person> people = ReadFromFile("names.txt");

        //1: Se till att det inte är några dubletter
        var FilteredPeople = ExtractNoDublesMethod(people);
        PrintPeople(FilteredPeople, "Filtrerad lista, utan dubletter:");
        //Console.WriteLine(FilteredPeople.Count());

        //2a: Lista alla namn som börjar på en viss bokstavskombination
        List<Person> PeopleWithLi = StartWithSearch(FilteredPeople);
        PrintPeople(PeopleWithLi, "Alla personer vars namn börjar på 'Li'");

        //2b:Lista alla namn som har namnsdag ett visst datum
        List<Person> SpecialNamedayDate = NamedayOnApril23rd(FilteredPeople);
        PrintPeople(SpecialNamedayDate, "Alla personer som har namnsdag 23 April");

        //2c: Lista alla namn som börjar på en bestämd bokstavskombination och har namnsdag 
        //en bestämd månad.
        List<Person> SelectedDateAndStartofName = StartAndDateDecided(FilteredPeople);
        PrintPeople(SelectedDateAndStartofName, "Alla som har namnsdag i Maj och börjar på Ma");

        //3: Skriv en Linq-fråga som listar hur många namn som börjar på varje bokstav i alfabetet.
        List<object> EachStartLetterCount = CountedStartLetter(FilteredPeople);
        PrintList(EachStartLetterCount, "Så här många börjar på resp. bokstav i alfabetet");

        //Skriv Linq-frågor som beräknar hur många som har namnsdag:

        //i varje månad
        List<object> NamedayEachMonth = CountedMonthlyNameday(FilteredPeople);
        PrintMonthlyList(NamedayEachMonth, "Så här många har namnsdag på resp. månad");

        //i varje kvartal
        List<Timeframes> NamedayEachQuarter = CountedQuarterlyNameday(FilteredPeople);
        PrintQuarterlyList(NamedayEachQuarter, "Så här många har namnsdag i resp. kvartal");

        //de fem dagar på året som flest har namnsdag.

    }

    static List<Person> ReadFromFile(string path)
    {
        var q = File.ReadLines(path);
        var people = q
        .Select(s => new Person { Name = s.Split(';')[0], Namnsdag = DateTime.Parse(s.Split(';')[1]) })
        //return new List<Person>();
        .ToList();
        return people;
    }

    //Uppgift 1
    static List<Person> ExtractNoDublesMethod(IEnumerable<Person> people)
    {
        return people
            .DistinctBy(p => p.Name)
            .ToList();
    }

    //Uppgift 2a
    static List<Person> StartWithSearch(IEnumerable<Person> people)
    {
        return people
            .Where(n => n.Name.StartsWith("Li", StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    //Uppgift 2b
    static List<Person> NamedayOnApril23rd(IEnumerable<Person> people)
    {
        return people
            .Where(d => d.Namnsdag.Month == 4 && d.Namnsdag.Day == 23)
            .ToList();
    }

    //Uppgift 2c
    static List<Person> StartAndDateDecided(IEnumerable<Person> people)
    {
        return people
            .Where(d => d.Namnsdag.Month == 5 && d.Name.StartsWith("Ma", StringComparison.OrdinalIgnoreCase))
            .ToList();
    }


    //Uppgift 3
    static List<object> CountedStartLetter(IEnumerable<Person> people)
    {
        return people
        .Where(p => !string.IsNullOrEmpty(p.Name))
        .GroupBy(p => p.Name.Substring(0, 1))
        .Select(g => new { FirstLetter = g.Key, Count = g.Count() })
        .Cast<object>()
        .ToList();
    }

    //Uppgift 4a
    static List<object> CountedMonthlyNameday(IEnumerable<Person> people)
    {
        return people
        .Where(p => !string.IsNullOrEmpty(p.Name))
        .GroupBy(d => d.Namnsdag.Month)
        .Select(g => new { Month = g.Key, Count = g.Count() })
        .Cast<object>()
        .ToList();
    }
    static List<Timeframes> CountedQuarterlyNameday(IEnumerable<Person> people)
    {
        return people
        .Where(p => !string.IsNullOrEmpty(p.Name))
        .GroupBy(d => (d.Namnsdag.Month +2) /3)
        .Select(g => new Timeframes { Quarter = g.Key, Count = g.Count() })
        .ToList();
    }


    //Olika printmetoder
    static void PrintPeople(List<Person> people, string header)
    {
        Console.WriteLine(header);
        foreach (var person in people)
        {
            Console.WriteLine($"{person.Name} {person.Namnsdag}");
        }
        Console.WriteLine($"Antal personer: {people.Count()}");
        Console.WriteLine(".........................");
    }
    static void PrintList(IEnumerable<dynamic> groups, string header)
    {
        Console.WriteLine(header);
        foreach (var group in groups)
        {
            Console.WriteLine($"{group.FirstLetter}: {group.Count} personer");
        }

        Console.WriteLine(".........................");
    }

    static void PrintMonthlyList(IEnumerable<dynamic> groups, string header)
    {
        Console.WriteLine(header);
        var SortedGroups = groups.OrderBy(g => g.Month);   //Sorterar månaderna i rätt ordning.
        foreach (var group in groups)
        {
            string monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Month);
            Console.WriteLine($"{monthName}: {group.Count} personer");
        }

        Console.WriteLine(".........................");
    }

    static void PrintQuarterlyList(IEnumerable<Timeframes> groups, string header)
    {
        Console.WriteLine(header);
        var sortedgroups = groups.OrderBy(g => g.Quarter);

        foreach (var group in sortedgroups)
        {
            string quarterName = $"Q{group.Quarter}";
            Console.WriteLine($"{quarterName}: {group.Count} personer");
        }

        Console.WriteLine(".........................");
    }
}



