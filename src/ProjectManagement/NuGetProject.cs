﻿using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.PackagingCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NuGet.ProjectManagement
{
    public abstract class NuGetProject
    {
        protected NuGetProject() : this(new Dictionary<string, object>()) { }
        protected NuGetProject(IDictionary<string, object> metadata)
        {
            if(metadata == null)
            {
                throw new ArgumentNullException("metadata");
            }
            InternalMetadata = metadata;
        }
        protected IDictionary<string, object> InternalMetadata { get; set; }
        public IReadOnlyDictionary<string, object> Metadata
        {
            get
            {
                return (IReadOnlyDictionary<string, object>)InternalMetadata;
            }
        }
        // TODO: Consider adding CancellationToken here
        /// <summary>
        /// This installs a package into the NuGetProject using the packageStream passed in
        /// <param name="packageStream"></param> should be seekable
        /// </summary>
        /// <returns>Returns false if the package was already present in the NuGetProject. On successful installation, returns true</returns>
        public abstract Task<bool> InstallPackageAsync(PackageIdentity packageIdentity, Stream packageStream,
            INuGetProjectContext nuGetProjectContext, CancellationToken token);
        /// <summary>
        /// This uninstalls the package from the NuGetProject, if found
        /// </summary>
        /// <returns>Returns false if the package was not found. On successful uninstallation, returns true</returns>
        public abstract Task<bool> UninstallPackageAsync(PackageIdentity packageIdentity, INuGetProjectContext nuGetProjectContext, CancellationToken token);
        /// <summary>
        /// GetInstalledPackages will be used by Dependency Resolver and more
        /// </summary>
        /// <returns></returns>
        public abstract Task<IEnumerable<PackageReference>> GetInstalledPackagesAsync(CancellationToken token);
        public T GetMetadata<T>(string key)
        {
            if(key == null)
            {
                throw new ArgumentNullException("key");
            }

            object value = Metadata[key];
            return (T)value;
        }

        public bool TryGetMetadata<T>(string key, out T value)
        {
            value = default(T);
            try
            {
                value = GetMetadata<T>(key);
                return true;
            }
            catch (KeyNotFoundException ex)
            {
            }
            catch(InvalidCastException ex)
            {
            }
            catch(Exception ex)
            {
            }
            return false;
        }
    }

    public static class NuGetProjectMetadataKeys
    {
        public const string Name = "Name";
        public const string UniqueName = "UniqueName";
        public const string TargetFramework = "TargetFramework";
        public const string FullPath = "FullPath";
    }
}
