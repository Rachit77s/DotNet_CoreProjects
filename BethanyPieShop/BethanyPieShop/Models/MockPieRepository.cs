using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanyPieShop.Models
{
    public class MockPieRepository : IPieRepository
    {

        private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();

        public IEnumerable<Pie> AllPies =>
            new List<Pie>
            {
                new Pie{ PieId=1, Name="Strawberry Pie", Price=15.95M, ShortDescription="", LongDescription="", AllergyInformation="", ImageUrl="", ImageThumbnailUrl="", IsPieOfTheWeek=false,
                InStock=true , CategoryId=1 },

                new Pie{ PieId=1, Name="Cheese cake", Price=18.95M, ShortDescription="", LongDescription="", AllergyInformation="", ImageUrl="", ImageThumbnailUrl="", IsPieOfTheWeek=false,
                InStock=true , CategoryId=2 },

                new Pie{ PieId=1, Name="Pumpkin pie", Price=12.95M, ShortDescription="", LongDescription="", AllergyInformation="", ImageUrl="", ImageThumbnailUrl="", IsPieOfTheWeek=false,
                InStock=true , CategoryId=3 }
            };

        public IEnumerable<Pie> PiesOfTheWeek { get; }

        public Pie GetPieById(int pieId)
        {
            return AllPies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
