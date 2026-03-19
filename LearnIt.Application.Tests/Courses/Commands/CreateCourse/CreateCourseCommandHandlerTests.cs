using System.Runtime.CompilerServices;
using FluentAssertions;
using LearnIt.Application.Features.Courses.Commands.CreateCourse;
using LearnIt.Domain.Abstractions;
using LearnIt.Domain.Entities;
using Moq;

namespace LearnIt.Application.Tests.Courses.Commands.CreateCourse;

public sealed class CreateCourseCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Course_And_Save_Changes()
    {
        var repositoryMock = new Mock<ICourseRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        Course? addedCourse = null;

        repositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>()))
            .Callback<Course, CancellationToken>((course, _) => addedCourse = course)
            .Returns(Task.CompletedTask);

        unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateCourseCommandHandler(
            repositoryMock.Object,
            unitOfWorkMock.Object);
        
        var command = new CreateCourseCommand(
            "Test Command Name",
            "Test Command Description",
            180);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();
        addedCourse.Should().NotBeNull();
        addedCourse.Title.Should().Be("Test Command Name");
        addedCourse.Description.Should().Be("Test Command Description");
        addedCourse.Duration.TotalMinutes.Should().Be(180);

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Course>(), It.IsAny<CancellationToken>()),
            Times.Once
        );

        unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}