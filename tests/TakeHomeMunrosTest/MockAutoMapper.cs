using AutoMapper;
using TakeHomeMunrosApi.Mappings;

namespace TakeHomeMunrosTest
{
    static class MockAutoMapper
    {
        public static IMapper CreateMockMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MunroProfile());
            });
            return mapperConfig.CreateMapper();
        }
    }
}
