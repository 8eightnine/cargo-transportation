using System;
using System.Reflection;
using System.Windows.Forms;

namespace cargo_transportation.Classes
{
    public class LibInvoke
    {
        public static int InvokeFunction(string dllName, string functionName, Form prnt)
        {
            if (dllName != "NULL")
            {
                try
                {
                    Assembly asm = Assembly.LoadFrom(dllName + ".dll");
                    Type t = asm.GetType(dllName + "." + dllName);
                    if (t != null)
                    {
                        MethodInfo square = t.GetMethod(functionName);
                        object result = square?.Invoke(null, new object[] { prnt });
                    }
                    else
                    {
                        throw new Exception("Отсутствует класс");
                    }
                }
                catch (Exception ex)
                {
                    ErrorHandler.DllLoadingError(ex.Message, dllName, functionName);
                }
            }

            return 0;
        }
    }
}
