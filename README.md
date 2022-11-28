# Table of Contents
Coming soon.
## Introduction
---
This is a collection of custom tools I regularly use. They are seperated into 6 different classes for ease of use.
| Class Name | Description |
| -----------|-------------|
| `NullTests` | More expansive checks for different types of `NULL` and `default`.|
| `Geography` | A multitude of county, state and country ISO abbreviations.|
| `URNFormatting` | Database object URN string parsing and creation.|
| `RangeTuple` | Struct for handling more complex range calculations in strings, lists, etc.|
| `Definitions` | Various dictionaries to aid in standardizing character substitutions, etc.|
| `SQLDefinitions` | Dictionaries and Lists of ODBC and T-SQL specific reserved words and operators.|
| `MSAccessDefinitions` | Dictionaries and Lists of Microsoft Access Database specific reserved words|
| `MiscExtensions` | Misc tools, such as SSN RegEx searches, random color generator, etc.|

## Installation
---
You can include these into your project via the following methods:
- ~~NuGet Package (supplied)~~ (Coming soon)
- ~~Windows DLL (supplied)~~ (Coming soon)
- Add the files to your project folder.

## **NullTests**
---
The `isNull()` extension is available for testing the following types:
- `string`
- `int`
- `DateTime`
- `object`

### Examples:
---
>Standard `Boolean` check for `null` or `default` value on a `DateTime`:
```c#
using MultiToolExtensions;

DateTime date1;
if (date1.isNull()) {
    Console.WriteLine("date1 is null or default")
} else {
    Console.WriteLine("date1 is NOT null or default")
}
// Prints:
// val1 is null or default
```
>Assign the value of `val1` to `val2` if `val1` is **NOT** `null` or `default`, or assign the passed value to `val2` if `val1` **IS** `null` or `default`:
```c#
using MultiToolExtensions;

string val1 = "Some value";
string val2 = val1.isNull("val1 is null or default");
Console.WriteLine(val2);
// Prints:
// Some value
```
>Assign the value of `val1` to `val2` if `val1` is **NOT** `null` or `default`, or assign the passed value to `val2` if `val1` **IS** `null` or `default`:
```c#
using MultiToolExtensions;

int val1;
int val2 = val1.isNull(1);
Console.WriteLine("Value is: " +  val2);
// Prints:
// Value is: 1
```
>Assign the first passed value if ` val1` **IS** `null` or `default`, or assign the second passed value to `val2` if `val1` is **NOT** `null` or `default`:
```c#
using MultiToolExtensions;

string val1 = "Some value";
string val2 = val1.isNull("val1 is null or default", "val1 is NOT null or default");
Console.WriteLine(val2);
// Prints:
// val1 is NOT null or default
```
## **Geography**
---
Provides functionality for easily abbreviating a handful of County, State, and Country level geographic locations. The following functions and locations are included:
| Location | Function | ISO|
| -----------|-------------|-------|
| US States | `abbrevUSState()`| [2] Character
| Canadian Provinces | `abbrevCanadianProvince()`| [2] Character
| UK Counties | `abbrevUKCounty()`| [3] Character
| UK Countries | `abbrevUKCountry()`| [3] Character
| Mexican States | `abbrevMexicanState()`| [2] Character

Additionally, there are ISO2 and ISO3 abbrevations avaliable for all countries:
| ISO| Function
|-------------|-------|
| [2] Character| `abbrevCountryISO2()`
| [3] Character | `abbrevCountryISO3()`

 To ensure the abbreviation is accurate, there is also the ability to abbreviate a U.S., Great Britain, Canadian, or Mexican State by supplying it's (`string`) Country name or it's (`string`) ISO2 Country abbreviation:

| ISO| Function
|-------------|-------------------------
| [2] Character| `abbrevStateByCountry([Country ISO2 or Full Country Name])`

Lastly, there are checks for validating that a State abbreviation is correct. Currently, only U.S. States are available for validation testing:

| ISO| Function|Type 
|-----|--------|-------------------------
| [2] Character| `isValidUSState()`|`Boolean`

