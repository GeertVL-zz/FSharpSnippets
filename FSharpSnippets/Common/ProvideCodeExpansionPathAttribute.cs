using System;
using Microsoft.VisualStudio.Shell;

namespace FSharpSnippets.Common
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
  sealed class ProvideCodeExpansionPathAttribute : RegistrationAttribute
  {
    private readonly string _languageStringId;
    private readonly string _description;
    private readonly string _paths;

    public ProvideCodeExpansionPathAttribute(string languageStringId, string description, string paths)
    {
      _languageStringId = languageStringId;
      _description = description;
      _paths = paths;
    }

    public string LanguageStringId => _languageStringId;
    public string Paths => _paths;

    private string LanguageName()
    {
      return $"Languages\\CodeExpansions\\{LanguageStringId}";
    }

    public override void Register(RegistrationContext context)
    {
      using (var childKey = context.CreateKey(LanguageName()))
      {
        string snippetPaths = context.ComponentPath;
        snippetPaths = System.IO.Path.Combine(snippetPaths, Paths);
        snippetPaths = context.EscapePath(System.IO.Path.GetFullPath(snippetPaths));

        using (var pathsSubKey = childKey.CreateSubkey("Paths"))
        {
          pathsSubKey.SetValue(_description, snippetPaths);
        }
      }
    }

    public override void Unregister(RegistrationContext context)
    {
    }
  }
}
