using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class ContactInformation : ValueObject<ContactInformation>
    {
        private PhoneNumber phone;
        private EmailAddress email;

        public PhoneNumber Phone
        {
            get { return this?.phone; }
            set { this.phone = value; }
        }

        public EmailAddress Email
        {
            get { return this?.email; }
            set { this.email = value; }
        }

        public ContactInformation(PhoneNumber phone, EmailAddress email)
        {
            this.phone = phone;
            this.email = email;
        }

        public ContactInformation()
        {
        }
    }
}