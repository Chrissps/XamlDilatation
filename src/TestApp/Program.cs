using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks.Dataflow;
using XamlDilatation;

var service = new XamlService();
service.RegisterDefault();


var window = new Window {Name = "window"};
var grid0 = new Grid {Name = "grdMain"};
var grid1 = new Grid {Name = "grdControls0"};
var grid2 = new Grid {Name = "grdControls1"};
var ctrl0 = new TextBox {Name = "tb0", Text = "hallo"};
var ctrl1 = new Label {Name = "lb0", Content = new Grid{Name = "innerGrid"}};
window.Content = grid0;
grid0.Children.Add(grid1);
grid0.Children.Add(grid2);
grid1.Children.Add(ctrl0);
grid2.Children.Add(ctrl1);

service.Serialize(window);



public class Element
{
    public string Name { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
}

public class Window : Element
{
    public Element Content { get; set; }
}

public class Grid : Element
{
    public List<Element> Children { get; set; } = new List<Element>();
}

public class TextBox : Element
{
    public string Text { get; set; }
}

public class Label : Element
{
    public object Content { get; set; }
}