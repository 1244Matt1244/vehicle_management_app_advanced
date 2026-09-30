using FluentAssertions;
using VehicleApp.Application.Common.Models;
using Xunit;

namespace VehicleApp.UnitTests;

public class PaginatedListTests
{
    [Fact]
    public void Create_Should_Calculate_TotalPages_Correctly()
    {
        var items = new List<int> { 1, 2, 3 };
        var result = PaginatedList<int>.Create(items, 10, 1, 3);

        result.Items.Should().HaveCount(3);
        result.TotalCount.Should().Be(10);
        result.TotalPages.Should().Be(4);
        result.PageIndex.Should().Be(1);
    }

    [Fact]
    public void HasPreviousPage_Should_Be_False_On_First_Page()
    {
        var result = PaginatedList<int>.Create(new List<int>(), 10, 1, 5);
        result.HasPreviousPage.Should().BeFalse();
    }

    [Fact]
    public void HasPreviousPage_Should_Be_True_On_Later_Pages()
    {
        var result = PaginatedList<int>.Create(new List<int>(), 10, 2, 5);
        result.HasPreviousPage.Should().BeTrue();
    }

    [Fact]
    public void HasNextPage_Should_Be_True_When_More_Pages_Exist()
    {
        var result = PaginatedList<int>.Create(new List<int>(), 10, 1, 5);
        result.HasNextPage.Should().BeTrue();
    }

    [Fact]
    public void HasNextPage_Should_Be_False_On_Last_Page()
    {
        var result = PaginatedList<int>.Create(new List<int>(), 10, 2, 5);
        result.HasNextPage.Should().BeFalse();
    }
}
