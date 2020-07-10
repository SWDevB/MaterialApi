using MaterialApi.Controllers;
using MaterialApi.Models;
using MaterialApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xunit;

namespace MaterialApiTest
{
    public class MaterialsControllerTest
    {
        [Fact]
        public void GetWithoutNameEmpty()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

            var result = materialsController.Get(null);
            Assert.Empty(result);
        }

        [Fact]
        public void GetWithNameEmpty()
        {
            var materialServiceFake = new MaterialServiceFake(false);
            var materialsController = new MaterialsController(materialServiceFake, null);

            var result = materialsController.Get("name");
            Assert.Empty(result);
        }

        [Fact]
        public void GetWithoutName()
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
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetWithName()
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
            Assert.Equal(2, result.Count());
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
        public void Post()
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
            Assert.NotNull(result);
            Assert.Single(materialServiceFake.Repository);
            Assert.NotEqual("Random", result.Id);
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
    }
}