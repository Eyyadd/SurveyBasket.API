using SurveyBasket.API.ErrorsHandling;

namespace SurveyBasket.API.Helper.Extensions
{
    public static class ResultExtensions
    {
        public static ObjectResult Problems(this Result result)
        {
            if (result.IsSucess)
                throw new InvalidOperationException("On success : should not have an errors");

            var problems = Results.Problem(statusCode:result.Error.statuesCode);
            // refletion the object to get the problem details property and this retur objec so with unboxing convert to problem details
            var problemDetails = problems.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problems) as ProblemDetails;
            problemDetails!.Extensions = new Dictionary<string, object?>
            {
                {
                    "Errors",new[]
                    {
                        new
                        {
                            result.Error.statuesCode,
                            result.Error.description,
                            result.Error.code
                        }
                    }
                }
            };

            return new ObjectResult(problemDetails);
        }
    }
}
