using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHIS_Portal.Models.Entities
{
    public class EnroleeComplain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? AuthorizationCode { get; set; }
        public string EnroleeName { get; set; }
        public string EnroleeNumber { get; set; }
        public string Diagnosis { get; set; }
        public string? Complaint { get; set; }
        public int ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public ProviderDetails? ProviderDetails { get; set; }
        public int TreatmentId { get; set; }
        [ForeignKey("TreatmentId")]
        public TreatmentType TreatmentType { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }





    }
}
