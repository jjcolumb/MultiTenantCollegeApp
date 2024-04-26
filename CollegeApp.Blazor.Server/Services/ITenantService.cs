namespace CollegeApp.Blazor.Server.Services
{
    public interface ITenantService
    {
        Guid? CurrentTenantOid { get; set; }
        Guid? FavoriteCollegeOid { get; set; }
    }
}
