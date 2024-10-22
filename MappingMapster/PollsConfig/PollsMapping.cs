namespace SurveyBasket.API.MappingMapster.PollsConfig
{
    public class PollsMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Poll, CreatePollsRequest>().TwoWays();
            config.NewConfig<Poll,PollsResponse>().TwoWays();
            config.NewConfig<CreatePollsRequest, PollsResponse>().TwoWays();
        }
    }
}
