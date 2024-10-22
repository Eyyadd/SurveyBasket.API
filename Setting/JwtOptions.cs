namespace SurveyBasket.API.Setting
{
    public class JwtOptions
    {
        public string Issuer {  get; set; }
        public string Auidence {  get; set; }
        public string SecurityKey {  get; set; }
        public int ExpireIn {  get; set; }
      
    }
}
