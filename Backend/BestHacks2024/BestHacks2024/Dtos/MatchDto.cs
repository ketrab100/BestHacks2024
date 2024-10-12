namespace BestHacks2024.Interfaces;

public class MatchDto
{
    public Guid EmployeeId { get; set; }
    public Guid JobId { get; set; }
    
    public bool DidEmployeeAcceptJobOffer { get; set; }
    public bool DidEmployerAcceptCandidate { get; set; }
}