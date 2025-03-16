namespace MyRestAPI.Models
{
    public class Partner
    {
        public Partner(string partnerNo, string allowedPartner, string partnerPassword)
        {
            this.partnerNo = partnerNo;
            this.allowedPartner = allowedPartner;
            this.partnerPassword = partnerPassword;
        }

        public string partnerNo { get; set; }
        public string allowedPartner { get; set; }
        public string partnerPassword { get; set; }
    }

    public class AllowedPartner
    {
        public List<Partner> Partners
        {
            get
            {
                return new List<Partner>
                {
                    new Partner("FG-00001","FAKEGOOGLE","FAKEPASSWORD1234"),
                    new Partner("FG-00002","FAKEPEOPLE","FAKEPASSWORD4578"),
                };
            }
        }
    }
}
