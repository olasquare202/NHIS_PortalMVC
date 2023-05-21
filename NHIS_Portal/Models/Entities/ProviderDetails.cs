using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHIS_Portal.Models.Entities
{
    public class ProviderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public string ProviderCode { get; set; }
        public string ProviderAddress { get; set; }
        public string ProviderEmail { get; set; }
        public string ProviderPhoneNumber { get; set; }
        public string? HMO_Officer { get; set; }
        public bool? HMO_Status { get; set; }
    }
}
