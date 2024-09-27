using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_model.SystemModels
{
    public static class ModelServices
    {
        public static string GetEnumDisplayName<TEnum>(TEnum enumValue) where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);

            var memberInfo = enumType.GetMember(enumValue.ToString());

            if (memberInfo.Length > 0)
            {
                var displayAttribute = memberInfo[0]
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    .FirstOrDefault() as DisplayAttribute;

                if (displayAttribute != null)
                {
                    return displayAttribute.Name;
                }
            }

            return enumValue.ToString();
        }
    }
}
