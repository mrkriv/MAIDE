﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace MAIDE.UI
{
    public class CsvConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var v = value as List<string>;
            if (destinationType == typeof(string))
                return string.Join(",", v.ToArray());
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}