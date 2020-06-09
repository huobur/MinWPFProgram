# Minimum "Hello, World!" Program for WPF on .NET Core

I know that this is just too old of a topic for any language on a platform.
But I'd like to explore it from several different angles since this is not simply a program language anymore. WPF is built on a managed code system with .NET platform on Windows. There are much more involved. For our task, we'll use C# and .NET Core 3.x or .NET 5.0.
As we all know that it's quite simple in a specific language only "Hello, World!" program, which is usually just displaying a line of text "Hello, World!" on a console.
In our case, WPF is a modern Windows UI design paradigm which consists of two categories of components: XAML and Code, where XAML is nothing but a markup language to organize supported types with much better UI presentation, and the code portion is usually called code-behind for actions and state changes to support XAML design and other tasks. For XAML purists, they would like to push as much as possible with their designs to XAML, and people who just move to WPF from WinForms and the like will feel much at home with just code to accomplish the tasks at hands.

## Objectives

Since we are trying to minimize our "Hello, World!" WPF program, we should set our objectives for the guidelines before we dive into our design to see where we are going to land:
1. We should use .NET SDK as much as possible, not try to reinvent things ;
2. A language only "Hello, World!" is to show a text line on a console, but for our design, it's more important to show a design window or visual portion on the screen, no matter how simple it is, which should implicitly present itself to the world with greetings. If we insist to display the text in the window on the screen, which should not be considered with any significance, but a trivial task once a window is displayed.
3. Both XAML and code-behind are working with the types supported by WPF. They should be interchangeable, so we are going to push this to two different directions to come up two versions of our project design:
WPF XAML only "Hello, World!"
WPF code only "Hello, World!"

## Setup

We assume that you have installed .NET Core 3.x or .NET 5.0 on your system, and here we are going to stick with .NET SDK 5.0.100-preview.4.
If you run dotnet:
```
$dotnet - version
5.0.100-preview.4.20258.7
```
and get result to show it's .NET Core 3.x or .NET 5, you are all set and ready to go.

## Designs

We'll create two skeleton projects, one for XAML only and the other for code only:
```
$mkdir c:\projects\wpf
$cd c:\projects\wpf
$dotnet new wpf -n xamlmin
$dotnet new wpf -n codemin
```

1. XAML Only "Hello, World!"
Let's start with XAML only program first, our goal is to get rid of all ".cs" files as well as others to minimize the project components and content of each file.
```
$cd c:\projects\wpf\xamlmin
$dir
368 App.xaml
340 App.xaml.cs
604 AssemblyInfo.cs
493 MainWindow.xaml
659 MainWindow.xaml.cs
<DIR> obj
224 xamlmin.csproj
6 File(s)    2,688 bytes
```
We have 6 files and one directory
"obj" directory is to hold temporary files for the build process, which we can get rid of
"AssemblyInfo.cs" is for ThemeInfo, which is non-essential and we can delete it as well
We now have two XAML files, two C# files and one project file left. We want to get rid of both C# files and simplify the two XAML files, and even try to merge them if possible. We need the project file to build and run our program, so it is necessary to keep it;
Let's take a look at "App.xaml.cs" file:
```
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace xamlmin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
}
```
well, it just contains an empty partial class App to be filled. In the "App.xaml", the App is ready to be instantiated. So, we can get rid of it.
* Where are we now? After the above steps, we have 4 files left in the project folder:
```
$dir
368 App.xaml
493 MainWindow.xaml
659 MainWindow.xaml.cs
224 xamlmin.csproj
4 File(s)    1,744 bytes
```
* With the above files, does it still run? Let's give it a try:
```
$dotnet run
```
Very good! it still runs and you should see a blank WPF window displayed on you screen.
* There is still one C# file "MainWindow.xaml.cs" in our project, can we somehow get rid of it and the program still runs? Let's take a look at it:
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace xamlmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
```
We have a partial class MainWindow derived from Window class and there is only one line of code in the constructor. We know that "MainWindow.xaml" has the "MainWindow" class ready to be instantiated, the question now
is if we either getting rid of the code completely or move the code somewhere in our XAML file. We really cannot get rid of the code as the program needs to be triggered to run. Fortunately, we know that XAML can 
have inline code, though it's limited and not encouraged to do so. But for our purpose, we'll do everything we can to meet our design goal.
Let's inset inline code to "MainWindow.xaml" and delete "MainWindow.xaml.cs" file:
```
<Window x:Class="xamlmin.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:xamlmin"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
		
    </Grid>
	
	<x:Code><![CDATA[
		public MainWindow()
		{
			InitializeComponent();
		}
	]]></x:Code>

