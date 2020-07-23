using System;
using Xunit;
using SiteMapper;
using SiteMapper.Core;
using SiteMapper.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace SiteMapper.Tests
{
    public class ParseLinksTests
    {
        [Theory]
        [InlineData("https://www.something.com/something.html")]
        [InlineData("https://www.something.com/something/")]
        [InlineData("https://www.something.com/some-thing/")]
        public void CreateLink_LinkIsPage(string link)
        {
            //Arrange
            var convertToLink = new ConvertToLink(link, "https://www.something.com");
            var expected = new Link { Url = link, LinkType = LinkType.Page, LinkSource = LinkSource.Internal };
            //Act
            var actual = convertToLink.CreateLinkFromFullAddress(link, LinkSource.Internal);
            //Assert
            Assert.Equal(expected.LinkType, actual.LinkType);
        }

        [Fact]
        public void CreateLink_LinkIsFile()
        {
            //Arrange
            var convertToLink = new ConvertToLink("https://www.something.com/something.png", "https://www.something.com");
            var expected = new Link { Url = "https://www.something.com/something.png", LinkType = LinkType.File, LinkSource = LinkSource.Internal };
            //Act
            var actual = convertToLink.CreateLinkFromFullAddress("https://www.something.com/something.png", LinkSource.Internal);
            //Assert
            Assert.Equal(expected.LinkType, actual.LinkType);
        }

        [Theory]
        [InlineData("/something", "https://www.something.com/athing/something")]
        [InlineData("/something/", "https://www.something.com/athing/something")]
        [InlineData("something.html", "https://www.something.com/athing/something.html")]
        [InlineData("something.png", "https://www.something.com/athing/something.png")]
        [InlineData("../andanotherthing/", "https://www.something.com/andanotherthing")]
        public void PartialAddressConvertToLink_Works(string partial, string expected)
        {
            //Arrange
            var convertToLink = new ConvertToLink(partial, "https://www.something.com/athing/");
            //Act
            var actual = convertToLink.PartialAddressConvertToLink(partial);
            //Assert
            Assert.Equal(expected, actual.Url);
        }

        [Fact]
        public void FullAddressConvertToLink_FindsInternal()
        {
            //Arrange
            var convertToLink = new ConvertToLink("https://www.something.com/somethingelse", "https://www.something.com");
            var expected = new Link { Url = "https://www.something.com/somethingelse", LinkType = LinkType.File, LinkSource = LinkSource.Internal };
            //Act
            var actual = convertToLink.FullAddressConvertToLink("https://www.something.com/somethingelse");
            //Assert
            Assert.Equal(expected.LinkSource, actual.LinkSource);
        }

        [Fact]
        public void FullAddressConvertToLink_FindsExternal()
        {
            //Arrange
            var convertToLink = new ConvertToLink("https://www.somethingelse.com/something", "https://www.something.com");
            var expected = new Link { Url = "https://www.something.com/somethingelse", LinkType = LinkType.File, LinkSource = LinkSource.External };
            //Act
            var actual = convertToLink.FullAddressConvertToLink("https://www.somethingelse.com/something");
            //Assert
            Assert.Equal(expected.LinkSource, actual.LinkSource);
        }
    }
}
