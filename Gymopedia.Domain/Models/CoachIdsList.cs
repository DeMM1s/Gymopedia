namespace Gymopedia.Domain.Models
{
    public class CoachIdsList
    {
        public int Id { get; init; }
        
        public int CoachId { get; init; }

        public CoachIdsList(int id, int coachId)
        {
            Id = id;
            CoachId = coachId;
        }
    }
}