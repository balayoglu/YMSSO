using YMDiscourseSSO.Models;

namespace YMDiscourseSSO.Services
{
    public interface IYourMembershipService
    {
        YmTokenResult GetYmTokenResult(string username, string password);
    }
}
