namespace Domain.Value_Objects
{
    public class ContactInformation
    {
        public PhoneNumber Phone { get; set; }
        public EmailAddress Email { get; set; }
        public ContactInformation(PhoneNumber phone, EmailAddress email)
        {
            this.Phone = phone;
            this.Email = email;
        }
    }
}