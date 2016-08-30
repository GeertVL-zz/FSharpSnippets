using Microsoft.VisualStudio.Text;

namespace FSharpSnippets.Tools
{
  internal static class EditorExtensions
  {
    internal static bool IsFSharpContent(this ITextBuffer buffer)
    {
      return buffer.ContentType.IsOfType(FSharpCoreConstants.ContentType);
    }

    internal static bool IsFSharpContent(this ITextSnapshot buffer)
    {
      return buffer.ContentType.IsOfType(FSharpCoreConstants.ContentType);
    }
  }
}
