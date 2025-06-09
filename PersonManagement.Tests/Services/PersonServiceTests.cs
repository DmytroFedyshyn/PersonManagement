using AutoMapper;
using Faker;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PersonManagement.BLL;
using PersonManagement.BLL.Requests;
using PersonManagement.BLL.Responses;
using PersonManagement.BLL.Services;
using PersonManagement.DAL;
using PersonManagement.DAL.Entities;
using PersonManagement.DAL.Interfaces;
using PersonManagement.DAL.Repositories;
using System.Linq.Expressions;

namespace PersonManagement.Tests.Services;

public class PersonServiceTests
{
    private readonly IMapper _mapper;

    public PersonServiceTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task AddPersonAsync_Should_Call_Add_And_Save()
    {
        // Arrange
        var dto = new PersonCreateResponse
        {
            FirstName = Name.First(),
            LastName = Name.Last(),
            City = Faker.Address.City(),
            AddressLine = Faker.Address.StreetAddress()
        };

        var mockRepo = new Mock<IPersonRepository>();
        var mockLogger = new Mock<ILogger<PersonService>>();

        var service = new PersonService(mockRepo.Object, _mapper, mockLogger.Object);

        // Act
        await service.AddPersonAsync(dto);

        // Assert
        mockRepo.Verify(r => r.AddAsync(It.Is<Person>(p =>
            p.FirstName == dto.FirstName &&
            p.LastName == dto.LastName &&
            p.Address != null &&
            p.Address.City == dto.City &&
            p.Address.AddressLine == dto.AddressLine
        )), Times.Once);

        mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetFilteredPersonsAsync_Should_Return_Filtered()
    {
        // Arrange
        var city = Faker.Address.City();
        var people = new List<Person>
        {
            new()
            {
                FirstName = Name.First(),
                LastName = Name.Last(),
                Address = new DAL.Entities.Address { City = city, AddressLine = Faker.Address.StreetAddress() }
            }
        };

        var mockRepo = new Mock<IPersonRepository>();
        var mockLogger = new Mock<ILogger<PersonService>>();

        mockRepo
            .Setup(r => r.GetAllAsync(
                It.IsAny<Expression<Func<Person, bool>>>(),
                It.IsAny<Expression<Func<Person, object>>[]>()))
            .ReturnsAsync(people);

        var service = new PersonService(mockRepo.Object, _mapper, mockLogger.Object);

        var filter = new GetAllRequest { City = city };

        // Act
        var result = await service.GetFilteredPersonsAsync(filter);

        // Assert
        result.Should().BeEquivalentTo(people);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Correct_Items_By_City_Filter()
    {
        // Arrange
        var city = Faker.Address.City();
        var expectedPerson = new Person
        {
            FirstName = Name.First(),
            LastName = Name.Last(),
            Address = new DAL.Entities.Address
            {
                City = city,
                AddressLine = Faker.Address.StreetAddress()
            }
        };

        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        await using (var context = new AppDbContext(dbOptions))
        {
            context.Persons.AddRange(
                expectedPerson,
                new Person
                {
                    FirstName = Name.First(),
                    LastName = Name.Last(),
                    Address = new DAL.Entities.Address
                    {
                        City = Faker.Address.City(),
                        AddressLine = Faker.Address.StreetAddress()
                    }
                }
            );
            await context.SaveChangesAsync();
        }

        await using (var context = new AppDbContext(dbOptions))
        {
            var repository = new PersonRepository(context);

            Expression<Func<Person, bool>> filter = p => p.Address != null && p.Address.City == city;

            // Act
            var result = await repository.GetAllAsync(filter, p => p.Address!);

            // Assert
            result.Should().ContainSingle()
                .Which.Should().BeEquivalentTo(expectedPerson, options =>
                    options.ExcludingMissingMembers());
        }
    }
}
