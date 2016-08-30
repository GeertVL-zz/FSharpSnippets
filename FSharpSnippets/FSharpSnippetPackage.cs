//------------------------------------------------------------------------------
// <copyright file="FSharpSnippetPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using FSharpSnippets.Common;
using Microsoft.VisualStudio.Shell;

namespace FSharpSnippets
{
  [PackageRegistration(UseManagedResourcesOnly = true)]
  [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
  [Guid(FSharpSnippetPackage.PackageGuidString)]
  [ProvideCodeExpansions(GuidList.guidFsharpLanguageService, false, 106, "FSharp", @"Snippets\%LCID%\SnippetsIndex.xml", @"Snippets\%LCID%\FSharp\")]
  [ProvideCodeExpansionPath("FSharp", "Test", @"Snippets\%LCID%\Test\")]
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
  public sealed class FSharpSnippetPackage : CommonPackage
  {
    /// <summary>
    /// FSharpSnippetPackage GUID string.
    /// </summary>
    public const string PackageGuidString = "d540a6b8-15c3-43f5-9e74-d056647c4d9f";

    /// <summary>
    /// Initializes a new instance of the <see cref="FSharpSnippetPackage"/> class.
    /// </summary>
    public FSharpSnippetPackage()
    {
      // Inside this method you can place any initialization code that does not require
      // any Visual Studio service because at this point the package object is created but
      // not sited yet inside Visual Studio environment. The place to do all the other
      // initialization is the Initialize method.
    }

    #region Package Members

    /// <summary>
    /// Initialization of the package; this method is called right after the package is sited, so this is the place
    /// where you can put all the initialization code that rely on services provided by VisualStudio.
    /// </summary>
    protected override void Initialize()
    {
            base.Initialize();
    }

    #endregion
  }
}
