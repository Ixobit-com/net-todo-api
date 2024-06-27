namespace Todo.Logic.Domain.Models {
    public class AccessTokenDto {
        public string? AccessToken { get; set; }
        public long ExpireIn { get; set; }
        public string? RefreshToken { get; set; }
    }
}