</Window>
```
Ok, let's give it a try:
```
$del mainwindow.xaml.cs
$dir
368 App.xaml
595 MainWindow.xaml
224 xamlmin.csproj
3 File(s)    1,187 bytes
$dotnet run
```
Do you see the blank window popping up on your screen? If you do, congratulation!
* Our next question is: "Can the two XAML files be combined?" The answer unfortunately is "No". [1] In the Microsoft article of "XAML overview in WPF", it states:
"A XAML file must have only one root element, in order to be both a well-formed XML file and a valid XAML file."
We need two classes in our program: Window and Application. They will have to live in its own XAML file respectively. 
* Now, we have 3 files for our XAML only projects: App.xaml, MainWindow.xaml and xamlmin.csproj, which are the minimum requirement for us to build and run our program.
* Our final job is to trim each of the 3 files to see how much we can squeeze.
For "xamlmin.csproj":
```
<Project Sdk="Microsoft.NET.Sdk.WindowsDesktop">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>netcoreapp5.0</TargetFramework>
    <UseWPF>true</UseWPF>
  </PropertyGroup>

</Project>
```
There is really not much you can touch. Every line is essential for us to successfully run the program. The only thing you may be able to play with is to replace "netcoreapp5.0" to "net5.0" and it should still work.

For "App.xaml":
```
<Application x:Class="xamlmin.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:xamlmin"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
         
    </Application.Resources>
</Application>
```
It's empty in "Application.Resources" which we can get rid of. we can also remove "xmlns:local" line. And we have the final version of:
```
<Application x:Class="xaml2009.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
</Application>
```

For "MainWindow.xaml", we can remove <Grid></Grid> as it's empty. we can also remove the "Title" line as the program will just run no title. After removing 3 more lines and we have the final version of "MainWindow.xaml" as:
```
<Window x:Class="xaml2009.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        >

  <x:Code><![CDATA[
        public MainWindow()
        {
            InitializeComponent();
        }
  ]]></x:Code>
  
</Window>
```
So, here you have it: 3 files in our XAML only WPF "Hello, World!" program.
* If you insist, we can add "<TextBlock>Hello, World!"</TextBlock>" either above the inline code or below, and run it, we will see:

2. Code Only "Hello, World!"
* Since we are only working on code for WPF, we can delete the XMAL files and "obj" directory.
* With much detailed analysis above, we can also delete "AssemblyInfo.cs" and "MainWindow.xaml.cs":
```
$cd c:\projects\wpf\codemin
$del *.xaml AssemblyInfo.cs MainWindow.xaml.cs
$rmdir /q/s obj
$dir
340 App.xaml.cs
224 codemin.csproj
2 File(s)   564 bytes
```
Let's look at "App.xaml.cs" file:
```
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace codemin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
}
```
We have our App derived from Application class, since we deleted Window class and its initiation, we'll need to introduce program entry point and create a window object.
Let's add the following code into the C# file:
```
public static void Main() {
            var win = new Window();

            win.Show();
        }
```
and trim the "using" list, we then have:
```
using System;
using System.Windows;

namespace codemin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static void Main() {
            var win = new Window();

            win.Show();
        }
    }
}
```
When we run the project:
```
$dotnet run
```
Nothing happens, but also there is no error or warning! Let's build the project:
```
$dotnet build
Microsoft (R) Build Engine version 16.7.0-preview-20220-01+80e487bff for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
  You are using a preview version of .NET. See: https://aka.ms/dotnet-core-preview
  codemin -> C:\projects\codemin\bin\Debug\netcoreapp5.0\codemin.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:00.71
```
Everything builds successfully. What's going on?

It turns out that Microsoft decided to use "single threading" model for WPF in their original design decisions:[2]
 "In the end, WPF's threading model was kept in sync with the existing User32 threading model of single threaded execution with thread affinity."
 So we'll need to set the attribute [STAThread] on the Main() method.
* Now we can run the program. However, you need to be very carefully watching your screen to see a blank window flashes and disappears. 
* In order for the program to run continuously, we need to add couple of more lines of code for the App to connect with the window and run in a loop. 
* we can also add a line of code to display "Hello, World!":
```
using System;

using System.Windows;

namespace codemin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main() {
            var app = new App();
            var win = new Window();

            win.Show();
            app.MainWindow = win;
            app.Run();

        }
    }
}
```
* If you really want to see the greeting: "Hello, World!" in the window, please just add the following three lines before "win.Show()":
```
            var tb = new TextBlock();
            tb.Text="Hello, World!";
            win.Content = tb;
```
and 
```
using System.Windows.Controls;
```
on the top of the program.

There are many other ways to display it as well.

That's all for this project. 

We are not propose to design extreme way or the other, we simply want to understand WPF better through this exercise. In real world, the best way is to use both XAML and code to design optimal products.

The final project code can be downloaded at [3]

# Reference

[1] https://docs.microsoft.com/en-us/dotnet/desktop-wpf/fundamentals/xaml

[2] https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/wpf-architecture 

[3] https://github.com/huobur/MinWPFProgram