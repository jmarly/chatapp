using net.applicationperformance.ChatApp.Auth;
using net.applicationperformance.ChatApp.BusinessLogic;
using net.applicationperformance.ChatApp.Models;
using net.applicationperformance.ChatApp.Repositories;


namespace ChatApp.Tests.Unit.BusinessLogic;

[TestFixture]
public class UsersTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MustPassSignIn()
    {
        // arrange
        var id = Guid.NewGuid();
        var userName = "thisIsTheUser";
        var repo = new Mock<IUserRepository>();
        repo.Setup(repo => repo.Add(It.IsAny<UserDto>())).Returns(id);

        var users = new Users(repo.Object);
        
        // act
        var result = users.SignIn(userName, "", "");
        
        // assert
        var tokenId = Token.ExtractId(result);
        var validToken = Token.ValidateToken(userName,result);
        Assert.That(id,Is.EqualTo(tokenId));
        Assert.That(validToken,Is.True);
    }
}