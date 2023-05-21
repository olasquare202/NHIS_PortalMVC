namespace NHIS_Portal.Models.Complains
{
    public class CreateComplain
    {
        public int ProviderId { get; set; }
        public int TreatmentId { get; set; }
        public int LocationId { get; set; }
        public string AuthorizationCode { get; set; }
        public string EnroleeName { get; set; }
        public string EnroleeNumber { get; set; }
        public string Diagnosis { get; set; }
        public string Complaint { get; set; }
    }
}
