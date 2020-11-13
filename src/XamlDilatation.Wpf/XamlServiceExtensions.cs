using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace XamlDilatation.Wpf
{
    public static class XamlServiceWpfExtensions
    {        
        public static XamlService RegisterWpf(this XamlService service)
        {
            service.RegisterContentProperty<Label>(nameof(Label.Content), (label, value) => value is not null);
            service.RegisterChildrenProperty<Grid>(
                nameof(Grid.Children),
                (object element, object value, out List<object> children) =>
                {
                    var val = (UIElementCollection) value;
                    children = null;
                    if (val.Count == 0) return false;
                    children = val.Cast<UIElement>().Cast<object>().ToList();
                    return true;
                });            
            service.RegisterContentProperty<Window>(nameof(Window.Content), true);
            
            return service;
        }
    }
}