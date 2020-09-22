using System;
using Xunit;
using Bunit;
using Bunit.Mocking.JSInterop;
using static Bunit.ComponentParameterFactory;
using Beam.Tests.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Index = Beam.Client.Pages.Index;
using Microsoft.AspNetCore.Components;

namespace Beam.Tests
{
    /// <summary>
    /// These tests are written entirely in C#.
    /// Learn more at https://bunit.egilhansen.com/docs/
    /// </summary>
    public class IndexPageTests : TestContext
    {
        [Fact]
        public void CounterStartsAtZero()
        {
            Services.AddAuthenticationServices(TestAuthenticationStateProvider.CreateAuthenticationState("TestUser"));
            var wrapper = RenderComponent<CascadingAuthenticationState>(p => p.AddChildContent<Index>());
            
            // Arrange
            var cut = wrapper.FindComponent<Index>();

            // Assert that content of the paragraph shows counter at zero
            Assert.Contains("Select a Frequency, or create a new one", cut.Markup);
            cut.MarkupMatches(@"<h2 diff:ignore></h2>
                                <p diff:ignore></p>"); 
        }

        [Fact]
        public void IndexShowLoginLinkWhenUnauthorized()
        {
            Services.AddAuthenticationServices(TestAuthenticationStateProvider.CreateUnauthenticationState(), AuthorizationResult.Failed());

            Services.AddScoped<NavigationManager, MockNavigationManager>();

            var wrapper = RenderComponent<CascadingAuthenticationState>(
                parameters => parameters.AddChildContent<Index>());

            var cut = wrapper.FindComponent<Index>();

            Assert.DoesNotContain("Select a Frequency, or create a new one", cut.Markup);

            cut.Find("a").MarkupMatches(@"<a href=""/login"">Log In</a>");
        }
    }
}