### Code Examples:
---
>Abbreviating a **State**, **Commonwealth State**, **Province**, or **County** (`abbrevCanadianProvince()`, `abbrevUKCounty()`, `abbrevUKCountry()` and `abbrevMexicanState()`):
```c#
using MultiToolExtensions;

string state_name = "Arkansas";
string state_abbrev = state_name.abbrevUSState();
Console.WriteLine("ISO2 Abbreviation is: " + state_abbrev);
// Prints:
// ISO2 Abbreviation is: AR
```
>ISO2 Abbreviating a **Country**:
```c#
using MultiToolExtensions;

string country_name = "Jamaica";
string country_abbrev = country_name.abbrevCountryISO2();
Console.WriteLine("ISO2 Country Abbreviation is: " + country_abbrev);
// Prints:
// ISO2 Country Abbreviation is: JM
```
>ISO3 Abbreviating a **Country**:
```c#
using MultiToolExtensions;

string country_name = "Jamaica";
string country_abbrev = country_name.abbrevCountryISO3();
Console.WriteLine("ISO3 Country Abbreviation is: " + country_abbrev);
// Prints:
// ISO3 Country Abbreviation is: JAM
```
>Abbreviating a **State** by supplying it's **Country**:
```c#
using MultiToolExtensions;

string state_name = "Tlaxcala";
string country_name = "Mexico";
string state_abbrev = state_name.abbrevStateByCountry(country_name);
Console.WriteLine("ISO2 Abbreviation is: " + state_abbrev);
// Prints:
// ISO2 Abbreviation is: TL
```
>Validating an ISO2 **US State** abbreviation:
```c#
using MultiToolExtensions;

string state_abbrev = "AK";
if (state_abbrev.isValidUSState()) {
    Console.WriteLine("ISO2 abbreviation is correct");
} else {
    Console.WriteLine("ISO2 abbreviation is NOT correct");
}
// Prints:
// ISO2 abbreviation is correct
```
## **URNFormatting**
---
In order to more efficiently parse out or create formatted database URN addresses programmatically, the following methods are included:

| Method  |Description|Input String Example| Return Type
|---------|-----------|-----------|----------
| `URNToList()`| Creates a list from a valid **URN**| `fooSchema.barTable.someColumn`|`List<string>`| 
| `ToFullURN()`| Format valid **URN**| `fooSchema.[barTable].someColumn`|`string`
| `ToFullURN(string)`| Format valid **URN**| `[barTable].[someColumn]`, (`fooSchema`)|`string`
| `ToFullURN(string, string)`| Format valid **URN**| `someColumn`, (`[barTable]`, `fooSchema`)|`string`

In the cases above, `URNToList()` would return a list value of `["fooSchema", "barTable", "someColumn"]`, while all three `ToFullURN()` methods would return a `string` value of `[fooSchema].[barTable].[someColumn]`.

### **Code Examples:**
---
>Properly format a URN string containing a schema, table, and column name. Brackets in the input string are ignored and optional:
```c#
using MultiToolExtensions;

string urn = "fooSchema.barTable.someColumn";
string formatted_urn = urn.ToFullURN();
Console.WriteLine("The formatted URN is: " + formatted_urn);
// Prints:
// The formatted URN is: [fooSchema].[barTable].[someColumn]
```
>Properly format a URN string containing a table, and column name, while passing the schema name as a parameter. Brackets in any of the input strings are ignored and optional:
```c#
using MultiToolExtensions;

string urn = "barTable.[someColumn]";
string schema_name = "fooSchema";
urn.ToFullURN(schema_name);
Console.WriteLine("The formatted URN is: " + urn);
// Prints:
// The formatted URN is: [fooSchema].[barTable].[someColumn]
```
>Properly format a URN string containing a column name, while passing the table name and the schema name as parameters. Brackets in any of the input strings are ignored and optional:
```c#
using MultiToolExtensions;

string urn = "someColumn";
string table_name = "[barTable]";
string schema_name = "fooSchema";
urn.ToFullURN(table_name, schema_name);
Console.WriteLine("The formatted URN is: " + urn);
// Prints:
// The formatted URN is: [fooSchema].[barTable].[someColumn]
```
## **RangeTuple**
---
Coming soon.
### **Code Examples:**
---
## **Definitions**
---
Coming soon.
### **Code Examples:**
---
## **SQLDefinitions**
---
Coming soon.
### **Code Examples:**
---
## **MSAccessDefinitions**
---
Coming soon.
### **Code Examples:**
---
## **MiscExtensions**
---
Coming soon.
### **Code Examples:**
---