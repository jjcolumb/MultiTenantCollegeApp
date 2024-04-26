using System.ComponentModel;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;

namespace CollegeApp.Module.BusinessObjects;

[MapInheritance(MapInheritanceType.ParentTable)]
[DefaultProperty(nameof(UserName))] 
public class ApplicationUser : PermissionPolicyUser, ISecurityUserWithLoginInfo, ISecurityUserLockout {
    private int accessFailedCount;
    private DateTime lockoutEnd;
    private Company company;
    private College favoriteCollege;
    public ApplicationUser(Session session) : base(session) { }

    [Browsable(false)]
    public int AccessFailedCount {
        get { return accessFailedCount; }
        set { SetPropertyValue(nameof(AccessFailedCount), ref accessFailedCount, value); }
    }

    [Browsable(false)]
    public DateTime LockoutEnd {
        get { return lockoutEnd; }
        set { SetPropertyValue(nameof(LockoutEnd), ref lockoutEnd, value); }
    }

    [Browsable(false)]
    [Aggregated, Association("User-LoginInfo")]
    public XPCollection<ApplicationUserLoginInfo> LoginInfo {
        get { return GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo)); }
    }

    [Association("Company-Users")]
    public Company Company
    {
        get => company;
        set => SetPropertyValue(nameof(Company), ref company, value);
    }

    [DataSourceCriteria("Tenant.Oid = '@CurrentTenantOid'")]
    public College FavoriteCollege
    {
        get => favoriteCollege;
        set => SetPropertyValue(nameof(FavoriteCollege), ref favoriteCollege, value);
    }

    [Browsable(false)]
    private Guid CurrentTenantOid => this.Company?.Oid ?? Guid.Empty;

    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey) {
        ApplicationUserLoginInfo result = new ApplicationUserLoginInfo(Session);
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }
}
