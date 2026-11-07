using BillingEngine.Infrastructure.Authentication;
using FluentAssertions;
using Xunit;

namespace BillingEngine.UnitTests.Authentication;

public class BcryptPasswordHasherTests
{
    private readonly BcryptPasswordHasher _sut;

    public BcryptPasswordHasherTests()
    {
        _sut = new BcryptPasswordHasher();
    }

    [Fact]
    public void Hash_ShouldReturnEncryptedString_DifferentFromPassword()
    {
        var password = "SuperSecretPassword123!";
        var hash = _sut.Hash(password);

        hash.Should().NotBeNullOrWhiteSpace();
        hash.Should().NotBe(password);
        hash.Should().StartWith("$2");
    }

    [Fact]
    public void Verify_ShouldReturnTrue_WhenPasswordIsCorrect()
    {
        var password = "SuperSecretPassword123!";
        var hash = _sut.Hash(password);

        var isValid = _sut.Verify(password, hash);

        isValid.Should().BeTrue();
    }

    [Fact]
    public void Verify_ShouldReturnFalse_WhenPasswordIsIncorrect()
    {
        var password = "SuperSecretPassword123!";
        var hash = _sut.Hash(password);

        var isValid = _sut.Verify("WrongPassword!", hash);

        isValid.Should().BeFalse();
    }
}