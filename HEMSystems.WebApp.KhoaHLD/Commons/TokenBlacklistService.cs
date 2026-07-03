using System.Collections.Concurrent;

namespace HEMSystems.WebApp.KhoaHLD.Commons;

public class TokenBlacklistService : ITokenBlacklistService
{
    private readonly ConcurrentDictionary<string, DateTime> _revoked = new();

    public void Revoke(string jti, DateTime expiresUtc)
    {
        if (string.IsNullOrWhiteSpace(jti))
        {
            return;
        }

        _revoked[jti] = expiresUtc;
    }

    public bool IsRevoked(string jti)
    {
        if (string.IsNullOrWhiteSpace(jti))
        {
            return false;
        }

        if (!_revoked.TryGetValue(jti, out var expiresUtc))
        {
            return false;
        }

        if (expiresUtc <= DateTime.UtcNow)
        {
            _revoked.TryRemove(jti, out _);
            return false;
        }

        return true;
    }
}
