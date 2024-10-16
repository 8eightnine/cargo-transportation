using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace cargo_transportation.Classes
{
    public class LibInvoke
    {
        public static int InvokeFunction(string dllName, string functionName, Form form)
        {
            try
            {
                // Assuming the function signature is known and involves no parameters and returns an int
                IntPtr hDll = LoadLibrary(dllName);
                if (hDll == IntPtr.Zero)
                {
                    return 0; // DLL could not be loaded
                }

                IntPtr pFunction = GetProcAddress(hDll, functionName);
                if (pFunction == IntPtr.Zero)
                {
                    return 0; // Function could not be found
                }

                var functionDelegate = Marshal.GetDelegateForFunctionPointer<FunctionDelegate>(pFunction);
                int result = functionDelegate(form.Handle);

                FreeLibrary(hDll);

                return result == 0 ? 1 : 0; // Return 1 if the function ran correctly, else return 0
            }
            catch (Exception)
            {
                return 0; // Return 0 in case of any exceptions
            }
        }

        // Delegate definition for the assumed function signature
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int FunctionDelegate(IntPtr formHandle);

        // P/Invoke declarations
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);
    }
}
