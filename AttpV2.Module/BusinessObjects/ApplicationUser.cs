using System.ComponentModel;
using System.Text;
using DevExpress.ExpressApp;

using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;

namespace AttpV2.Module.BusinessObjects;

[MapInheritance(MapInheritanceType.ParentTable)]
[DefaultProperty(nameof(UserName))]
public class ApplicationUser : PermissionPolicyUser, IObjectSpaceLink, ISecurityUserWithLoginInfo {
    public ApplicationUser(Session session) : base(session) { }

    [Browsable(false)]
    [Aggregated, Association("User-LoginInfo")]
    public XPCollection<ApplicationUserLoginInfo> LoginInfo {
        get { return GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo)); }
    }
    IObjectSpace IObjectSpaceLink.ObjectSpace { get; set; }

    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey) {
        ApplicationUserLoginInfo result = new ApplicationUserLoginInfo(Session);
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }

    CoQuanQuanLy coquanQuanly;
    [DevExpress.ExpressApp.DC.XafDisplayName("Cơ quan quản lý")]
    [Association("CoQuanQuanLy-ApplicationUsers")]
    public CoQuanQuanLy CoquanQuanly
    {
        get => coquanQuanly;
        set => SetPropertyValue(nameof(CoquanQuanly), ref coquanQuanly, value);
    }
}
