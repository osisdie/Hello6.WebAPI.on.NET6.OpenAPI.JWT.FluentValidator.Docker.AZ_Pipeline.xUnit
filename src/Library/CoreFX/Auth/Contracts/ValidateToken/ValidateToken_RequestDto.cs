namespace CoreFX.Auth.Contracts.ValidateToken
{
    public class ValidateToken_RequestDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsGetProfile { get; set; }
    }
}
