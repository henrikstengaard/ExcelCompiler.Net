﻿using System.Text;
using ExcelCompiler.Net.Entities;

namespace ExcelCompiler.Net.Compilers.CSharp
{
    public class ModuleBuilder
    {
        private readonly WorkbookBuilder workbookBuilder;

        public ModuleBuilder(WorkbookBuilder workbookBuilder)
        {
            this.workbookBuilder = workbookBuilder;
        }

        public string Build(string namespaceName, Workbook workbook)
        {
            var code = new StringBuilder();
            code.AppendLine("using System;");
            code.AppendLine("using System.Collections.Generic;");
            code.AppendLine("using System.Globalization;");
            code.AppendLine("using ExcelCompiler.Net.Contracts;");
            code.AppendLine("using ExcelCompiler.Net.Entities;");
            code.AppendLine("using static ExcelCompiler.Net.Formulas.Formula;");
            code.AppendLine(string.Empty);
            code.AppendLine($"namespace {namespaceName}");
            code.AppendLine("{");
            code.Append(workbookBuilder.Build(workbook));
            code.AppendLine("}");            
            return code.ToString();
        }
    }
}