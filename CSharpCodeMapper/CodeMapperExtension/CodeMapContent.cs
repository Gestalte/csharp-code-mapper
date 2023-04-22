using Microsoft.VisualStudio.Extensibility.UI;

namespace CodeMapperExtension
{
    /// <summary>
    /// A remote user control to use as tool window UI content.
    /// </summary>
    internal class CodeMapContent : RemoteUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeMapContent" /> class.
        /// </summary>
        public CodeMapContent()
            : base(dataContext: new CodeMapData())
        {
        }
    }
}