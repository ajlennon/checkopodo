CheckOpodo
==========

CheckOpodo allows to you graph costs for return plane flights across a range of dates. The tool queries Opodo with different start dates. Set the maximum range using the Departure Date and Return Date calendar pickers. Then select the number of days to increment for each Opodo query, and the minimum length of stay. Click 'Search' and the utility will start building a graph of return ticket prices. For more details, to request enhancements or to report issues with the software please contact Dynamic Devices at info@dynamicdevices.co.uk

Originally written in 2006 to get to grips with C# .NET I've updated this to Visual Studio 2010 
and it still seems to be scraping Opodo correctly.

NOTE: You may need to install the MSI to be able to debug the application as the Tidy.dll used needs to be registered.

ChangeLog
=========

- 1.4				30/10/2013		AJL			Pulled into VS2010 and released as GPLv3 via GitHub

- 1.3								AJL			Modified to fix broken scraping of Opodo

- 1.2				04/08/2006		AJL			Added site "down for maintenance" message

- 1.1				31/07/2006		AJL			Backported to create a VS.NET2003 solution
											which can target .NET 1.1.
											
											Added support for the default IE proxy
											
											Separated Core (query) project from UI code
											
											Added thread shutdown on form close.
											
- 1.0				30/07/2006		AJL			Initial Release

TODO
====

- Add search/dropdowns for airport codes
- Add query timeout support.
- Harmonise .NET 2 and .NET 1.1 versions
- Rework queries to abstract them out better
- Parse using better HTML cleanup / XML method
- Add closed loop injection testing

