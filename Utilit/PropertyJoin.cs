using System;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

namespace MAIDE.Utilit
{
    public class PropertyJoin
    {
        public delegate void ChangedProperty<T>(T value);
        public delegate void Clallback();

        private object objIn, objTo;
        private PropertyDescriptor propTo, propIn;
        private PropertyInfo propToInfo;

        public PropertyJoin(object objTo, string propertyTo, object objIn, string propertyIn)
        {
            this.objIn = objIn;
            this.objTo = objTo;

            propTo = TypeDescriptor.GetProperties(objTo).Find(propertyTo, true);
            propIn = TypeDescriptor.GetProperties(objIn).Find(propertyIn, true);

            propIn.AddValueChanged(objIn, PropertyChangedDynamic);
            PropertyChangedDynamic(this, EventArgs.Empty);
        }

        public PropertyJoin(Type type, string propertyTo, object objIn, string propertyIn)
        {
            this.objIn = objIn;

            propToInfo = type.GetProperty(propertyTo);
            propIn = TypeDescriptor.GetProperties(objIn).Find(propertyIn, true);

            propIn.AddValueChanged(objIn, PropertyChangedStatic);
            PropertyChangedStatic(this, EventArgs.Empty);
        }

        public void Break()
        {
            if (propToInfo != null)
                propIn.RemoveValueChanged(objIn, PropertyChangedStatic);
            else
                propIn.RemoveValueChanged(objIn, PropertyChangedDynamic);
        }

        private void PropertyChangedStatic(object sender, EventArgs e)
        {
            propToInfo.SetValue(null, propIn.GetValue(objIn));
        }

        private void PropertyChangedDynamic(object sender, EventArgs e)
        {
            object value = propIn.GetValue(objIn);

            if (propIn.Converter.CanConvertTo(propTo.PropertyType))
                propTo.SetValue(objTo, propIn.Converter.ConvertTo(value, propTo.PropertyType));
            else if (propTo.Converter.CanConvertFrom(propIn.PropertyType))
                propTo.SetValue(objTo, propTo.Converter.ConvertFrom(value));
            else
                propTo.SetValue(objTo, value);
        }

        public static PropertyJoin Create(object objTo, string propertyTo, object objIn, string propertyIn)
        {
            return new PropertyJoin(objTo, propertyTo, objIn, propertyIn);
        }

        public static PropertyJoin Create(Type type, string propertyTo, object objIn, string propertyIn)
        {
            return new PropertyJoin(type, propertyTo, objIn, propertyIn);
        }

        public static void ChangedPropertyEvent<T>(object obj, string property, ChangedProperty<T> callback) where T : class
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(obj).Find(property, true);
            prop.AddValueChanged(obj, (s, e) => { callback(prop.GetValue(obj) as T); });
        }

        public static void ChangedPropertyEvent(object obj, string property, Clallback callback)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(obj).Find(property, true);
            prop.AddValueChanged(obj, (s, e) => { callback(); });
        }

        public static void ChangedPropertyEvent(object obj, string[] propertes, Clallback callback)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
            {
                if (propertes.Contains(prop.Name))
                    prop.AddValueChanged(obj, (s, e) => { callback(); });
            }
        }
    }
}