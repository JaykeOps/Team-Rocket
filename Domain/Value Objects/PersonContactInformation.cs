namespace Domain.Value_Objects
{
    public class ContactInformation
    {
        public ContactInformation(PhoneNumber phone, EmailAddress email)
        {
            this.Phone = phone;
            this.Email = email;
        }

        public PhoneNumber Phone { get; }
        public EmailAddress Email { get; }
    }
}