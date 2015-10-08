using FluentValidation.Results;
using SGAM.Elfec.Model.Validations;
using System;
using System.ComponentModel;
using System.Linq;

namespace SGAM.Elfec.Model
{
    public abstract class BaseEntity : INotifyPropertyChanged, IDataErrorInfo
    {
        public void RaisePropertyChanged(params string[] propertyName)
        {
            propertyName.ToList().ForEach(x =>
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(x));
            });
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract ValidationResult SelfValidate();

        public bool IsValid { get { return SelfValidate().IsValid; } }

        public string Error
        {
            get { return ValidationHelper.GetError(SelfValidate()); }
        }

        public string this[string columnName]
        {
            get
            {
                var __ValidationResults = SelfValidate();
                if (__ValidationResults == null) return string.Empty;
                var __ColumnResults = __ValidationResults.Errors.FirstOrDefault<ValidationFailure>(x => string.Compare(x.PropertyName, columnName, true) == 0);
                return __ColumnResults != null ? __ColumnResults.ErrorMessage : string.Empty;
            }
        }
    }
}
