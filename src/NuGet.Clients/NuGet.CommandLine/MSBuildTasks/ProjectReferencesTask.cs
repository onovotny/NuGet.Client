using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.LibraryModel;
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

            if (filePath.EndsWith(".xproj", StringComparison.OrdinalIgnoreCase))
            {
                var dir = Path.GetDirectoryName(filePath);
                var projectName = Path.GetFileNameWithoutExtension(filePath);
                var jsonPath = Path.Combine(dir, "project.json");

                if (File.Exists(jsonPath))
                {
                    var json = File.ReadAllText(jsonPath);

                    var spec = JsonPackageSpecReader.GetPackageSpec(json, projectName, filePath);

                    var resolver = new PackageSpecResolver(spec);

                    var deps = new HashSet<LibraryDependency>();
                    deps.UnionWith(spec.Dependencies);
                    deps.UnionWith(spec.TargetFrameworks.SelectMany(f => f.Dependencies));

                    foreach (var dep in spec.Dependencies)
                    {
                        PackageSpec childSpec;
                        if (resolver.TryResolvePackageSpec(dep.Name, out childSpec))
                        {
                            var childPath = childSpec.FilePath;

                            var childDir = Path.GetDirectoryName(childPath);
                            var dirName = Path.GetFileName(childDir);
                            var xprojPath = Path.Combine(childDir, dirName + ".xproj");

                            output.Add(xprojPath);
                        }
                    }
                }
            }

            return output;
        }
    }
}
