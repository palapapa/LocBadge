using DotNet.Globbing;

namespace LocBadge.Models;

public record SearchPath
{
    public required Glob Glob { get; init; }

    public required bool IsGlobIncluded { get; init; }
}
