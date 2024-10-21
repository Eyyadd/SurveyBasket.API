namespace SurveyBasket.API.IServices
{
    public interface IPollService
    {
        IReadOnlyList<Poll> GetPolls();
        Poll? GetPollById(int id);

        Poll? AddPoll(Poll poll);
        Poll? UpdatePoll(Poll poll);
        void DeletePoll(int id);
    }
}
