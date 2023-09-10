using DotNet.Globbing;
using System.Collections.Generic;

namespace LocBadge.Models;

public record LocModel
{
    public required IList<Glob> IncludedGlobs { get; init; }

    public required IList<Glob> ExcludedGlobs { get; init; }

    public required IList<string> Extensions { get; init; }

    public required bool AreExtensionsExcluded { get; init; }
}
