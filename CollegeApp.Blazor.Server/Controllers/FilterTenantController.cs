﻿using CollegeApp.Blazor.Server.Services;
using CollegeApp.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollegeApp.Blazor.Server.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FilterTenantController : ViewController<ListView>
    {
        ITenantService tenantService;
        public FilterTenantController()
        {
            InitializeComponent();
   
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            tenantService = Application.ServiceProvider?.GetRequiredService<ITenantService>();

            if (tenantService.CurrentTenantOid == null)
                return;

            View.CollectionSource.Criteria["TenantFilter"] = CriteriaOperator.Parse("[Tenant] = ?", tenantService.CurrentTenantOid);
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
