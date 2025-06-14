namespace Docmino.Application.Models.External.Email;
public class ApprovalNotificationEmail : BaseEmailContent
{
    public string ReviewerName { get; set; }
    public string DocumentName { get; set; }
    public string CodeNotation { get; set; }
    public string DocumentTypeName { get; set; }
    public int CurrentStepNumber { get; set; }
    public string UrgentPriorityName { get; set; }
    public string SignTypeName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }

}
