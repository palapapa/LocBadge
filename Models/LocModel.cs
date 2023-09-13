using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocBadge.Models;

public record LocModel
{
    [Required(ErrorMessage = $"{nameof(Paths)} is required.")]
    public required IList<string> Paths { get; init; }

    [Required(ErrorMessage = $"{nameof(Extensions)} is required.")]
    public required IList<string> Extensions { get; init; }

    [Required(ErrorMessage = $"{nameof(AreExtensionsExcluded)} is required.")]
    public required bool AreExtensionsExcluded { get; init; }
}
