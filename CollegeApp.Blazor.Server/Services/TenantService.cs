namespace CollegeApp.Blazor.Server.Services
{
    public class TenantService : ITenantService
    {
        public Guid? CurrentTenantOid { get; set; }

        public Guid? FavoriteCollegeOid { get; set; }
    }
}
