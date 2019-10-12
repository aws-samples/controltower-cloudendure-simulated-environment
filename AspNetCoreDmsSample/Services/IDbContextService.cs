using DMSSample.Models;

namespace DMSSample.Services
{
    public interface IDbContextService
    {
        BaseContext CreateDbContext(LoginModel model);
        BaseContext CreateReplicaDbContext(LoginModel model);
    }
}