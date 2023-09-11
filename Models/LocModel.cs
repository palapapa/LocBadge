using DotNet.Globbing;
using LocBadge.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocBadge.Models;

[ModelBinder(typeof(LocModelBinder))]
public record LocModel
{
    [Required]
    public required IList<SearchPath> Paths { get; init; }

    [Required]
    public required IList<string> Extensions { get; init; }

    [Required]
    public required bool AreExtensionsExcluded { get; init; }
}
