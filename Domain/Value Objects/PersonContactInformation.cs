namespace Domain.Value_Objects
{
    public class ContactInformation
    {
        public PhoneNumber Phone { get; }
        public EmailAddress Email { get; }
        public ContactInformation(PhoneNumber phone, EmailAddress email)
        {
            this.Phone = phone;
            this.Email = email;
        }
    }
}