using CollegeApp.Blazor.Server.Components;
using CollegeApp.Blazor.Server.Dtos;
using CollegeApp.Blazor.Server.Services;
using CollegeApp.Module.BusinessObjects;
using DevExpress.Blazor;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.BaseImpl;

namespace CollegeApp.Blazor.Server.Controllers
{
    
    public partial class CollegeSelectorController : ViewController
    {
        SingleChoiceAction selectCollege;
        ITenantService tenantService;
        ICollegeSelectorService collegeSelectorService;
        public CollegeSelectorController()  
        {
            InitializeComponent();
            selectCollege = new SingleChoiceAction(this, id: "Select college", category: "QuickAccess");
            selectCollege.Caption = "Select college.";
            selectCollege.CustomizeControl += SelectCollege_CustomizeControl;
            
        }
        private void SelectCollege_CustomizeControl(object sender, CustomizeControlEventArgs e)
        {
            if(e.Control is DxToolbarComboBoxItemSingleChoiceActionControl control)
            {
                collegeSelectorService.ToolBarInfo.ItemClick += ToolBarInfo_ItemClick;
                collegeSelectorService.ToolBarInfo.Items = GetItems(selectCollege.Items);

                if (tenantService.FavoriteCollegeOid != null)
                {
                    var item = collegeSelectorService.ToolBarInfo.Items.FirstOrDefault(i => i.Item2.Data.ToString() == tenantService.FavoriteCollegeOid.ToString());
                    int index = collegeSelectorService.ToolBarInfo.Items.IndexOf(item);
                    item.Item1 = true;
                    collegeSelectorService.ToolBarInfo.Items[index] = item;
                    collegeSelectorService.ToolBarInfo.SelectedItems.Add(item.Item2);
                    collegeSelectorService.SelectedCollegeIds.Add(item.Item2.Data);
                }

                control.ToolbarItemModel.Template = (info) => CollegeSelectorComponent.RenderTemplate(collegeSelectorService.ToolBarInfo);
            }
        }

        private List<(bool, ChoiceActionItem)> GetItems(ChoiceActionItemCollection items)
        {
            List<(bool, ChoiceActionItem)> list = new List<(bool, ChoiceActionItem)> ();
            foreach(ChoiceActionItem item in items) 
            {
                list.Add((false, item));
            }
            return list;
        }
        private void ToolBarInfo_ItemClick(object sender, EventArgs e)
        {
            ToolBarInfo toolBarInfo = (ToolBarInfo)sender;
            collegeSelectorService.SelectedCollegeIds = toolBarInfo.SelectedItems.Select(item => item.Data).ToList();

            if (View is ListView)
            {
                ListView listView = (ListView)View;
                listView.CollectionSource.Criteria["CollegeFilter"] = new InOperator("College.Oid", collegeSelectorService.SelectedCollegeIds);
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            collegeSelectorService = Application.ServiceProvider.GetRequiredService<ICollegeSelectorService>();
            collegeSelectorService.ExecuteControllerActivated();
            tenantService = Application.ServiceProvider.GetRequiredService<ITenantService>();


            if (View == null || View.ObjectSpace == null || View.ObjectTypeInfo == null)
                return;

            IObjectSpace objectSpace = View.ObjectSpace;

            CriteriaOperator criteria = CriteriaOperator.Parse("[Tenant] = ?", tenantService.CurrentTenantOid);
            var Colleges = objectSpace.GetObjects<College>(criteria);

            selectCollege.Items.Clear();
            foreach (College college in Colleges)
            {
                selectCollege.Items.Add(new ChoiceActionItem(college.Name, college.Oid));
            }

            ITypeInfo typesInfo = View.ObjectTypeInfo;

            IMemberInfo collegeMember = typesInfo.Members.FirstOrDefault(m => m.Name == "College");

            if (collegeMember == null)
            {
                selectCollege.Active.SetItemValue("SelectCollegeDisabled", false);
                return;
            }
            else
                selectCollege.Active.SetItemValue("SelectCollegeDisabled", true);

            if(View is ListView listView)
            {
                if (tenantService.CurrentTenantOid == null)
                    return;

                if (collegeSelectorService.SelectedCollegeIds.Count > 0)
                    listView.CollectionSource.Criteria["CollegeFilter"] = new InOperator("College.Oid", collegeSelectorService.SelectedCollegeIds);
            }           
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
