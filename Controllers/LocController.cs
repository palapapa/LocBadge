using System;
using Microsoft.AspNetCore.Mvc;
using Ignore;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LocBadge.Controllers;

/// <summary>
/// A controller for retrieving files from a github repo and calculating the total lines of code.
/// </summary>
[Route("[controller]")]
[ApiController]
public class LocController : ControllerBase
{
    /// <summary>
    /// <para>Gets an <see cref="IList{T}"/> of ignored paths from a comma-separated list of ignored paths.</para>
    /// <para>Commas can be escaped by prefixing a backslash and backslashes can be escaped by prefixing a backslash.</para>
    /// </summary>
    /// <exception cref="ArgumentException">When <paramref name="ignorePath"/> contains an invalid escape sequence.</exception>
    /// <param name="ignorePath">A comma-separated list of ignored paths.</param>
    /// <returns>An <see cref="IList{T}"/> of ignored paths.</returns>
    private static List<string> ParseCommaSeparatedIgnorePaths(string ignorePath)
    {
        List<StringBuilder> results = [];
        StringBuilder result = new();
        bool isInEscapeSequence = false;
        for (int i = 0; i < ignorePath.Length; i++)
        {
            if (!isInEscapeSequence)
            {
                switch (ignorePath[i])
                {
                    case '\\':
                        isInEscapeSequence = true;
                        break;
                    // If a comma is read and it's not being escaped, a path has been completely read.
                    case ',':
                        results.Add(result);
                        if (i != ignorePath.Length - 1)
                        {
                            result = new();
                        }
                        break;
                    default:
                        result.Append(ignorePath[i]);
                        break;
                }
            }
            else
            {
                switch (ignorePath[i])
                {
                    case '\\':
                    case ',':
                        isInEscapeSequence = false;
                        result.Append(ignorePath[i]);
                        break;
                    default:
                        throw new ArgumentException($"Invalid escape sequence at position {i}.");
                }
            }
        }
        return results.Select(result => result.ToString()).ToList();
    }
}
