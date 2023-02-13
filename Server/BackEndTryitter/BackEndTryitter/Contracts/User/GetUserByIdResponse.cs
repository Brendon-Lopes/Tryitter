namespace BackEndTryitter.Contracts.User;

public record GetUserByIdResponse(
    Guid Id,
    string FullName,
    string Username,
    int CurrentModule,
    string StatusMessage);