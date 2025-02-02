﻿using DevExpress.Data.Filtering;
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
    [NonPersistent]
    [DefaultProperty(nameof(Name))]
    public abstract class Person : TenantBaseObject
    { 
        public Person(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private string name;
        private string lastName;
        private DateTime dateOfBirth;

        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name ,value);
        }

        public string LastName
        {
            get => lastName;
            set => SetPropertyValue(nameof(LastName), ref lastName, value);
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set => SetPropertyValue(nameof(DateOfBirth), ref dateOfBirth, value);
        }
    }
}