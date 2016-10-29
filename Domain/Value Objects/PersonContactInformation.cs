namespace Domain.Value_Objects
{
    public class PersonContactInformation
    {
        public PersonContactInformation(PhoneNumber phone, EmailAddress email)
        {
            this.Phone = phone;
            this.Email = email;
        }

        public PhoneNumber Phone { get; }
        public EmailAddress Email { get; }
    }
}