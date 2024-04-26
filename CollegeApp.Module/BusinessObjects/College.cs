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
    public class College : TenantBaseObject
    { 
        public College(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string name;

        [RuleRequiredField(DefaultContexts.Save)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [Association("College-Professors")]
        public XPCollection<Professor> Professors
        {
            get => GetCollection<Professor>(nameof(Professors));
        }
        [Association("College-Courses")]
        public XPCollection<Course> Courses
        {
            get => GetCollection<Course>(nameof(Courses));
        }

        [Association("College-Students")]
        public XPCollection<Student> Students
        {
            get => GetCollection<Student>(nameof(Students));
        }
    }
}