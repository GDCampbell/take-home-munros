using Microsoft.AspNetCore.Mvc;

using TakeHomeMunros.Data;
using TakeHomeMunrosApi.Controllers;
using TakeHomeMunrosApi.Queries;
using TakeHomeMunrosApi.Services;
using Xunit;

namespace TakeHomeMunrosTest.ControllerTests
{
    public class MunroControllerTest
    {
        readonly MunrosController controller;

        public MunroControllerTest()
        {
            var dataContext = new MunroDataContext();
            var service = new MunroService(dataContext, MockAutoMapper.CreateMockMapper());
            controller = new MunrosController(service);
        }

        [Theory]
        [InlineData(100, 0)]
        public void Get_WithMaxHeightLessThanMinHeight_ReturnsBadRequest(double minimumHeight, double maximumHeight)
        {
            var result = controller.Get(new MunroQuery
            {
                MinHeightInMetres = minimumHeight,
                MaxHeightInMetres = maximumHeight
            });

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

    }
}
