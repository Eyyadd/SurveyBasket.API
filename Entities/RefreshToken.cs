namespace SurveyBasket.API.Entities
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedAt {  get; set; }
        public DateTime ExpireOn { get; set; }

        public bool IsExpire => DateTime.UtcNow >= ExpireOn;
        public bool IsActive => !IsExpire && RevokedAt is null;
    }
}
