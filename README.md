# BOLCSharpHelper
Bugsonline C# project helper


Instructions for use Bugsonline:

1) Register [here](https://www.bugsonline.biz/#anchor-register "here") by choosing a plan.
2) In your C# project install nuget package from CLI: `dotnet add package Bugsonline`

  or by Package Manager: `NuGet\Install-Package Bugsonline`
  
3) In your code, initialize an instance:
```
BOLHelper bol = BOLHelper.initInstance(
                "your@email.com",
                "yourpassword",
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                AppType.Console,
                Languages.CSharp);
```
Fifth parameter is an enum, you can choose your project type

Sixth parameter is another enum in which you can specify your programming language

4) Now you can send your bugs, for example, in a catch:
```
int c = 0;
try
{
    // Oh no, division by zero!
    int g = 5 / c;
}
catch(Exception ex)
{
    await bol.Send(ex); // but i send this bug!
}
```

5) Well, login [here](https://www.bugsonline.biz "here") and you will see your bug in a dashboard!
