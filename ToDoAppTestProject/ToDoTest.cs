using Microsoft.EntityFrameworkCore;
using System;
using ToDoApp.Data;
using ToDoApp.Services;
using Xunit;
using System.Linq;

namespace ToDoAppTestProject
{
    public class ToDoTest
    {
        [Fact]
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
                Assert.True(result==expectedItemCount);
            }



        }
    }
}
