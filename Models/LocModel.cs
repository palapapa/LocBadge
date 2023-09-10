using DotNet.Globbing;
using LocBadge.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LocBadge.Models;

[ModelBinder(BinderType = typeof(LocModelBinder))]
public record LocModel
{
    public required IList<SearchPath> Paths { get; init; }

    public required IList<string> Extensions { get; init; }

    public required bool AreExtensionsExcluded { get; init; }
}
