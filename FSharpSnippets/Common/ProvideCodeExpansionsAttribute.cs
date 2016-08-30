using System;
using System.Globalization;
using Microsoft.VisualStudio.Shell;

namespace FSharpSnippets.Common
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
  sealed class ProvideCodeExpansionsAttribute : RegistrationAttribute
  {
    private readonly bool _showRoots;
    private readonly short _displayName;
    private readonly string _languageStringId;
    private readonly string _indexPath;
    private readonly string _paths;
    private readonly Guid _languageGuid;

    public ProvideCodeExpansionsAttribute(string languageGuid, bool showRoots, short displayName, string languageStringId, string indexPath, string paths)
    {
      _showRoots = showRoots;
      _displayName = displayName;
      _languageStringId = languageStringId;
      _indexPath = indexPath;
      _paths = paths;
      _languageGuid = new Guid(languageGuid);
    }

    public string LanguageGuid => _languageGuid.ToString("B");
    public bool ShowRoots => _showRoots;
    public short DisplayName => _displayName;
    public string LanguageStringId => _languageStringId;
    public string IndexPath => _indexPath;
    public string Paths => _paths;

    private string LanguageName()
    {
      return $"Languages\\CodeExpansions\\{LanguageStringId}";
    }
    

    public override void Register(RegistrationContext context)
    {
      if (context == null) return;

      using (var childKey = context.CreateKey(LanguageName()))
      {
        childKey.SetValue("", LanguageGuid);
        string snippetIndexPath = context.ComponentPath;
        snippetIndexPath = System.IO.Path.Combine(snippetIndexPath, IndexPath);
        snippetIndexPath = context.EscapePath(System.IO.Path.GetFullPath(snippetIndexPath));

        childKey.SetValue("DisplayName", DisplayName.ToString(CultureInfo.InvariantCulture));
        childKey.SetValue("IndexPath", snippetIndexPath);
        childKey.SetValue("LangStringId", LanguageStringId.ToLowerInvariant());
        childKey.SetValue("Package", context.ComponentType.GUID.ToString("B"));
        childKey.SetValue("ShowRoots", ShowRoots ? 1 : 0);

        string snippetPaths = context.ComponentPath;
        snippetPaths = System.IO.Path.Combine(snippetPaths, Paths);
        snippetPaths = context.EscapePath(System.IO.Path.GetFullPath(snippetPaths));

        string myDocumentsPath = @";%MyDocs%\Code Snippets\" + _languageStringId + @"\My Code Snippets\";
        using (var forceSubKey = childKey.CreateSubkey("ForceCreateDirs"))
        {
          forceSubKey.SetValue(LanguageStringId, snippetPaths + myDocumentsPath);
        }

        using (var pathsSubKey = childKey.CreateSubkey("Paths"))
        {
          pathsSubKey.SetValue(LanguageStringId, snippetPaths + myDocumentsPath);
        }
      }
    }

    public override void Unregister(RegistrationContext context)
    {
      if (context != null)
      {
        context.RemoveKey(LanguageName());
      }
    }
  }
}
