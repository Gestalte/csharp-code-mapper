using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace CodeMapperExtension
{
    /// <summary>
    /// A command for showing a tool window.
    /// </summary>
    [VisualStudioContribution]
    public class CodeMapCommand : Command
    {
        /// <summary>CodeMapCommand" /> class.
        /// </summary>
        /// <param name="extensibility">Extensibility object instance.</param>
        public CodeMapCommand(VisualStudioExtensibility extensibility)
            : base(extensibility)
        {
        }

        /// <inheritdoc />
        public override CommandConfiguration CommandConfiguration => new(displayName: "Show Code Map")
        {
            // Use this object initializer to set optional parameters for the command. The required parameter,
            // displayName, is set above. To localize the displayName, add an entry in .vsextension\string-resources.json
            // and reference it here by passing "%CodeMapperExtension.CodeMapCommand.DisplayName%" as a constructor parameter.
            Placements = new[] { CommandPlacement.KnownPlacements.ExtensionsMenu },
            Icon = new(ImageMoniker.KnownValues.Extension, IconSettings.IconAndText),
        };

        /// <inheritdoc />
        public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
        {
            await this.Extensibility.Shell().ShowToolWindowAsync<CodeMap>(activate: true, cancellationToken);
        }
    }
}