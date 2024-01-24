using database.context;
using database.Models;
using repository.Interface;


namespace repository.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DbContext dbContext;

        // Constructor for the JobRepository class, which injects a DbContext.
        public AuthRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public User auth(SingleSignOn singleSignOn)
        {
            singleSignOn.Id = Guid.Empty;
            singleSignOn.ProviderData.ForEach(x => x.Id = Guid.Empty);
            singleSignOn.UserId = Guid.Empty;
            User user = dbContext.User.Where(u => u.Email.Equals(singleSignOn.Email)).FirstOrDefault();
            if (user == null)
            {
                User newUser = new User();
                newUser.Email = singleSignOn.Email;
                string? displayName = singleSignOn?.ProviderData?.FirstOrDefault()?.DisplayName;
                newUser.FirstName = displayName?.Split(' ').Length > 0 
                    ? displayName?.Split(' ')[0] : "";
                newUser.LastName = displayName?.Split(' ').Length > 0 
                    ? displayName?.Split(' ')[1] : "";
                newUser.CreatedDate = DateTime.Now;
                newUser.UpdatedDate = DateTime.Now;
                dbContext.Add(newUser);
                dbContext.SaveChanges();
            }

            User savedUser = dbContext.User.Where(u => u.Email.Equals(singleSignOn.Email)).FirstOrDefault();
            if(savedUser != null)
            {
                singleSignOn.UserId = savedUser.Id;
                var listData = singleSignOn.ProviderData.ToList();
                singleSignOn.ProviderData = null;
                dbContext.Add(singleSignOn);
                dbContext.SaveChanges();


                listData.ForEach((x) =>
                {
                    ProviderData providerData = new ProviderData();
                    providerData.Email = x.Email;
                    providerData.PhoneNumber = x.PhoneNumber;
                    providerData.Uid = x.Uid;
                    providerData.PhotoURL = x.PhotoURL;
                    providerData.ProviderId = x.ProviderId;
                    providerData.DisplayName = x.DisplayName;
                    providerData.SingleSignOnId = singleSignOn.Id;
                    dbContext.Add(providerData);
                    dbContext.SaveChanges();
                });

                singleSignOn.ProviderData = listData;
                dbContext.Update(singleSignOn);
                dbContext.SaveChanges();

                return savedUser;
            }
            return null;
        }
    }
}
