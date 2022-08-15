using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.Model
{
    public abstract class Entity : ValidationBase, IDataErrorInfo
    {
        protected string id;
        protected IDictionary<string, bool> errors = new Dictionary<string, bool>();
        protected bool validation = true;
        protected static List<string> exportList = new List<string>();

        public Entity()
        {
            id = string.Empty;
        }

        public Entity(string id)
        {
            this.id = id;
        }

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        public IDictionary<string, bool> Errors
        {
            get
            {
                return errors;
            }
            set
            {
                Errors = value;
                OnPropertyChanged(nameof(Errors));
            }
        }

        public bool Validation
        {
            get { return validation; }
            set
            {
                validation = value;

                if (!validation)
                {
                    ClearErrors();
                }
                else
                {
                    CheckErrors();
                }
            }
        }

        #region Methods

        public void CheckErrors()
        {
            foreach (string key in new List<string>(errors.Keys))
            {
                OnPropertyChanged(key);
            }
        }

        public void ClearErrors()
        {
            foreach (string key in new List<string>(errors.Keys))
            {
                errors[key] = true;
            }

            CheckErrors();
        }

        public T ExportObject<T>()
        {
            T exportObject = (T)Activator.CreateInstance(this.GetType());

            foreach (string propertyName in exportList)
            {
                setPropertyValue(propertyName, exportObject, GetPropertyValue(propertyName, this));
            }

            return exportObject;
        }

        public T GetEntity<T>(Type type)
        {
            T entity = (T)Activator.CreateInstance(type);

            foreach (string propertyName in exportList)
            {
                setPropertyValue(propertyName, entity, GetPropertyValue(propertyName, this));
            }

            return entity;
        }

        public void ImportObject(object importObject)
        {
            if (importObject == null)
            {
                return;
            }

            foreach (string propertyName in exportList)
            {
                setPropertyValue(propertyName, this, GetPropertyValue(propertyName, importObject));
            }
        }

        public bool ShouldExportProperty(string name)
        {
            return exportList.Where(item => exportList.Contains(name)).Any();
        }

        protected void setPropertyValue(string name, object target, object value)
        {
            target.GetType().GetProperty(name).SetValue(target, value);
        }

        protected object GetPropertyValue(string name, object target)
        {
            return target.GetType().GetProperty(name).GetValue(target, null);
        }

        protected object GetPropertyValue(string name)
        {
            return GetPropertyValue(name, this);
        }

        public bool HasErrors()
        {
            foreach (KeyValuePair<string, bool> entry in errors)
            {
                if (entry.Value)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion Methods

        #region IDataErrorInfo

        public bool IsNullOrEmpty(string propertyValue, out string error)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                error = "This field is required";
                return true;
            }

            error = string.Empty;
            return false;
        }

        public bool IsNull(object value, out string error)
        {
            if (value == null)
            {
                error = "This field is required";
                return true;
            }

            error = string.Empty;
            return false;
        }

        public bool IsLenghtValid(string propertyValue, int maxLenght, out string error)
        {
            if (propertyValue.Length > maxLenght)
            {
                error = string.Format("Maksimalna duzina unetog teksta ne smee biti veca od {0} karaktera!", maxLenght);
                return false;
            }

            error = string.Empty;
            return false;
        }

        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        public string Error
        {
            get { return null; }
        }
        #endregion IDataErrorInfo

        #region Abstracts methods

        public abstract string Validate(string columnName);
        public abstract void InitExportList();

        #endregion Abstracts methods
    }
}
