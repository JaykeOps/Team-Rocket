using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class ContactInformation : ValueObject<ContactInformation>
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