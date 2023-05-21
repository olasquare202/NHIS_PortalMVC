namespace NHIS_Portal.Models.AddNHIS_Provider
{
    public class CreateProvider
    {
        public string ProviderName { get; set; }
        public string NHIScode { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
