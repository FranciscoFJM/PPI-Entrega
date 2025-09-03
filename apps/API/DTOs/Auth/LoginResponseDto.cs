namespace API.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string TokenType { get; set; } = "Bearer";
    }
}
