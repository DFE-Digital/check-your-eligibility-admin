using AutoFixture;
using CheckYourEligibility.Admin.Models;
using CheckYourEligibility.Admin.UseCases;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;

namespace CheckYourEligibility.Admin.Tests.UseCases;

[TestFixture]
public class LoadParentDetailsUseCaseTests
{
    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<LoadParentDetailsUseCase>>();
        _sut = new LoadParentDetailsUseCase(_loggerMock.Object);
        _fixture = new Fixture();
    }

    private LoadParentDetailsUseCase _sut;
    private Mock<ILogger<LoadParentDetailsUseCase>> _loggerMock;
    private Fixture _fixture;

    [Test]
    public async Task Execute_WithValidData_ReturnsParentAndErrors()
    {
        // Arrange
        var parent = _fixture.Create<ParentGuardian>();
        var errors = new Dictionary<string, List<string>>
        {
            { "TestError", new List<string> { "Error message" } }
        };
        var parentJson = JsonConvert.SerializeObject(parent);
        var errorsJson = JsonConvert.SerializeObject(errors);

        // Act
        var result = await _sut.Execute(parentJson, errorsJson);
        var (resultParent, resultErrors) = result;

        // Assert
        resultParent.Should().BeEquivalentTo(parent);
        resultErrors.Should().BeEquivalentTo(errors);
    }

    [Test]
    public async Task Execute_WithNullData_ReturnsNullValues()
    {
        // Act
        var result = await _sut.Execute();
        var (resultParent, resultErrors) = result;

        // Assert
        resultParent.Should().BeNull();
        resultErrors.Should().BeNull();
    }
}