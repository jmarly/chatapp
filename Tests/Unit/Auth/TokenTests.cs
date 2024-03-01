using System.Linq.Expressions;
using net.applicationperformance.ChatApp.Auth;

namespace ChatApp.Tests.Unit.Auth;

[TestFixture]
public class TokenTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MustPassGenerateToken()
    {
        // arrange
        var id = Guid.NewGuid();
        const string userName = "a :.;$user name";
        // act
        var token = Token.ComputeToken(id,userName);
        var tokenId = Token.ExtractId(token);
        
        // assert
        Assert.That(tokenId, Is.InstanceOf(typeof(Guid)));
        Assert.That(tokenId, Is.EqualTo(id));
    }

    [Test]
    public void MustPassValidToken()
    {
        // arrange
        var id = Guid.NewGuid();
        const string userName = "a :.;$user name";
        var token = Token.ComputeToken(id,userName);

        // act
        var valid = Token.ValidateToken(userName, token);
        
        // assert
        Assert.That(valid, Is.True);
    }

    [Test]
    public void MustFailCorruptToken()
    {
        // arrange
        var id = Guid.NewGuid();
        const string userName = "a :.;$user name";
        var token = Token.ComputeToken(id,userName);
        var corruptedToken = token.Replace("a", "b").Replace("A", "X");

        
        // act
        var valid = Token.ValidateToken(userName, corruptedToken);
        
        // assert
        Assert.That(valid, Is.False);
    }

    [Test]
    public void MustFailWrongUser()
    {
        // arrange
        var id = Guid.NewGuid();
        const string userName = "a :.;$user name";
        var token = Token.ComputeToken(id,userName);

        
        // act
        var valid = Token.ValidateToken("wronguser", token);
        
        // assert
        Assert.That(valid, Is.False);
    }


    [Test]
    public void MustFailGenerateTokenEmptyUser()
    {
        // arrange
        var id = Guid.NewGuid();
        const string userName = "";
        // act
        var token = Token.ComputeToken(id,userName);
        
        // assert
        Assert.That(token, Is.Empty);
    }
}