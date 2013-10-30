CheckOpodo
==========

Checks Flight Prices on Opodo over a range of dates. 

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

