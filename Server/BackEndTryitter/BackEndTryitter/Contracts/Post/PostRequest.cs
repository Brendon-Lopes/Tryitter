namespace BackEndTryitter.Contracts.Post
{
    public record PostRequest(
        string text,
        Guid userId);
}
