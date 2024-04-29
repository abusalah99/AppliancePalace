namespace AppliancePalaceWebsite;

public interface IJwtProvider
{
    string GenrateAccessToken(User user);
}
