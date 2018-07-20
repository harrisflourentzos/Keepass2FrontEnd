using System.Windows;

namespace Keepass2.Utilities
{
    public static class Extensions
    {
        public static void CopyToClipboard(this string text) => Clipboard.SetText(text);
    }
}
