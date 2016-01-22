﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Xunit;

namespace NuGet.Frameworks.Test
{
    public class CompatibilityListProviderTests
    {
        [Fact]
        public void CompatibilityListProvider_NetStandardSupporting()
        {
            // Arrange
            var provider = DefaultCompatibilityListProvider.Instance;

            // Act
            var actual = provider
                .GetFrameworksSupporting(FrameworkConstants.CommonFrameworks.NetStandard12)
                .Select(f => f.ToString())
                .ToArray();

            // Assert
            // positive
            Assert.Contains(".NETPortable,Version=v0.0,Profile=Profile32", actual); // PCL frameworks are not reduced
            Assert.Contains(".NETPortable,Version=v0.0,Profile=Profile44", actual);
            Assert.Contains(".NETPortable,Version=v0.0,Profile=Profile151", actual);
            Assert.Contains("Windows,Version=v8.1", actual); // the preferred equivalent name is returned
            Assert.Contains(".NETStandard,Version=v1.2", actual); // the framework itself is supported
            Assert.Contains(".NETStandardApp,Version=v1.2", actual); // superset frameworks are supported
            Assert.Contains(".NETFramework,Version=v4.5.1", actual);
            Assert.Contains("DNXCore,Version=v5.0", actual);
            Assert.Contains("MonoAndroid,Version=v0.0", actual);
            Assert.Contains("MonoMac,Version=v0.0", actual);
            Assert.Contains("MonoTouch,Version=v0.0", actual);
            Assert.Contains("UAP,Version=v10.0", actual);
            Assert.Contains("WindowsPhoneApp,Version=v8.1", actual);
            Assert.Contains("Xamarin.iOS,Version=v0.0", actual);
            Assert.Contains("Xamarin.Mac,Version=v0.0", actual);
            Assert.Contains("Xamarin.PlayStation3,Version=v0.0", actual);
            Assert.Contains("Xamarin.PlayStation4,Version=v0.0", actual);
            Assert.Contains("Xamarin.PlayStationVita,Version=v0.0", actual);
            Assert.Contains("Xamarin.TVOS,Version=v0.0", actual);
            Assert.Contains("Xamarin.WatchOS,Version=v0.0", actual);
            Assert.Contains("Xamarin.Xbox360,Version=v0.0", actual);
            Assert.Contains("Xamarin.XboxOne,Version=v0.0", actual);

            // negative
            Assert.DoesNotContain(".NETFramework,Version=v4.6", actual); // only the minimum support version is returned
            Assert.DoesNotContain(".NETFramework,Version=v4.5", actual); // versions that are too small are not returned
            Assert.DoesNotContain(".NETPlatform,Version=v5.3", actual); // frameworks with no relationship are not returned
        }
    }
}
