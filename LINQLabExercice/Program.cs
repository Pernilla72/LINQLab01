using DocumentFormat.OpenXml.ExtendedProperties;
using System;
using System.Diagnostics;

namespace LINQLabExercice;


public class Program
{
    static void Main(string[] args)
    {
        List<Person> people = ReadFromFile("names.txt");

        var FilteredPeople = ExtractNoDublesMethod(people);

            foreach (var p in FilteredPeople)
            {
                Console.WriteLine($"{p.Name} {p.Namnsdag}");
            }
            Console.WriteLine(FilteredPeople.Count());

        List<Person> PeopleWithLi = StartWithSearch(people);
            foreach (var person in PeopleWithLi)
            {
                Console.WriteLine($"{person.Name} {person.Namnsdag}");
            }
            Console.WriteLine(PeopleWithLi.Count());
            Console.WriteLine(".........................");
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

        static List<Person> StartWithSearch(IEnumerable<Person> people)
        {
            return people
                .Where(static n => n.Name.StartsWith("Li", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    
}



