using BackEndTryitter.Models;

namespace BackEndTryitter.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);