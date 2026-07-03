namespace HEMSystems.WebApp.KhoaHLD.Commons;

public interface ITokenBlacklistService
{
    void Revoke(string jti, DateTime expiresUtc);

    bool IsRevoked(string jti);
}
