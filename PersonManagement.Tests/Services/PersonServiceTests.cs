using AutoMapper;
using Faker;
using FluentAssertions;
using Moq;
using PersonManagement.BLL;
using PersonManagement.BLL.DTOs;
using PersonManagement.BLL.Services;
using PersonManagement.DAL.Entities;
using PersonManagement.DAL.Interfaces;
using Microsoft.Extensions.Logging;
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
        var dto = new PersonCreateDto
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
}
