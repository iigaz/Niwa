using System.Web;
using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Niwa.Extensions;

public class LinkExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        pipeline.DocumentProcessed += ChangeLinkPath;
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
    }

    private static void ChangeLinkPath(MarkdownDocument document)
    {
        foreach (var link in document.Descendants<LinkInline>())
            if (!link.IsImage)
                link.Url = "/link?url=" + HttpUtility.UrlEncode(link.Url);
    }
}