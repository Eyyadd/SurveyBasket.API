namespace SurveyBasket.API.Setting
{
    public class JwtOptions
    {
        public string Issuer {  get; set; } =string.Empty;
        public string Auidence { get; set; } = string.Empty;
        public string SecurityKey { get; set; } = string.Empty;
        public int ExpireTokenIn {  get; set; }  
       
      
    }
}
