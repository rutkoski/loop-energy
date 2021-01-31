using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TypeUtils
{

    public static object ConvertValue(object Value, Type newType)
    {
        try
        {
            if (newType == typeof(byte[])) return new byte[0];

            if (Value == null) return Activator.CreateInstance(newType);

            if (Value.GetType() == newType) return Value;

            if (Nullable.GetUnderlyingType(newType) != null)
            {
                if (Value.GetType() == newType)
                {
                    return Value;
                }
                return Convert.ChangeType(Value, Nullable.GetUnderlyingType(newType));
            }

            if (newType.ToString().Contains("List`1"))
            {
                Type tmp = newType.GetGenericArguments()[0];
                Type list = typeof(List<>).MakeGenericType(tmp);
                IList tmpList = Activator.CreateInstance(list) as IList;

                if (Value != null)
                {
                    var tmpArr = (Value as string)
                        .Split(',')
                        .Select(itm => ConvertValue(itm, tmp))
                        .ToArray();

                    for (int x = 0; x < tmpArr.Length; x++)
                    {
                        tmpList.Add(tmpArr[x]);
                    }
                }

                return tmpList;
            }

            return Convert.ChangeType(Value, newType);
        }
        catch
        {
        }

        return Activator.CreateInstance(newType);
    }

    public static T ConvertValue<T>(object Value)
    {
        try
        {
            if (Value == null)
            {
                return default(T);
            }

            if (Nullable.GetUnderlyingType(typeof(T)) != null)
            {
                return (T)Convert.ChangeType(Value, Nullable.GetUnderlyingType(typeof(T)));
            }

            return (T)Convert.ChangeType(Value, typeof(T));
        }
        catch
        {
        }

        return default(T);
    }
}
