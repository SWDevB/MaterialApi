using MaterialApi.Controllers;
using MaterialApi.Models;
using MaterialApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MaterialApiTest
{
    public class MaterialsControllerTest
    {
        [Fact]
        public void GetNameEmptyNoMatchesResult200()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

            var result = materialsController.Get(null);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(((OkObjectResult)result.Result).Value);
            Assert.Empty((IEnumerable<Material>)((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public void GetNameSetNoMatchesResult200()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

            var result = materialsController.Get("name");
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(((OkObjectResult)result.Result).Value);
            Assert.Empty((IEnumerable<Material>)((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public void GetNameEmptyResult200()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

            #region oject initialization

            materialServiceFake.Repository.Add(
                new Material
                {
                    Id = "1",
                    Author = "Author1",
                    Hidden = false,
                    Name = "Name1",
                    Notes = "Notes1",
                    Phase = KindOfPhase.Continuous
                });
            materialServiceFake.Repository.Add(
                new Material
                {
                    Id = "2",
                    Author = "Author2",
                    Hidden = false,
                    Name = "Name2",
                    Notes = "Notes2",
                    Phase = KindOfPhase.Disperse
                });
            materialServiceFake.Repository.Add(
                new Material
                {
                    Id = "3",
                    Author = "Author3",
                    Hidden = false,
                    Name = "Somthing",
                    Notes = "Notes3",
                    Phase = KindOfPhase.Continuous
                });

            #endregion

            var result = materialsController.Get(null);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(((OkObjectResult)result.Result).Value);
            Assert.Equal(3, ((IEnumerable<Material>)((OkObjectResult)result.Result).Value).Count());
        }

        [Fact]
        public void GetNameSetResult200()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

            #region oject initialization

            materialServiceFake.Repository.Add(
                new Material
                {
                    Id = "1",
                    Author = "Author1",
                    Hidden = false,
                    Name = "Name1",
                    Notes = "Notes1",
                    Phase = KindOfPhase.Continuous
                });
            materialServiceFake.Repository.Add(
                new Material
                {
                    Id = "2",
                    Author = "Author2",
                    Hidden = false,
                    Name = "Name2",
                    Notes = "Notes2",
                    Phase = KindOfPhase.Disperse
                });
            materialServiceFake.Repository.Add(
                new Material
                {
                    Id = "3",
                    Author = "Author3",
                    Hidden = false,
                    Name = "Somthing",
                    Notes = "Notes3",
                    Phase = KindOfPhase.Continuous
                });

            #endregion

            var result = materialsController.Get("name");
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(((OkObjectResult)result.Result).Value);
            Assert.Equal(2, ((IEnumerable<Material>)((OkObjectResult)result.Result).Value).Count());
        }

        [Fact]
        public void GetResult500()
        {
            var materialServiceMock = new Mock<IMaterialService>();
            materialServiceMock
                .Setup(m => m.Get(It.IsAny<string>()))
                    .Throws<System.Exception>();

            var loggerMock = new Mock<ILogger<MaterialsController>>();

            var materialsController = new MaterialsController(materialServiceMock.Object, loggerMock.Object);
            materialsController.ProblemDetailsFactory = GetProblemsDetailsFactoryMock().Object;
            var result = materialsController.Get(null);

            Assert.IsType<ObjectResult>(result.Result);
            Assert.IsType<ProblemDetails>(((ObjectResult)result.Result).Value);
        }
        

        [Fact]
        public void GetByIdResult404()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

            var result = materialsController.GetById("1");
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetByIdResult500()
        {
            var materialServiceMock = new Mock<IMaterialService>();
            materialServiceMock
                .Setup(m => m.GetById(It.IsAny<string>()))
                .Throws<System.Exception>();

            var loggerMock = new Mock<ILogger<MaterialsController>>();

            var materialsController = new MaterialsController(materialServiceMock.Object, loggerMock.Object);
            materialsController.ProblemDetailsFactory = GetProblemsDetailsFactoryMock().Object;
            var result = materialsController.GetById("1");

            Assert.IsType<ObjectResult>(result.Result);
            Assert.IsType<ProblemDetails>(((ObjectResult)result.Result).Value);
        }

        [Fact]
        public void GetByIdResult200()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

            //Add material to Get
            materialServiceFake.Repository.Add(
                new Material
                {
                    Id = "1",
                    Author = "Author",
                    Hidden = false,
                    Name = "Name",
                    Notes = "Notes",
                    Phase = KindOfPhase.Continuous
                });


            var result = materialsController.GetById("1");

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(((OkObjectResult)result.Result).Value);

        }

        [Fact]
        public void PostResult200()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

            Material material = new Material()
            {
                Id = "Random",
                Author = "Author",
                Hidden = false,
                Name = "Name",
                Notes = "Notes",
                Phase = KindOfPhase.Continuous
            };

            var result = materialsController.Post(material);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(((OkObjectResult)result.Result).Value);
            Assert.Single(materialServiceFake.Repository);
            Assert.NotEqual("Random", ((Material)((OkObjectResult)result.Result).Value).Id);
        }

        [Fact]
        public void PostResult500()
        {
            Material material = new Material()
            {
                Id = "Random",
                Author = "Author",
                Hidden = false,
                Name = "Name",
                Notes = "Notes",
                Phase = KindOfPhase.Continuous
            };

            var materialServiceMock = new Mock<IMaterialService>();
            materialServiceMock
                .Setup(m => m.Add(It.IsAny<Material>()))
                .Throws<System.Exception>();

            var loggerMock = new Mock<ILogger<MaterialsController>>();

            var materialsController = new MaterialsController(materialServiceMock.Object, loggerMock.Object);
            materialsController.ProblemDetailsFactory = GetProblemsDetailsFactoryMock().Object;
            var result = materialsController.Post(material);

            Assert.IsType<ObjectResult>(result.Result);
            Assert.IsType<ProblemDetails>(((ObjectResult)result.Result).Value);
        }

        [Fact]
        public void PutResult400()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var loggerMock = new Mock<ILogger<MaterialsController>>();
            var materialsController = new MaterialsController(materialServiceFake, loggerMock.Object);

            Material material = new Material
            {
                Id = null,
                Author = "Author",
                Hidden = false,
                Name = "Name",
                Notes = "Notes",
                Phase = KindOfPhase.Continuous
            };

            var result = materialsController.Put(material);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PutResult404()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

            Material material = new Material
            {
                Id = "1",
                Author = "Author",
                Hidden = false,
                Name = "Name",
                Notes = "Notes",
                Phase = KindOfPhase.Continuous
            };

            var result = materialsController.Put(material);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PutResult200()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null); 
            
            //Add material to update
            materialServiceFake.Repository.Add(
                new Material
                {
                    Id = "1",
                    Author = "Author",
                    Hidden = false,
                    Name = "Name",
                    Notes = "Notes",
                    Phase = KindOfPhase.Continuous
                });

            Material changed =  new Material()
            {
                Id = "1",
                Author = "Somebody",
                Hidden = true,
                Name = "Name, First Name",
                Notes = "Notes Notes Notes",
                Phase = KindOfPhase.Disperse
            };

            var result = materialsController.Put(changed);

            Assert.IsType<OkResult>(result);
            //this actually tests the service fake, not the Controller
            Assert.Equal(changed.Author, materialServiceFake.Repository[0].Author);
            Assert.Equal(changed.Hidden, materialServiceFake.Repository[0].Hidden);
            Assert.Equal(changed.Id, materialServiceFake.Repository[0].Id);
            Assert.Equal(changed.Name, materialServiceFake.Repository[0].Name);
            Assert.Equal(changed.Notes, materialServiceFake.Repository[0].Notes);
            Assert.Equal(changed.Phase, materialServiceFake.Repository[0].Phase);
        }

        [Fact]
        public void PutResult500()
        {
            var materialServiceMock = new Mock<IMaterialService>();
            materialServiceMock
                .Setup(m => m.Update(It.IsAny<Material>()))
                .Throws<System.Exception>();

            var loggerMock = new Mock<ILogger<MaterialsController>>();

            var materialsController = new MaterialsController(materialServiceMock.Object, loggerMock.Object);
            materialsController.ProblemDetailsFactory = GetProblemsDetailsFactoryMock().Object;

            Material changed = new Material()
            {
                Id = "1",
                Author = "Somebody",
                Hidden = true,
                Name = "Name, First Name",
                Notes = "Notes Notes Notes",
                Phase = KindOfPhase.Disperse
            };

            var result = materialsController.Put(changed);

            Assert.IsType<ObjectResult>(result);
            Assert.IsType<ProblemDetails>(((ObjectResult)result).Value);
        }

        [Fact]
        public void DeleteResult404()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

             var result = materialsController.Delete("1");
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteResult200()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

            //Add material to delete
            materialServiceFake.Repository.Add(
                new Material
                {
                    Id = "1",
                    Author = "Author",
                    Hidden = false,
                    Name = "Name",
                    Notes = "Notes",
                    Phase = KindOfPhase.Continuous
                });


            var result = materialsController.Delete("1");

            Assert.IsType<OkResult>(result);
            //this actually tests the service fake, not the Controller
            Assert.Empty(materialServiceFake.Repository);
        }

        [Fact]
        public void DeleteResult500()
        {
            var materialServiceMock = new Mock<IMaterialService>();
            materialServiceMock
                .Setup(m => m.Delete(It.IsAny<string>()))
                .Throws<System.Exception>();

            var loggerMock = new Mock<ILogger<MaterialsController>>();

            var materialsController = new MaterialsController(materialServiceMock.Object, loggerMock.Object);
            materialsController.ProblemDetailsFactory = GetProblemsDetailsFactoryMock().Object;
            var result = materialsController.Delete("1");

            Assert.IsType<ObjectResult>(result);
            Assert.IsType<ProblemDetails>(((ObjectResult)result).Value);
        }

        /// <summary>
        /// Sets up a ProblemDetailsFactory Mock, which is needed to execute Problem within the Controller
        /// </summary>
        /// <returns>ProblemDetailsFactory Mock</returns>
        private Mock<ProblemDetailsFactory> GetProblemsDetailsFactoryMock()
        {
            var problemDetails = new ProblemDetails();
            var mock = new Mock<ProblemDetailsFactory>();
            mock
                .Setup(_ => _.CreateProblemDetails(
                    It.IsAny<HttpContext>(),
                    It.IsAny<int?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>())
                )
                .Returns(problemDetails);

            return mock;
        }
    }
}
