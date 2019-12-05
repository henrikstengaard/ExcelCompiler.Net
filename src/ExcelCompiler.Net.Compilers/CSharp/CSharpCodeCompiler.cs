using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ExcelCompiler.Net.Contracts;
using ExcelCompiler.Net.Entities;
using ExcelCompiler.Net.Entities.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace ExcelCompiler.Net.Compilers.CSharp
{
    public static class CSharpCodeCompiler
    {
        public static IEnumerable<byte> GetAssemblyBytes(string assemblyName, string code)
        {
            using (var peStream = new MemoryStream())
            {
                var result = GetCompilation(assemblyName, code).Emit(peStream);
                if (!result.Success)
                {
                    throw new Exception(
                        $"Failed to compile: {string.Join($",{Environment.NewLine}", result.Diagnostics.Select(x => x.ToString()))}");
                }
                peStream.Seek(0, SeekOrigin.Begin);
                return peStream.ToArray();
            }
        }

        public static CSharpCompilation GetCompilation(string assemblyName, string code)
        {
            var netStandard = Assembly.Load("netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51");
            var systemCollections = Assembly.Load("System.Collections, Version=4.1.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            
            var codeString = SourceText.From(code);
            var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp7_3);
            var parsedSyntaxTree = SyntaxFactory.ParseSyntaxTree(codeString, options);
            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(netStandard.Location),
                MetadataReference.CreateFromFile(systemCollections.Location),
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Workbook).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IWorkbook).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly
                    .Location),
                MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).Assembly
                    .Location),
            };

            return CSharpCompilation.Create(assemblyName,
                new[] {parsedSyntaxTree},
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
                    optimizationLevel: OptimizationLevel.Release,
                    assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
        }
    }
}