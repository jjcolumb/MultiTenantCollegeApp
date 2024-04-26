using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using CollegeApp.Blazor.Server.Services;
using CollegeApp.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using System.Security.AccessControl;
using CollegeApp.Blazor.Server.Dtos;
using DevExpress.ExpressApp.Templates;
using static System.Net.Mime.MediaTypeNames;

namespace CollegeApp.Blazor.Server;

public class CollegeAppBlazorApplication : BlazorApplication {
    public CollegeAppBlazorApplication() {
        ApplicationName = "CollegeApp";
        CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
        DatabaseVersionMismatch += CollegeAppBlazorApplication_DatabaseVersionMismatch;
    }
    protected override void OnSetupStarted() {
        base.OnSetupStarted();
#if DEBUG
        if(System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
            DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
        }
#endif
    }

    protected override void OnLoggedOn(LogonEventArgs args)
    {
        base.OnLoggedOn(args);
        using (IObjectSpace objectSpace = ObjectSpaceProvider.CreateObjectSpace())
        {
            var CurrentUser = objectSpace.GetObjectByKey<ApplicationUser>(ServiceProvider.GetRequiredService<ISecurityStrategyBase>().UserId);
            if(CurrentUser.Company != null)
            {
                ITenantService TenantService = ServiceProvider.GetRequiredService<ITenantService>();
                TenantService.CurrentTenantOid = CurrentUser.Company.Oid;

                if (CurrentUser.FavoriteCollege != null)
                {
                    TenantService.FavoriteCollegeOid = CurrentUser.FavoriteCollege.Oid;
                    //IModelApplicationNavigationItems NavigationItems = Model as IModelApplicationNavigationItems;
                    //IModelNavigationItem TargetNavigatioItem = NavigationItems.NavigationItems
                    //                                                             .AllItems
                    //                                                            .FirstOrDefault(n => n.Id == "FavoriteCollege_DetailView");

                    //TargetNavigatioItem.ObjectKey = CurrentUser.FavoriteCollege.Oid.ToString();
                    //NavigationItems.NavigationItems.StartupNavigationItem = TargetNavigatioItem;
                }
            }
           
        }
            
    }
    private void CollegeAppBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
        e.Updater.Update();
        e.Handled = true;
#else
        if(System.Diagnostics.Debugger.IsAttached) {
            e.Updater.Update();
            e.Handled = true;
        }
        else {
            string message = "The application cannot connect to the specified database, " +
                "because the database doesn't exist, its version is older " +
                "than that of the application or its schema does not match " +
                "the ORM data model structure. To avoid this error, use one " +
                "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

            if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
            }
            throw new InvalidOperationException(message);
        }
#endif
    }
}
