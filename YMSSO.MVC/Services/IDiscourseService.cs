using System.Threading.Tasks;
using YMDiscourseSSO.Models;

namespace YMDiscourseSSO.Services
{
    public interface IDiscourseService
    {
        string GetPlainNonce(string encodedNonce);

        string GetRedirectUrl(DiscoursePayload discoursePayload);
    }
}
