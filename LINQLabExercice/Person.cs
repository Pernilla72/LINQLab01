using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLabExercice;

public class Person
{
    public string? Name { get; set; }
    public DateTime Namnsdag { get; set; }

    public override string ToString()
    {
        return $"{Name?.PadRight(20)}  {Namnsdag: yyyy-MM-dd}";
    }
}


