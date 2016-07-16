using System.Windows.Input;

namespace Sappi
{
    class ContextCommands
    {
        public static readonly RoutedUICommand Edit = new RoutedUICommand
            (
            "Edit",
            "Edit",
            typeof(ContextCommands)
            );
        public static readonly RoutedUICommand EditCell = new RoutedUICommand
            (
            "EditCell",
            "EditCell",
            typeof(ContextCommands)
            );
        public static readonly RoutedUICommand Delete = new RoutedUICommand
            (
            "Delete",
            "Delete",
            typeof(ContextCommands)
            );
        public static readonly RoutedUICommand Print = new RoutedUICommand
            (
            "Print",
            "Print",
            typeof(ContextCommands)
            );
    }
}
