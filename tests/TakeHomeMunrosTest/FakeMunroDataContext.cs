using System.Collections.Generic;
using System.Linq;

using TakeHomeMunros.Data;

namespace TakeHomeMunrosTest
{
    internal class FakeMunroDataContext : IMunroDataContext
    {
        public FakeMunroDataContext(IEnumerable<Munro> munros)
        {
            Munros = munros;
        }
        

        public IEnumerable<Munro> Munros { get;  }

        public static FakeMunroDataContext GenerateFakeMunroDataContext(int numberOfMuns, int numberOfTops,
            int numberOfBlanks, double minHeight, double maxHeight)
        {
            return new (GenerateFakeMunros(numberOfMuns, numberOfTops, numberOfBlanks, minHeight, maxHeight));
        }

        public static IList<Munro> GenerateFakeMunros(int numberOfMuns, int numberOfTops, int numberOfBlanks, double minHeight, double maxHeight)
        {
            var munros = new List<Munro>();

            var categoriesWithCount = new[]
            {
                new
                {
                    Category = "MUN",
                    Count = numberOfMuns
                },
                new
                {
                    Category = "TOP",
                    Count = numberOfTops
                },
                new
                {
                    Category = "",
                    Count = numberOfBlanks
                }
            };

            var heightDelta = maxHeight - minHeight;

            foreach (var category in categoriesWithCount)
            {
                var heightIncrement = heightDelta / category.Count;
                munros.AddRange(Enumerable.Range(0, category.Count).ToList().Select(i => new Munro
                {
                    Name = $"{(string.IsNullOrEmpty(category.Category) ? "blank" : category.Category)}{i}",
                    HillCategoryPost1997 = category.Category,
                    GridRef = "NN257459",
                    HeightInMetres = minHeight + heightIncrement * i
                }));
            }

            return munros;
        }
    }
}
