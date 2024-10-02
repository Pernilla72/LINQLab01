using System;
using System.Diagnostics;

namespace LINQLabExercice;


internal class Program
{
    static void Main(string[] args)
    {
        List<Person> people = ReadFromFile("names.txt");

        List<Person> peopleWithPo = StartWithSearch(people);

        var FilteredPeople = ExtractNoDublesMethod(people);
        
            foreach (var p in FilteredPeople)
            {
                Console.WriteLine($"{p.Name} {p.Namnsdag}");
            }
            Console.WriteLine(FilteredPeople.Count());
            
        
    }
     
        static List<Person> ReadFromFile(string path)
        {
            var q = File.ReadLines(path);
            return new List<Person>();
        }

        static List<Person> ExtractNoDublesMethod(IEnumerable<string> lines)
        {
            return lines
                .Select(s => new Person { Name = s.Split(';')[0], Namnsdag = DateTime.Parse(s.Split(';')[1]) })
                .DistinctBy(p => p.Name)
                .ToList();
        }

        static List<Person> StartWithSearch(IEnumerable<Person> people)
        {
            return people
                .Where(static n => n.Name.StartsWith("Po", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    
}



