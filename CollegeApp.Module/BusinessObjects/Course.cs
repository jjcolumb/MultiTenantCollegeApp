using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CollegeApp.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty(nameof(Name))]
    public class Course : TenantBaseObject
    { 
        public Course(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private string name;
        private Professor professor;
        private College college;

        [RuleRequiredField(DefaultContexts.Save)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [Association("Course-Students")]
        public XPCollection<Student> Students
        {
            get => GetCollection<Student>(nameof(Students));
        }

        [Association("Professor-Courses")]
        public Professor Professor
        {
            get => professor;
            set => SetPropertyValue(nameof(Professor), ref professor, value);
        }

        [Association("College-Courses")]
        public College College
        {
            get => college;
            set => SetPropertyValue(nameof(College), ref college, value);
        }
    }
}