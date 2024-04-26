using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CollegeApp.Module.BusinessObjects
{
    [NonPersistent]
    public abstract class TenantBaseObject : BaseObject
    { 
        public TenantBaseObject(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            var CurrentUser = Session.GetObjectByKey<ApplicationUser>(
                                    Session.ServiceProvider.GetRequiredService<ISecurityStrategyBase>().UserId);
            Tenant = CurrentUser.Company;
        }

        private Company tenant;

        [Browsable(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public Company Tenant
        {
            get => tenant;
            set => SetPropertyValue(nameof(Tenant), ref tenant, value);
        }
    }
}