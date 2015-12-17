using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.LibraryModel;
using NuGet.ProjectManagement;
using NuGet.ProjectModel;

namespace NuGet.CommandLine.MSBuildTasks
{
    public class ProjectReferencesTask : Task
    {
        [Required]
        public ITaskItem ProjectFile { get; set; }

        [Required]
        public ITaskItem[] InputFiles { get; set; }

        [Output]
        public ITaskItem[] OutputFiles { get; set; }

        [Output]
        public ITaskItem[] ToProcess { get; set; }

        private const string XProj = ".xproj";

        public override bool Execute()
        {
            var filePath = ProjectFile.ToString();
            var inputFiles = InputFiles.Select(item => item.ToString()).ToList();
            var output = GetChildProjects(filePath, inputFiles);

            OutputFiles = output.Select(name => new TaskItem(name)).ToArray();

            return true;
        }

        private static List<string> GetChildProjects(string filePath, List<string> inputFiles)
        {
            var output = new List<string>();

            foreach (var file in inputFiles)
            {
                output.Add(file);
            }

            if (filePath.EndsWith(XProj, StringComparison.OrdinalIgnoreCase))
            {
                var dir = Path.GetDirectoryName(filePath);
                var jsonPath = Path.Combine(dir, BuildIntegratedProjectUtility.ProjectConfigFileName);

                if (File.Exists(jsonPath))
                {
                    var json = File.ReadAllText(jsonPath);
                    var projectName = Path.GetFileNameWithoutExtension(filePath);

                    var spec = JsonPackageSpecReader.GetPackageSpec(json, projectName, filePath);

                    var resolver = new PackageSpecResolver(spec);

                    // combine all dependencies
                    // This will include references for every TxM, these will have to be filtered later
                    var dependencies = new HashSet<LibraryDependency>();
                    dependencies.UnionWith(spec.Dependencies.Where(d => IsProjectReference(d)));
                    dependencies.UnionWith(spec.TargetFrameworks
                        .SelectMany(f => f.Dependencies)
                        .Where(d => IsProjectReference(d)));

                    foreach (var dependency in spec.Dependencies)
                    {
                        PackageSpec childSpec;
                        if (resolver.TryResolvePackageSpec(dependency.Name, out childSpec))
                        {
                            var childPath = childSpec.FilePath;

                            var childDir = Path.GetDirectoryName(childPath);
                            var dirName = Path.GetFileName(childDir);
                            var xprojPath = Path.Combine(childDir, dirName + XProj);

                            output.Add(xprojPath);
                        }
                    }
                }
            }

            return output;
        }

        private static bool IsProjectReference(LibraryDependency dependency)
        {
            var type = dependency.LibraryRange.TypeConstraint;

            return string.IsNullOrEmpty(type)
                || type == LibraryTypes.Project
                || type == LibraryTypes.ExternalProject;
        }
    }
}
