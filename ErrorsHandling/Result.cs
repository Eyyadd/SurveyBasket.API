namespace SurveyBasket.API.ErrorsHandling
{
    public  class Result
    {
        public bool IsSucess { get; }
        public Error Error { get; } = default!;

        public Result(bool isSuccess,Error Error)
        {
            //TODO Check That he should send the error if the isSuccess is false
            IsSucess = isSuccess;
            this.Error = Error;
        }
        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false,error);

        public static Result<TValue> Success<TValue>(TValue value)=> new(true,Error.None,value);
        public static Result<TValue> Failure<TValue>(Error error)=> new(false,error,default!);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;
        public Result(bool isSucess, Error Error, TValue value) : base(isSucess, Error)
        {
            _value = value;
        }

        public TValue? Value => IsSucess ?
            _value :
            throw new InvalidOperationException("failure result can not be have value");
    }
}
