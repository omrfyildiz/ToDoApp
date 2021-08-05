using Microsoft.EntityFrameworkCore;
using System;
using ToDoApp.Data;
using ToDoApp.Services;
using Xunit;
using System.Linq;
using ToDoApp.Models;

namespace ToDoAppTestProject
{
    public class ToDoTest
    {
        [Fact]      //We use xUnit Fact when we have some criteria that always must be met, regardless of data. 
        public void Should_Get_InComplete_Item()
        {
            //Arrange
            int expectedItemCount = 1;
            string userId = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ToDoDbContext>().UseInMemoryDatabase(databaseName: "Test_GetItems").Options;

            using (var context = new ToDoDbContext(options))
            {
                context.ToDoItems.Add(new ToDoApp.Models.ToDoItem()
                {
                    IsDone = false,
                    OwnerId = userId,
                    StartedAt = DateTimeOffset.Now,
                    Title = "Unit Test"
                });

                context.SaveChanges();
                ToDoItemService toDoItemService = new ToDoItemService(context);

                //Act
                var result = toDoItemService.GetIncompleteItemsAsync(userId).Result.Count();

                //Assert
                Assert.True(result == expectedItemCount);
            }



        }

        [Fact]
        public void Should_Add_Item()
        {
            //Arrange
            string userId = Guid.NewGuid().ToString();
            NewToDoItem toDoItem = new NewToDoItem()
            {
                Title = "Unit Test"
            };

            var options = new DbContextOptionsBuilder<ToDoDbContext>().UseInMemoryDatabase(databaseName: "Test_GetItems").Options;

            using (var context = new ToDoDbContext(options))
            {

                ToDoItemService toDoItemService = new ToDoItemService(context);

                //Act
                var result = toDoItemService.AddItemAsync(toDoItem, userId).Result;

                //Assert
                Assert.True(result == true);


            }



        }

        [Fact]
        public void Should_Mark_Done_Item()
        {
            //Arrange
            string userId = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ToDoDbContext>().UseInMemoryDatabase(databaseName: "Test_GetItems").Options;

            using (var context = new ToDoDbContext(options))
            {
                context.ToDoItems.Add(new ToDoItem()
                {
                    IsDone = false,
                    OwnerId = userId,
                    StartedAt = DateTimeOffset.Now,
                    Title = "Unit Test"
                });

                context.SaveChanges();
                ToDoItemService toDoItemService = new ToDoItemService(context);

                //Act
                var result = toDoItemService.MarkDoneAsync(context.ToDoItems.First().Id).Result;

                //Assert
                Assert.True(result == true);
            }
        }
    }
}

