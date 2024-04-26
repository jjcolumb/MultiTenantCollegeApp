using DevExpress.Data.Filtering;
using DevExpress.DataAccess.DataFederation;
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
    public class Student : Person
    { 
        public Student(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        int average;

        public int Average
        {
            get => average; 
            set => SetPropertyValue(nameof(Average), ref average, value);
        }

        private Course course;

        [Association("Course-Students")]
        public Course Course
        {
            get => course;
            set 
            {
                SetPropertyValue(nameof(Course), ref course, value);
                College = Course.College;
            }
        }

        private College college;

        [Association("College-Students")]
        [VisibleInListView(true)]
        [VisibleInDetailView(false)]
        [ModelDefault("AllowEdit", "False")]
        public College College
        {
            get => college;
            set => SetPropertyValue(nameof(College), ref college, value);
        }

    }
}