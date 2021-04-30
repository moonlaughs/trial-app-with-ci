using trial_api.Data;
using trial_api.Models;

namespace trial_api.Repositories
{
    public class EfCoreUserRepository : EfCoreRepository<Book, DataContext>
    {
        // For specific to the user methods if to be added
        public EfCoreUserRepository(DataContext context) : base(context)
        {

        }
    }
}