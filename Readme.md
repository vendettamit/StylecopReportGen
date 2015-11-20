## Style cop rule violation report(html) generation tool.

The utility is created to generate a visual report from Stylecopviolation.xml file.

Version 1.0
#### Usage
`StylecopReportGen.exe <fullpath_solutionfile.sln> [optional]<Release/Debug/Build configuration name..> [optional]<stylecopTransform.xslt>`

#### Report sample
![sample screenshot of report](https://github.com/vendettamit/StylecopReportGen/blob/master/sampleReport/Report_Screenshot.png)
#### How it works?
- This is a commandLine utility that feeds on visual studio solutionfile. 
- Build configuration is optional argument and only required if the stylecop analysis was performed on build configuration other than `Debug`
- Buit-in stylecopTransform.xslt is used. But if you have a custom transform xslt file you can use that too by supplying command line argument.

#### Setup 
- To generate the report customize the arguments in ReportBuilder.bat or use StylcopReportGen.exe with proper arguments.
- Generated reports can be found in the same directory as SteylcopReportGen.exe.
- Download [XMLPrime library](https://www.xmlprime.com/xmlprime) and add it to the reference to make application working. 

#### Found any bugs:
Create an issue in issue section.

Licensing -
This utility is free to use, recreate etc. for non-commercial purposes. Here non-commerical means you're not chargning money for distributing copies of this utility. 
 
_This utility is created with the help of XMLPrime library. The free usage of this library is limited to non-commercial purposes. please refer to the [licensing terms](https://www.xmlprime.com/xmlprime/licensing.htm) for more information._
