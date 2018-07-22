using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;

namespace Keepass2.Utilities
{
    public static class Extensions
    {
        public static void CopyToClipboard(this string text) => Clipboard.SetText(text);

        public static string SecureStringToString(this SecureString value)
        {
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public static SecureString StringToSecureString(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return null;

            var result = new SecureString();
            foreach (var c in source) result.AppendChar(c);
            return result;
        }
    }
}
