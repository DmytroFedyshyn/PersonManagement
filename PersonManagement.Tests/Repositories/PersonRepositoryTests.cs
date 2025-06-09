using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using PersonManagement.DAL;
using PersonManagement.DAL.Entities;
using PersonManagement.DAL.Repositories;
using System.Linq.Expressions;
using Faker;

namespace PersonManagement.Tests.Repositories;

public class PersonRepositoryTests
{
    [Fact]
    public async Task AddAsync_Should_Call_DbSet()
    {
        // Arrange
        var person = new Person { FirstName = Name.First(), LastName = Name.Last() };
        var mockSet = new Mock<DbSet<Person>>();
        var mockContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockContext.Setup(x => x.Persons).Returns(mockSet.Object);

        var repo = new PersonRepository(mockContext.Object);

        // Act
        await repo.AddAsync(person);

        // Assert
        mockSet.Verify(x => x.AddAsync(person, default), Times.Once);
    }

    [Fact]
    public async Task SaveChangesAsync_Should_Call_Context()
    {
        // Arrange
        var mockContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        var repo = new PersonRepository(mockContext.Object);

        // Act
        await repo.SaveChangesAsync();

        // Assert
        mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
    } 
}
