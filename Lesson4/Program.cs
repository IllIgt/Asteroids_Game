using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4
{
    class Program
    {
        static void Main(string[] args)
        { 
            List<int> col = new List<int>() { 1, 2, 3, 4, 5, 6, 1, 1, 2, 3, 4, 6, 7, 2 };

            var col_dict = col.Select(num => new { Name = num, Count = col.Count(s => s == num) }).
                Distinct().
                ToDictionary(res => res.Name, res => res.Count);

            foreach (KeyValuePair<int, int> num in col_dict)
                Console.WriteLine($"{num.Key}:{num.Value}");

            
            //Task 3
            Dictionary<string, int> dict = new Dictionary<string, int>()
                      {
                        {"four",4 },
                        {"two",2 },
                        { "one",1 },
                        {"three",3 },
                      };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });

            foreach (var pair in d)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
            }
            Console.ReadLine();
        }
    }
}
