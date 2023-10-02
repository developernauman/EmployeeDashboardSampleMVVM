using MVPVM.ToolKit.Behaviors;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MVPVM
{
    public class DirtyViewModel : BindableBase, IDataErrorInfo, IValidationExceptionHandler
    {
        bool isViewDirty;
        public bool IsViewDirty
        {
            get
            {
                return isViewDirty;
            }
            protected set
            {
                isViewDirty = value;
                OnPropertyChanged();
            }
        }


        private readonly Dictionary<string, ValidationAttribute[]> validators;
        private readonly Dictionary<string, Func<DirtyViewModel, object>> propertyGetters;


        public string Error
        {
            get
            {
                var errors = from validator in this.validators
                             from attribute in validator.Value
                             where !attribute.IsValid(this.propertyGetters[validator.Key](this))
                             select attribute.ErrorMessage;

                return string.Join(Environment.NewLine, errors.ToArray());
            }
        }

        public string this[string propertyName]
        {
            get
            {
                if (this.propertyGetters.ContainsKey(propertyName))
                {
                    var propertyValue = this.propertyGetters[propertyName](this);
                    var errorMessages = this.validators[propertyName]
                        .Where(v => !v.IsValid(propertyValue))
                        .Select(v => v.ErrorMessage).ToArray();

                    return string.Join(Environment.NewLine, errorMessages);
                }

                return string.Empty;
            }
        }


        
        public int ValidPropertiesCount
        {
            get
            {
                var query = from validator in this.validators
                            where validator.Value.All(attribute => attribute.IsValid(this.propertyGetters[validator.Key](this)))
                            select validator;

                var count = query.Count() - this.validationExceptionCount;
                return count;
            }
        }

        public int TotalPropertiesWithValidationCount
        {
            get
            {
                return this.validators.Count();
            }
        }



        private int validationExceptionCount;

        public void ValidationExceptionsChanged(int count)
        {
            this.validationExceptionCount = count;
            this.OnPropertyChanged("ValidPropertiesCount");
        }


        public DirtyViewModel()
        {
            this.validators = this.GetType()
                .GetProperties()
                .Where(p => this.GetValidations(p).Length != 0)
                .ToDictionary(p => p.Name, p => this.GetValidations(p));

            this.propertyGetters = this.GetType()
                .GetProperties()
                .Where(p => this.GetValidations(p).Length != 0)
                .ToDictionary(p => p.Name, p => this.GetValueGetter(p));
        }


        private ValidationAttribute[] GetValidations(PropertyInfo property)
        {
            return (ValidationAttribute[])property.GetCustomAttributes(typeof(ValidationAttribute), true);
        }

        private Func<DirtyViewModel, object> GetValueGetter(PropertyInfo property)
        {
            return new Func<DirtyViewModel, object>(viewmodel => property.GetValue(viewmodel, null));
        }


        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                Debug.Fail("Invalid property name: " + propertyName);
            }
        }

        protected void PropertyChangedCompleted(string propertyName)
        {
            if (propertyName != "IsViewDirty")
            {
                if (string.IsNullOrEmpty(this.Error) && this.ValidPropertiesCount == this.TotalPropertiesWithValidationCount)
                {
                    this.IsViewDirty = true;
                }
                else
                {
                    this.IsViewDirty = false;
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            VerifyPropertyName(name);
            RaisePropertyChanged(name);
            this.PropertyChangedCompleted(name);
        }
    }
}
