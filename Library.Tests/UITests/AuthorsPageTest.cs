using Bunit;
using Library.Domain.Entities;
using Library.FluentUI.Components.Pages.AuthorComponents;
using Library.ServicesInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using Moq;
using NSubstitute;
using Shouldly;

public class AuthorsTest
{
    [Fact]
    public void ShouldRenderLoadingTextWhileDataLoadsAsync()
    {
        // Arrange
        var mockAuthorService = new Mock<IAuthorService>();
        mockAuthorService.Setup(x => x.GetAll()).Returns(Task.FromResult(new List<Author>()));
        var mockKeyCodeService = new Mock<IKeyCodeService>();

        using var ctx = new TestContext();
        ctx.Services.AddSingleton<IAuthorService>(mockAuthorService.Object);
        ctx.Services.AddSingleton<IDialogService>(Mock.Of<IDialogService>()); // Mock DialogService
        ctx.Services.AddSingleton<IKeyCodeService>(mockKeyCodeService.Object); // Add Mock for IKeyCodeService
        ctx.JSInterop.SetupModule("./_content/Microsoft.FluentUI.AspNetCore.Components/Components/Divider/FluentDivider.razor.js");
        ctx.JSInterop.SetupVoid("setDividerAriaOrientation");
        // Act
        var rendered = ctx.RenderComponent<AuthorsListPage>();

        // Assert
        rendered.Find("span").TextContent.ShouldBe("Loading...");
        //rendered.Find("FluentDataGrid").ShouldBeNull();
    }

    [Fact]
    public void ShouldRenderDataGridWithAuthorsAsync()
    {
        // Arrange
        var mockAuthorService = new Mock<IAuthorService>();
        var authors = new List<Author>() { 
            new() { Name = "Test Author1" },
            new() { Name = "Test Author2" } 
        };
        mockAuthorService.Setup(x => x.GetAll()).Returns(Task.FromResult(authors));
        var mockKeyCodeService = new Mock<IKeyCodeService>();

        using var ctx = new TestContext();
        ctx.Services.AddSingleton<IAuthorService>(mockAuthorService.Object);
        ctx.Services.AddSingleton<IDialogService>(Mock.Of<IDialogService>()); // Mock DialogService
        ctx.Services.AddSingleton<IKeyCodeService>(mockKeyCodeService.Object); // Add Mock for IKeyCodeService

        ctx.JSInterop.SetupModule("./_content/Microsoft.FluentUI.AspNetCore.Components/Components/Divider/FluentDivider.razor.js");
        ctx.JSInterop.SetupVoid("setDividerAriaOrientation");
        // Act
        var rendered = ctx.RenderComponent<AuthorsListPage>();

        // Assert
        rendered.Find("span").ShouldBe(null);
        var dataGrid = rendered.Find("FluentDataGrid");
        dataGrid.ShouldNotBe(null);
        dataGrid.GetAttribute("Items").ShouldNotBeOfType<List<Author>>();
    }

    [Fact]
    public void ShouldCallShowAddModalOnClick()
    {
        // Arrange
        var mockAuthorService = new Mock<IAuthorService>();
        var mockDialogService = new Mock<IDialogService>();
        var mockKeyCodeService = new Mock<IKeyCodeService>();

        mockAuthorService.Setup(x => x.GetAll()).Returns(Task.FromResult(new List<Author>()));

        using var ctx = new TestContext();
        ctx.Services.AddSingleton<IAuthorService>(mockAuthorService.Object);
        ctx.Services.AddSingleton<IDialogService>(mockDialogService.Object);
        ctx.Services.AddSingleton<IKeyCodeService>(mockKeyCodeService.Object); // Add Mock for IKeyCodeService
        ctx.JSInterop.SetupModule("./_content/Microsoft.FluentUI.AspNetCore.Components/Components/Divider/FluentDivider.razor.js");
        ctx.JSInterop.SetupVoid("setDividerAriaOrientation");

        var rendered = ctx.RenderComponent<AuthorsListPage>();

        // Act
        rendered.Find("button").Click();

        // Assert
        mockDialogService.Verify(x => x.ShowDialogAsync<AddEditAuthorDialog>(It.IsAny<Author>(), It.IsAny<DialogParameters>()), Times.Once);
    }

    // You can add similar tests for other functionalities like DeleteAuthor, RefreshList, etc.

}
