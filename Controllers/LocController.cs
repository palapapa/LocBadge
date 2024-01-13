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
    /// <exception cref="ArgumentException">When <paramref name="ignoredPath"/> contains an invalid escape sequence.</exception>
    /// <param name="ignoredPath">A comma-separated list of ignored paths.</param>
    /// <returns>An <see cref="IList{T}"/> of ignored paths.</returns>
    private static List<string> ParseCommaSeparatedIgnoredPaths(string ignoredPath)
    {
        List<StringBuilder> results = [];
        StringBuilder result = new();
        bool isInEscapeSequence = false;
        for (int i = 0; i < ignoredPath.Length; i++)
        {
            if (!isInEscapeSequence)
            {
                switch (ignoredPath[i])
                {
                    case '\\':
                        isInEscapeSequence = true;
                        break;
                    // If a comma is read and it's not being escaped, a path has been completely read.
                    case ',':
                        results.Add(result);
                        if (i != ignoredPath.Length - 1) // Don't replace the current ignored path if this is the last path
                        {
                            result = new();
                        }
                        // If this is the last character, the loop ends and the last result needs to be added to results outside the loop
                        break;
                    default:
                        result.Append(ignoredPath[i]);
                        break;
                }
            }
            else
            {
                switch (ignoredPath[i])
                {
                    case '\\':
                    case ',':
                        isInEscapeSequence = false;
                        result.Append(ignoredPath[i]);
                        break;
                    default:
                        throw new ArgumentException($"Invalid escape sequence at position {i}.");
                }
            }
        }
        if (isInEscapeSequence) // If the last character is a backslash
        {
            throw new ArgumentException($"Invalid escape sequence at position {ignoredPath.Length - 1}.");
        }
        results.Add(result); // After the loop ends, there will still be one last ignored path not added ot results
        return results.Select(result => result.ToString()).ToList();
    }
}
