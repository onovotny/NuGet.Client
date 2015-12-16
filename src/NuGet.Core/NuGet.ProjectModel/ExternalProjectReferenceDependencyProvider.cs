// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NuGet.DependencyResolver;
using NuGet.Frameworks;
using NuGet.LibraryModel;
using NuGet.Versioning;

namespace NuGet.ProjectModel
{
    /// <summary>
    /// Resolves dependencies retrieved from a list of <see cref="ExternalProjectReference" />.
    /// </summary>
    public class ExternalProjectReferenceDependencyProvider : IDependencyProvider
    {
        public IReadOnlyDictionary<string, ExternalProjectReference> ExternalProjects { get; }

        public ExternalProjectReferenceDependencyProvider(IEnumerable<ExternalProjectReference> externalProjects)
        {
            ExternalProjects = new ReadOnlyDictionary<string, ExternalProjectReference>(externalProjects.ToDictionary(e => e.UniqueName, StringComparer.OrdinalIgnoreCase));
        }

        public bool SupportsType(string libraryType)
        {
            return string.Equals(libraryType, LibraryTypes.ExternalProject);
        }

        public IEnumerable<string> GetAttemptedPaths(NuGetFramework targetFramework)
        {
            return Enumerable.Empty<string>();
        }

        public Library GetLibrary(LibraryRange libraryRange, NuGetFramework targetFramework)
        {
            ExternalProjectReference externalProject;
            if (!ExternalProjects.TryGetValue(libraryRange.Name, out externalProject))
            {
                // No project!
                return null;
            }

            // Construct the library and return it
            var library = new Library()
            {
                Identity = new LibraryIdentity()
                {
                    Name = externalProject.UniqueName,
                    Type = LibraryTypes.ExternalProject
                },
                LibraryRange = libraryRange,
                Resolved = true,
            };

            // Fill dependencies from external project references
            var dependencies = externalProject.ExternalProjectReferences.Select(s => new LibraryDependency()
                {
                    LibraryRange = new LibraryRange()
                        {
                            Name = s,
                            VersionRange = null,
                            TypeConstraint = LibraryTypes.ExternalProject
                        },
                    Type = LibraryDependencyType.Default
                }).ToList();

            TargetFrameworkInformation frameworkInfo = null;

            // Add dependencies from the project.json file
            if (externalProject.PackageSpec != null)
            {
                // Add framework-agnostic dependencies
                dependencies.AddRange(externalProject.PackageSpec.Dependencies);

                // Add framework-specific dependencies
                frameworkInfo = externalProject.PackageSpec.GetTargetFramework(targetFramework);
                if (frameworkInfo != null)
                {
                    dependencies.AddRange(frameworkInfo.Dependencies);

                    if (frameworkInfo != null)
                    {
                        library[KnownLibraryProperties.TargetFrameworkInformation] = frameworkInfo;
                    }
                }
            }

            // Set the dependencies
            library.Dependencies = dependencies;

            if (externalProject.PackageSpec != null)
            {
                // Set the file path to project.json
                library.Path = externalProject.PackageSpec.FilePath;

                if (externalProject.MSBuildProjectPath?.EndsWith(".xproj", StringComparison.OrdinalIgnoreCase) == true)
                {
                    // Use the nuspec version
                    library.Identity.Version = externalProject.PackageSpec.Version;

                    // Set the compile asset
                    var tfmFolder = frameworkInfo.FrameworkName.GetShortFolderName();
                    library[KnownLibraryProperties.CompileAsset] = $"{tfmFolder}/{externalProject.PackageSpec.Name}.dll";
                }
            }

            // Default to 1.0.0 if not version exists
            if (library.Identity.Version == null)
            {
                library.Identity.Version = new NuGetVersion("1.0.0");
            }

            if (externalProject.MSBuildProjectPath != null)
            {
                library[KnownLibraryProperties.MSBuildProjectPath] = externalProject.MSBuildProjectPath;
            }

            return library;
        }
    }
}
