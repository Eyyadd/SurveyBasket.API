namespace SurveyBasket.API.ErrorsHandling
{
    public record Error(string code,string description,int? statuesCode)
    {
        public static readonly Error None = new Error(string.Empty,string.Empty,statuesCode:null);
    }
}
