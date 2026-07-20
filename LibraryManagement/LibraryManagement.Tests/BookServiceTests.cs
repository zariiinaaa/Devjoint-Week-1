using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Application.Services;
using Moq;
using Xunit;

namespace LibraryManagement.Tests;

public class BookServiceTests
{
    [Fact]
    public async Task GetByIdAsync_WhenBookExists_ReturnsBook()
    {

        var repositoryMock = new Mock<IBookRepository>();

        var book = new Book
        {
            Id = 1,
            Title = "Clean Code",
            BookCode = "BK-001",
            PublishedYear = 2008,
            TotalCopies = 5,
            AvailableCopies = 3
        };

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(1))
            .ReturnsAsync(book);

        var service = new BookService(repositoryMock.Object);


        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Clean Code", result.Title);
        Assert.Equal("BK-001", result.BookCode);
    }
}