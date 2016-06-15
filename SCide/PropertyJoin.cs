using System;
using System.ComponentModel;

namespace ASM
{
    public class PropertyJoin
    {
        private object obj1, obj2;
        private PropertyDescriptor prop1, prop2;

        /// <summary>
        /// object1.propertyName1 = object2.propertyName2
        /// </summary>
        public PropertyJoin(object object1, string propertyName1, object object2, string propertyName2, bool doubleJoin = false)
        {
            obj1 = object1;
            obj2 = object2;

            prop1 = TypeDescriptor.GetProperties(object1).Find(propertyName1, true);
            prop2 = TypeDescriptor.GetProperties(object2).Find(propertyName2, true);

            if (doubleJoin)
                prop1.AddValueChanged(object1, Object1PropertyChanged);
            prop2.AddValueChanged(object2, Object2PropertyChanged);

            Object2PropertyChanged(this, EventArgs.Empty);
        }

        private void Object1PropertyChanged(object sender, EventArgs e)
        {
            object value = prop1.GetValue(obj1);

            if (prop1.Converter.CanConvertTo(prop2.PropertyType))
                prop2.SetValue(obj2, prop1.Converter.ConvertTo(value, prop2.PropertyType));
            else if (prop2.Converter.CanConvertFrom(prop1.PropertyType))
                prop2.SetValue(obj2, prop2.Converter.ConvertFrom(value));
            else
                prop2.SetValue(obj2, value);
        }

        private void Object2PropertyChanged(object sender, EventArgs e)
        {
            object value = prop2.GetValue(obj2);

            if (prop2.Converter.CanConvertTo(prop1.PropertyType))
                prop1.SetValue(obj1, prop2.Converter.ConvertTo(value, prop1.PropertyType));
            else if (prop1.Converter.CanConvertFrom(prop2.PropertyType))
                prop1.SetValue(obj1, prop1.Converter.ConvertFrom(value));
            else
                prop1.SetValue(obj1, value);
        }
    }
}