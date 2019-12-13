# ExcelCompiler.Net

Excel compiler for .Net can read and parse an Excel spreadsheet file and compile it into a .Net Standard class library.

## Background

I was working on a project where a client had complex price calculations in Excel spreadsheets for products they where going to sell online. 
The client wanted to change these prices from day to day, which meant I couldn't re-write them in C# code as the client wouldn't be able to maintain prices.

Then I discovered DotNetCore.NPOI nuget package, which worked great for reading Excel spreadsheets, set new cell values, evaluating all cell formulas and get values of cells with prices. This solved my first issue as I could now have the client maintaining prices in Excel spreadsheets and use them to update prices, when the client uploaded new Excel spreadsheets.

Next my client wanted to show a price matrix with amount in x coordinate and product variant y coordinate. This meant evaluating all cell formulas for each combination of coordinate variations and was too slow for online usage.

So I started investigating a way of making evaluating all cell formulas faster then reading, parsing and evaluating an Excel spreadsheet.
With a few experiments and prototypes I managed to make a simple console app, that build C# code to represent Excel spreadsheet cells and formulas and turn it onto a C# class.
This was still using DotNetCore.NPOI nuget package to read and parse Excel spreadsheets and my "compiler" to build C# classes from it.

The compiled spreadsheet was now really fast with evaluating all cell formulas went from seconds to a few miliseconds for complex calculations.
As the Excel compiler had re-written the Excel spreadsheet into a C# class I could then load it's assembly and run calculations as if they where part of the website project.

My client would still be able to maintain their complex price calculations in Excel spreadsheets and I could then compile new versions of their Excel spreadsheets when they uploaded them.