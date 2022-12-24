using System.Collections.Generic;

namespace Diary_Sample.Infra
{
    public interface IJwtHandler
    {
        public string GenerateEncodedToken(string userName, string deviceId, IList<string> roles);
    }
}