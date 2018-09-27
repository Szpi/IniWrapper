# IniWrapper
[![Latest version](https://img.shields.io/nuget/v/IniWrapper.svg)](https://www.nuget.org/packages/IniWrapper/) [![codecov](https://codecov.io/gh/Szpi/IniWrapper/branch/master/graph/badge.svg)](https://codecov.io/gh/Szpi/IniWrapper) ![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)

## Build Status
&nbsp; | `Azure Pipelines` | `Travis`
--- | --- | --- 
**master** | ![Azure Pipelines](https://iniwrapper.visualstudio.com/_apis/public/build/definitions/9232e33a-db8d-4617-a1b1-8cf3ce4c88f5/3/badge)  | ![Travis](https://travis-ci.org/Szpi/IniWrapper.svg?branch=master)

IniWrapper uses reflection to bind value read from ini file to provided model. The purpose of this library is NOT parsing ini file, but to wrap it to provide easier use of existing parsing libraries. In configuration there is possibility to pass IniParser interface, which is used as file access layer. This library provides class that wraps Windows C++ methods to retrieve values from ini file.

***For more information please go to [wiki page](https://github.com/Szpi/IniWrapper/wiki).***

## Quick start
### Loading configuration

You can use custom IniParser class by passing it to Create Method in IniWrapperFactory class. Then call LoadConfiguration method with class that IniWrapper should discover and bind values.
``` csharp
var iniWrapperFactory = new IniWrapperFactory();
var iniWrapper = iniWrapperFactory.Create(new CustomIniParser());

var loadedIniConfiguration = iniWrapper.LoadConfiguration<TestConfiguration>();
```
If you want to use default IniParser you can call CreateWithDefaultIniParser method. By doing this library will create IniParser that wraps Windows C++ methods from kernel. For more information see Microsoft documentation for WritePrivateProfileString, GetPrivateProfileString and GetPrivateProfileSection and [IniParser.cs](https://github.com/Szpi/IniWrapper/blob/master/IniWrapper/IniWrapper/ParserWrapper/IniParser.cs).
For CreateWithDefaultIniParser property IniFilePath in IniSettings has to be set.
``` csharp
var iniWrapperFactory = new IniWrapperFactory();
var inisettings = new IniSettings()
{	
	IniFilePath = "test.ini"
};
var iniWrapper = iniWrapperFactory.CreateWithDefaultIniParser(inisettings);

var loadedIniConfiguration = iniWrapper.LoadConfiguration<TestConfiguration>();
```

Configure library's [Settings](https://github.com/Szpi/IniWrapper/wiki/Settings) to change it's default behaviour.
```csharp
var iniWrapper = new IniWrapperFactory().Create(iniSettings =>
{
  iniSettings.MissingFileWhenLoadingHandling = MissingFileWhenLoadingHandling.ForceLoad;
  iniSettings.EnumerableEntitySeparator = '*';
  iniSettings.IniFilePath = "test.ini";
  iniSettings.NullValueHandling = NullValueHandling.ReplaceWithEmptyString;
  iniSettings.DefaultIniWrapperBufferSize = 1024;
}, iniParser);
```
**Note:**
In version 1.1.0 and 1.0.0 you have to call IniWrapperFactory with CreateWithDefaultIniWrapper.

``` csharp
var iniWrapper = iniWrapperFactory.CreateWithDefaultIniWrapper("test.ini");
```
### Saving configuration
To save configuration just call Save method and pass configuration class.
``` csharp
var iniWrapperFactory = new IniWrapperFactory();
var inisettings = new IniSettings()
{	
	IniFilePath = "test.ini"
};
var iniWrapper = iniWrapperFactory.CreateWithDefaultIniParser(inisettings);

iniWrapper.SaveConfiguration(new TestConfiguration());
```
**Note:**
In version 1.1.0 and 1.0.0 you have to call IniWrapperFactory with CreateWithDefaultIniWrapper.

``` csharp
var iniWrapper = iniWrapperFactory.CreateWithDefaultIniWrapper("test.ini");
```
## How does it work?
For given configuration class:
``` csharp
public struct TestConfiguration
{
    public string TestString { get; set; }
    public List<int> TestIntList { get; set; }
}
```
IniWrapper will call IIniParser with following ini parameters Section:TestConfiguration, Key: TestString, Value : value in TestString
property.

Overall rules:
- Section is evaluated from class / struct name
- Key is evaluated from property / field name
- Value is taken from property / field value

**Exception is for IDictionary type:**
- Section is evaluated from property / field name
- Key is taken from Key (from IDictionary)
- Value is taken from Value (from IDictionary)

To override library's default name resolving you can use [IniOptionsAttribute](https://github.com/Szpi/IniWrapper/wiki/Attributes).