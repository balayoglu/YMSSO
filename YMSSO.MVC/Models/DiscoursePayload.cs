using EnsureThat;

namespace YMDiscourseSSO.Models
{
    public class DiscoursePayload
    {
        public DiscoursePayload(
            string nonce,
            string name,
            string surname,
            string email,
            string externalId,
            bool? requireActivation)
        {
            this.Nonce = Ensure.String.IsNotNullOrWhiteSpace(nonce, nameof(nonce));
            this.Email = Ensure.String.IsNotNullOrWhiteSpace(email, nameof(email));
            this.Name = name;
            this.Surname = surname;
            this.ExternalId = externalId;
            this.RequireActivation = requireActivation;
        }

        public string Nonce { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string ExternalId { get; private set; }
        public bool? RequireActivation { get; private set; }
    }
}
