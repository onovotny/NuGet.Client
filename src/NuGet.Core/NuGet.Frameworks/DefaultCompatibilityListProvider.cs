// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace NuGet.Frameworks
{
    public sealed class DefaultCompatibilityListProvider : IFrameworkCompatibilityListProvider
    {
        private readonly IFrameworkNameProvider _nameProvider;
        private readonly IFrameworkCompatibilityProvider _compatibilityProvider;
        private readonly FrameworkReducer _reducer;

        public DefaultCompatibilityListProvider()
            : this(DefaultFrameworkNameProvider.Instance, DefaultCompatibilityProvider.Instance)
        {
        }

        public DefaultCompatibilityListProvider(IFrameworkNameProvider nameProvider, IFrameworkCompatibilityProvider compatibilityProvider)
        {
            _nameProvider = nameProvider;
            _compatibilityProvider = compatibilityProvider;
            _reducer = new FrameworkReducer(_nameProvider, _compatibilityProvider);
        }

        public IEnumerable<NuGetFramework> GetFrameworksSupporting(NuGetFramework target)
        {
            var remaining = _nameProvider
                .GetCompatibleCandidates()
                .Where(candidate => _compatibilityProvider.IsCompatible(candidate, target));

            remaining = _reducer.ReduceEquivalent(remaining);

            remaining = ReduceDownwards(remaining);

            return remaining;
        }

        private IEnumerable<NuGetFramework> ReduceDownwards(IEnumerable<NuGetFramework> frameworks)
        {
            // This is a simplified reduce downwards that does not reduce frameworks with
            // different names or PCL frameworks.
            var lookup = frameworks.ToLookup(f => f.IsPCL);
            return lookup[false]
                .GroupBy(f => f.Framework, StringComparer.OrdinalIgnoreCase)
                .Select(g => g.Aggregate((a, b) => a.Version < b.Version ? a : b))
                .Concat(lookup[true]);

        }

        private static IFrameworkCompatibilityListProvider _instance;

        public static IFrameworkCompatibilityListProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DefaultCompatibilityListProvider();
                }

                return _instance;
            }
        }
    }
}
