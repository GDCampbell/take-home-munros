using System;
using System.Linq;
using AutoMapper;

using TakeHomeMunrosApi.Queries;
using TakeHomeMunrosApi.Services;
using Xunit;

namespace TakeHomeMunrosTest.ServiceTests
{
    public class MunroServiceTest
    {
        readonly IMapper testMapper;

        public MunroServiceTest()
        {
            testMapper = MockAutoMapper.CreateMockMapper();

        }

        [Fact]
        public void GetMunros_WhenCalledWithDefaultMunroQuery_ReturnsAllMunrosWithAPost1997Category()
        {
            const int numberOfMuns = 10;
            const int numberOfTops = 10;
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(numberOfMuns, numberOfTops, 20, 600, 1500);

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery()).ToList();

            Assert.Equal(result.Count, numberOfMuns + numberOfTops);
            Assert.All(result, munro => Assert.NotEqual("", munro.HillCategory));
        }

        [Theory]
        [InlineData(10, 40, 50)]
        [InlineData(50, 20, 60)]
        [InlineData(15, 5, 5)]
        public void GetMunros_WhenCalledWithLimit_ReturnsLimitNumberOfValidMunros(int limit, int numMuns, int numTops)
        {
            
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(numMuns, numTops, 0, 600, 1500);

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                Limit = limit
            }).ToList();

            Assert.Equal(result.Count, Math.Min(limit, numMuns + numTops));
            Assert.All(result, munro => Assert.NotEqual("", munro.HillCategory));

        }

        [Fact]
        public void GetMunros_WhenCalledWithValidMinAndMaxHeightsEitherCategory_ReturnsOnlyMunrosWithinHeightRange()
        {
            
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(10, 10, 5, 600, 1500);

            const int minimumHeight = 650;
            const int maximumHeight = 1250;


            var count = fakeMunros.Count(fm => !string.IsNullOrEmpty(fm.HillCategoryPost1997)
                                               && fm.HeightInMetres >= minimumHeight
                                               && fm.HeightInMetres <= maximumHeight);
            

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                MinHeightInMetres = minimumHeight,
                MaxHeightInMetres = maximumHeight
            }).ToList();

            Assert.Equal(count, result.Count);
            Assert.NotEqual(fakeMunros.Count, result.Count );

            Assert.All(result, munro => Assert.InRange(munro.HeightInMetres, minimumHeight, maximumHeight));
        }

        [Theory]
        [InlineData(10, 10, HillCategory.Munro)]
        [InlineData(5, 10, HillCategory.Top)]
        [InlineData(15, 20, HillCategory.Either)]
        public void GetMunros_WhenCalledWithSpecificCategory_ReturnsOnlyMunrosWithThatCategory(int numMuns, int numTops, HillCategory category)
        {
            const int numBlanks = 20;
            var fakeDataContext = FakeMunroDataContext.GenerateFakeMunroDataContext(numMuns, numTops, numBlanks, 10, 1000);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                Category = category
            }).ToList();
            
            switch (category)
            {
                case HillCategory.Munro:
                    Assert.Equal(numMuns, result.Count);
                    Assert.All(result, munro => Assert.Equal("MUN", munro.HillCategory));
                    break;
                case HillCategory.Top:
                    Assert.Equal(numTops, result.Count);
                    Assert.All(result, munro => Assert.Equal("TOP", munro.HillCategory));
                    break;
                case HillCategory.Either:
                    Assert.Equal(numMuns + numTops, result.Count);
                    Assert.All(result, munro => Assert.NotEqual("", munro.HillCategory));
                    break;
            }
            
            Assert.NotEqual(result.Count, fakeDataContext.Munros.Count());
        }

        [Fact]
        public void GetMunros_WhenCalledWithEitherCategorySortByHeightAscThenNameDesc_ReturnsMunrosInCorrectOrder()
        {
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(20, 15, 10, 500, 2000);

            var orderedFakeMunros = fakeMunros.Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                                                .OrderBy(m => m.HeightInMetres)
                                                .ThenByDescending(m => m.Name).ToList();

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                SortBy = "asc(height),desc(name)"
            }).ToList();

            Assert.Equal(orderedFakeMunros.Count, result.Count);

            Assert.Equal(orderedFakeMunros.Select(m => m.Name), result.Select(m => m.Name));

        }

        [Fact]
        public void GetMunros_WhenCalledWithEitherCategorySortByHeightAscThenNameAsc_ReturnsMunrosInCorrectOrder()
        {
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(20, 15, 10, 500, 2000);

            var orderedFakeMunros = fakeMunros.Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                                                .OrderBy(m => m.HeightInMetres)
                                                .ThenBy(m => m.Name).ToList();

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                SortBy = "asc(height),asc(name)"
            }).ToList();

            Assert.Equal(orderedFakeMunros.Count, result.Count);

            Assert.Equal(orderedFakeMunros.Select(m => m.Name), result.Select(m => m.Name));

        }

        [Fact]
        public void GetMunros_WhenCalledWithEitherCategorySortByHeightDescThenNameDesc_ReturnsMunrosInCorrectOrder()
        {
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(20, 15, 10, 500, 2000);

            var orderedFakeMunros = fakeMunros.Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                                                .OrderByDescending(m => m.HeightInMetres)
                                                .ThenByDescending(m => m.Name).ToList();

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                SortBy = "desc(height),desc(name)"
            }).ToList();

            Assert.Equal(orderedFakeMunros.Count, result.Count);

            Assert.Equal(orderedFakeMunros.Select(m => m.Name), result.Select(m => m.Name));

        }

        [Fact]
        public void GetMunros_WhenCalledWithEitherCategorySortByHeightDescThenNameAsc_ReturnsMunrosInCorrectOrder()
        {
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(20, 15, 10, 500, 2000);

            var orderedFakeMunros = fakeMunros.Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                                                .OrderByDescending(m => m.HeightInMetres)
                                                .ThenBy(m => m.Name).ToList();

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                SortBy = "desc(height),asc(name)"
            }).ToList();

            Assert.Equal(orderedFakeMunros.Count, result.Count);

            Assert.Equal(orderedFakeMunros.Select(m => m.Name), result.Select(m => m.Name));

        }
        /// ********************
        /// 
        [Fact]
        public void GetMunros_WhenCalledWithEitherCategorySortByNameAscThenHeightDesc_ReturnsMunrosInCorrectOrder()
        {
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(20, 15, 10, 500, 2000);

            var orderedFakeMunros = fakeMunros.Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                                                .OrderBy(m => m.Name)
                                                .ThenByDescending(m => m.HeightInMetres).ToList();

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                SortBy = "asc(name),desc(height)"
            }).ToList();

            Assert.Equal(orderedFakeMunros.Count, result.Count);

            Assert.Equal(orderedFakeMunros.Select(m => m.Name), result.Select(m => m.Name));

        }

        [Fact]
        public void GetMunros_WhenCalledWithEitherCategorySortByNameAscThenHeightAsc_ReturnsMunrosInCorrectOrder()
        {
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(20, 15, 10, 500, 2000);

            var orderedFakeMunros = fakeMunros.Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                                                .OrderBy(m => m.Name)
                                                .ThenBy(m => m.HeightInMetres).ToList();

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                SortBy = "asc(name),asc(height)"
            }).ToList();

            Assert.Equal(orderedFakeMunros.Count, result.Count);

            Assert.Equal(orderedFakeMunros.Select(m => m.Name), result.Select(m => m.Name));

        }

        [Fact]
        public void GetMunros_WhenCalledWithEitherCategorySortByNameDescThenHeightDesc_ReturnsMunrosInCorrectOrder()
        {
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(20, 15, 10, 500, 2000);

            var orderedFakeMunros = fakeMunros.Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                                                .OrderByDescending(m => m.Name)
                                                .ThenByDescending(m => m.HeightInMetres).ToList();

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                SortBy = "desc(name),desc(height)"
            }).ToList();

            Assert.Equal(orderedFakeMunros.Count, result.Count);

            Assert.Equal(orderedFakeMunros.Select(m => m.Name), result.Select(m => m.Name));

        }

        [Fact]
        public void GetMunros_WhenCalledWithEitherCategorySortByNameDescThenHeightAsc_ReturnsMunrosInCorrectOrder()
        {
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(20, 15, 10, 500, 2000);

            var orderedFakeMunros = fakeMunros.Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                                                .OrderByDescending(m => m.Name)
                                                .ThenBy(m => m.HeightInMetres).ToList();

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                SortBy = "desc(name),asc(height)"
            }).ToList();

            Assert.Equal(orderedFakeMunros.Count, result.Count);

            Assert.Equal(orderedFakeMunros.Select(m => m.Name), result.Select(m => m.Name));

        }

        [Fact]
        public void GetMunros_WhenCalledWithEitherCategorySortByNameDescThenByNameAsc_ReturnsMunrosInCorrectDescendingOrder()
        {
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(20, 15, 10, 500, 2000);

            var orderedFakeMunros = fakeMunros.Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                                                    .OrderByDescending(m => m.Name).ToList();

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var result = service.GetMunros(new MunroQuery
            {
                SortBy = "desc(name),asc(name)"
            }).ToList();

            Assert.Equal(orderedFakeMunros.Count, result.Count);

            Assert.Equal(orderedFakeMunros.Select(m => m.Name), result.Select(m => m.Name));

        }

        [Fact]
        public void GetMunros_WhenCalledSortByWithEmptyStringBetweenCriteria_ReturnsMunrosInCorrectOrder()
        {
            var fakeMunros = FakeMunroDataContext.GenerateFakeMunros(20, 15, 10, 500, 2000);

            var orderedFakeMunros = fakeMunros.Where(m => !string.IsNullOrEmpty(m.HillCategoryPost1997))
                .OrderByDescending(m => m.Name).ToList();

            var fakeDataContext = new FakeMunroDataContext(fakeMunros);

            var service = new MunroService(fakeDataContext, testMapper);

            var query = new MunroQuery
            {
                SortBy = "desc(name),,asc(name)"
            };

            var result = service.GetMunros(query).ToList();

            Assert.Equal(2, query.SortingCriterias.Count);

            Assert.Equal(orderedFakeMunros.Count, result.Count);

            Assert.Equal(orderedFakeMunros.Select(m => m.Name), result.Select(m => m.Name));

        }

    }
}
