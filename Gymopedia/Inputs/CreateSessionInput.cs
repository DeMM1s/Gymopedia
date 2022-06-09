
namespace Gymopedia.Inputs
{
    public class CreateSessionInput
    {
        public DateTime From { get; set; }
        public DateTime Until { get; set; }
        public int MaxClient { get; set; }
        public int CoachWorkDayId { get; set; }
    }
}
