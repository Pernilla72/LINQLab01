using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Diagnostics;

namespace LINQLabExercice;


public class Program
{
    static void Main(string[] args)
    {       
            //Läs in personer från filen
        List<Person> people = ReadFromFile("names.txt");

        var FilteredPeople = ExtractNoDublesMethod(people);
        PrintPeople(FilteredPeople, "Filtrerad lista, utan dubletter:");

            //Console.WriteLine(FilteredPeople.Count());

        List<Person> PeopleWithLi = StartWithSearch(FilteredPeople);
        PrintPeople(PeopleWithLi, "Alla personer vars namn börjar på 'Li'");
        
        List<Person> SpecialNamedayDate = NamedayOnApril23rd(FilteredPeople);
        PrintPeople(SpecialNamedayDate, "Alla personer som har namnsdag 23 April");

        //Lista alla namn som börjar på en bestämd bokstavskombination och har namnsdag 
        //en bestämd månad.
        List<Person> SelectedDateAndStartofName = StartAndDateDecided(FilteredPeople);
        PrintPeople(SelectedDateAndStartofName, "Alla som har namnsdag i Maj och börjar på Ma");

        //Skriv en Linq-fråga som listar hur många namn som börjar på varje bokstav i alfabetet.
        List<object> EachStartLetterCount = CountedStartLetter(FilteredPeople);
        PrintList(EachStartLetterCount, "Så här många börjar på resp. bokstav i alfabetet");
        


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
                .Where(d => d.Namnsdag.Month == 5 && d.Name.StartsWith ("Ma", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


        //Uppgift 3
        static List<object> CountedStartLetter(IEnumerable<Person> people)
            {
                return people
                .Where(p => !string.IsNullOrEmpty(p.Name))
                .GroupBy(p => p.Name.Substring(0,1))
                .Select(g => new { FirstLetter = g.Key, Count = g.Count() })
                .Cast<object>()
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

}



