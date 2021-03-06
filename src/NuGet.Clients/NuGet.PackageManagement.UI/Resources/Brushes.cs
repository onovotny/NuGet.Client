﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;

namespace NuGet.PackageManagement.UI
{
    public static class Brushes
    {
        public static object ActiveBorderKey { get; private set; } = SystemColors.ActiveBorderBrushKey;

        public static object BackgroundBrushKey { get; private set; } = SystemColors.WindowBrushKey;

        public static object BorderBrush { get; private set; } = SystemColors.InactiveBorderBrushKey;

        public static object ComboBoxBorderKey { get; private set; } = SystemColors.InactiveBorderBrushKey;

        public static object ContentBrushKey { get; private set; } = SystemColors.WindowBrushKey;

        public static object ContentInactiveSelectedBrushKey { get; private set; } = SystemColors.ControlTextBrushKey;

        public static object ContentInactiveSelectedTextBrushKey { get; private set; } = SystemColors.ControlTextBrushKey;

        public static object ContentMouseOverBrushKey { get; private set; } = SystemColors.ControlTextBrushKey;

        public static object ContentMouseOverTextBrushKey { get; private set; } = SystemColors.ControlTextBrushKey;

        public static object ContentSelectedBrushKey { get; private set; } = SystemColors.ActiveCaptionBrushKey;

        public static object ContentSelectedTextBrushKey { get; private set; } = SystemColors.ActiveCaptionTextBrushKey;

        public static object ControlLinkTextHoverKey { get; private set; } = SystemColors.HighlightBrushKey;

        public static object ControlLinkTextKey { get; private set; } = SystemColors.HighlightBrushKey;

        public static object DetailPaneBackground { get; private set; } = SystemColors.WindowBrushKey;

        public static object HeaderBackground { get; private set; } = SystemColors.WindowBrushKey;

        public static object HeaderColorsDefaultBrushKey { get; private set; } = SystemColors.WindowBrushKey;

        public static object HeaderColorsDefaultTextBrushKey { get; private set; } = SystemColors.WindowTextBrushKey;

        public static object HeaderColorsMouseDownBrushKey { get; private set; } = SystemColors.WindowBrushKey;

        public static object HeaderColorsMouseDownTextBrushKey { get; private set; } = SystemColors.WindowTextBrushKey;

        public static object HeaderColorsMouseOverBrushKey { get; private set; } = SystemColors.WindowBrushKey;

        public static object HeaderColorsMouseOverTextBrushKey { get; private set; } = SystemColors.WindowTextBrushKey;

        public static object HeaderColorsSeparatorLineBrushKey { get; private set; } = SystemColors.ActiveBorderBrushKey;

        public static object IndicatorFillBrushKey { get; private set; } = SystemColors.WindowFrameColor;

        public static object InfoBackgroundKey { get; private set; } = SystemColors.InfoBrushKey;

        public static object InfoTextKey { get; private set; } = SystemColors.InfoTextBrushKey;

        public static object LegalMessageBackground { get; private set; } = SystemColors.ControlBrushKey;

        public static object ListPaneBackground { get; private set; } = SystemColors.WindowBrushKey;

        public static object SplitterBackgroundKey { get; private set; } = SystemColors.WindowBrushKey;

        public static object ToolWindowBorderKey { get; private set; } = SystemColors.WindowBrushKey;

        public static object ToolWindowButtonDownBorderKey { get; private set; } = SystemColors.WindowBrushKey;

        public static object ToolWindowButtonDownKey { get; private set; } = SystemColors.WindowBrushKey;

        public static object ToolWindowButtonHoverActiveBorderKey { get; private set; } = SystemColors.WindowBrushKey;

        public static object ToolWindowButtonHoverActiveKey { get; private set; } = SystemColors.WindowBrushKey;

        public static object UIText { get; private set; } = SystemColors.ControlTextBrushKey;

        public static object WindowTextKey { get; private set; } = SystemColors.WindowTextBrushKey;

        public static object CheckBoxBackgroundBrushKey { get; private set; } = SystemColors.WindowBrushKey; 

        public static object CheckBoxBackgroundDisabledBrushKey { get; private set; } = SystemColors.InactiveBorderBrushKey; 

        public static object CheckBoxBackgroundHoverBrushKey { get; private set; } = SystemColors.HighlightBrushKey; 

        public static object CheckBoxBackgroundPressedBrushKey { get; private set; } = SystemColors.GradientActiveCaptionBrushKey; 

        public static object CheckBoxBorderBrushKey { get; private set; } = SystemColors.ActiveBorderBrushKey; 

        public static object CheckBoxBorderDisabledBrushKey { get; private set; } = SystemColors.InactiveBorderBrushKey; 

        public static object CheckBoxGlyphBrushKey { get; private set; } = SystemColors.WindowTextBrushKey; 

        public static object CheckBoxGlyphHoverBrushKey { get; private set; } = SystemColors.HighlightTextBrushKey; 

        public static object CheckBoxGlyphDisabledBrushKey { get; private set; } = SystemColors.GrayTextBrushKey; 

        public static object CheckBoxGlyphPressedBrushKey { get; private set; } = SystemColors.WindowBrushKey; 

        public static object CheckBoxTextBrushKey { get; private set; } = SystemColors.ControlTextBrushKey; 

        public static object CheckBoxTextDisabledBrushKey { get; private set; } = SystemColors.GrayTextBrushKey; 

        public static object CheckBoxTextHoverBrushKey { get; private set; } = SystemColors.HighlightTextBrushKey; 

        public static object CheckBoxTextPressedBrushKey { get; private set; } = SystemColors.WindowTextBrushKey; 

        public static object CheckBoxBorderHoverBrushKey { get; private set; } = SystemColors.HighlightBrushKey; 

        public static object CheckBoxBorderPressedBrushKey { get; private set; } = SystemColors.HighlightBrushKey; 

        public static object FocusVisualStyleBrushKey { get; private set; } = SystemColors.ControlTextBrushKey; 

        public static object ButtonTextStyleBrushKey { get; private set; } = SystemColors.ControlTextBrushKey; 

        public static object ButtonBorderBrushKey { get; private set; } = SystemColors.ActiveBorderBrushKey; 

        public static object ButtonDisabledTextStyleBrushKey { get; private set; } = SystemColors.GrayTextBrush; 

        public static object ButtonBackgroundStyleBrushKey { get; private set; } = SystemColors.ControlBrushKey; 

        public static object ButtonDisabledStyleBrushKey { get; private set; } = SystemColors.InactiveBorderBrushKey; 

        public static object ButtonPressedStyleBrushKey { get; private set; } = SystemColors.InactiveBorderBrushKey; 

        public static object ButtonPressedBorderStyleBrushKey { get; private set; } = SystemColors.InactiveBorderBrushKey; 

        public static object ButtonPressedTextStyleBrushKey { get; private set; } = SystemColors.WindowTextBrushKey; 

        public static object ButtonHoverBorderStyleBrushKey { get; private set; } = SystemColors.HighlightTextBrushKey; 

        public static object ButtonHoverStyleBrushKey { get; private set; } = SystemColors.HighlightBrushKey; 

        public static object ButtonHoverTextStyleBrushKey { get; private set; } = SystemColors.HighlightTextBrushKey; 

        public static object ButtonDisabledBorderStyleBrushKey { get; private set; } = SystemColors.HighlightTextBrushKey; 
 
        public static object ButtonDefaultStyleBrushKey { get; private set; } = SystemColors.HighlightTextBrushKey; 
 
        public static object ButtonDefaultBorderStyleBrushKey { get; private set; } = SystemColors.HighlightTextBrushKey; 

        public static object ButtonDefaultTextStyleBrushKey { get; private set; } = SystemColors.HighlightTextBrushKey; 
 
        public static object ButtonFocusedTextStyleBrushKey { get; private set; } = SystemColors.HighlightTextBrushKey; 
 
        public static object ButtonBorderFocusedStyleBrushKey { get; private set; } = SystemColors.HighlightTextBrushKey; 
 
        public static object ButtonFocusedStyleBrushKey { get; private set; } = SystemColors.HighlightTextBrushKey; 

        public static void LoadVsBrushes()
        {
            FocusVisualStyleBrushKey = VsBrushes.ToolWindowTextKey; 
            ActiveBorderKey = VsBrushes.ActiveBorderKey;
            BorderBrush = VsBrushes.BrandedUIBorderKey;
            ComboBoxBorderKey = VsBrushes.ComboBoxBorderKey;
            ControlLinkTextHoverKey = VsBrushes.ControlLinkTextHoverKey;
            ControlLinkTextKey = VsBrushes.ControlLinkTextKey;
            DetailPaneBackground = VsBrushes.BrandedUIBackgroundKey;
            HeaderBackground = VsBrushes.BrandedUIBackgroundKey;
            InfoBackgroundKey = VsBrushes.InfoBackgroundKey;
            InfoTextKey = VsBrushes.InfoTextKey;
            LegalMessageBackground = VsBrushes.BrandedUIBackgroundKey;
            ListPaneBackground = VsBrushes.BrandedUIBackgroundKey;
            SplitterBackgroundKey = VsBrushes.CommandShelfBackgroundGradientKey;
            ToolWindowBorderKey = VsBrushes.ToolWindowBorderKey;
            ToolWindowButtonDownBorderKey = VsBrushes.ToolWindowButtonDownBorderKey;
            ToolWindowButtonDownKey = VsBrushes.ToolWindowButtonDownKey;
            ToolWindowButtonHoverActiveBorderKey = VsBrushes.ToolWindowButtonHoverActiveBorderKey;
            ToolWindowButtonHoverActiveKey = VsBrushes.ToolWindowButtonHoverActiveKey;
            UIText = VsBrushes.BrandedUITextKey;
            WindowTextKey = VsBrushes.WindowTextKey;

            HeaderColorsDefaultBrushKey = HeaderColors.DefaultBrushKey;
            HeaderColorsDefaultTextBrushKey = HeaderColors.DefaultTextBrushKey;
            HeaderColorsMouseDownBrushKey = HeaderColors.MouseDownBrushKey;
            HeaderColorsMouseDownTextBrushKey = HeaderColors.MouseDownTextBrushKey;
            HeaderColorsMouseOverBrushKey = HeaderColors.MouseOverBrushKey;
            HeaderColorsMouseOverTextBrushKey = HeaderColors.MouseOverTextBrushKey;
            HeaderColorsSeparatorLineBrushKey = HeaderColors.SeparatorLineBrushKey;

            IndicatorFillBrushKey = ProgressBarColors.IndicatorFillBrushKey;
            ButtonTextStyleBrushKey = CommonControlsColors.ButtonTextBrushKey; 
            ButtonBorderBrushKey = CommonControlsColors.ButtonBorderBrushKey; 
            ButtonBackgroundStyleBrushKey = CommonControlsColors.ButtonBrushKey; 
            ButtonDisabledTextStyleBrushKey = CommonControlsColors.ButtonDisabledTextBrushKey; 
            ButtonDisabledStyleBrushKey = CommonControlsColors.ButtonDisabledBrushKey; 
            ButtonDisabledBorderStyleBrushKey = CommonControlsColors.ButtonBorderDisabledBrushKey; 
            ButtonDefaultStyleBrushKey = CommonControlsColors.ButtonDefaultBrushKey; 
            ButtonDefaultBorderStyleBrushKey = CommonControlsColors.ButtonBorderDefaultBrushKey; 
            ButtonDefaultTextStyleBrushKey = CommonControlsColors.ButtonDefaultTextBrushKey; 
            ButtonPressedStyleBrushKey = CommonControlsColors.ButtonPressedBrushKey; 
            ButtonPressedBorderStyleBrushKey = CommonControlsColors.ButtonBorderPressedBrushKey; 
            ButtonPressedTextStyleBrushKey = CommonControlsColors.ButtonPressedTextBrushKey; 
            ButtonHoverBorderStyleBrushKey = CommonControlsColors.ButtonBorderHoverBrushKey; 
            ButtonHoverStyleBrushKey = CommonControlsColors.ButtonHoverBrushKey; 
            ButtonHoverTextStyleBrushKey = CommonControlsColors.ButtonHoverTextBrushKey; 
            CheckBoxBackgroundBrushKey = CommonControlsColors.CheckBoxBackgroundBrushKey; 
            CheckBoxBackgroundDisabledBrushKey = CommonControlsColors.CheckBoxBackgroundDisabledBrushKey; 
            CheckBoxBackgroundHoverBrushKey = CommonControlsColors.CheckBoxBackgroundHoverBrushKey; 
            CheckBoxBackgroundPressedBrushKey = CommonControlsColors.CheckBoxBackgroundPressedBrushKey; 
            CheckBoxBorderBrushKey = CommonControlsColors.CheckBoxBorderBrushKey; 
            CheckBoxBorderDisabledBrushKey = CommonControlsColors.CheckBoxBorderDisabledBrushKey; 
            CheckBoxGlyphBrushKey = CommonControlsColors.CheckBoxGlyphBrushKey; 
            CheckBoxGlyphHoverBrushKey = CommonControlsColors.CheckBoxGlyphHoverBrushKey; 
            CheckBoxGlyphDisabledBrushKey = CommonControlsColors.CheckBoxGlyphDisabledBrushKey; 
            CheckBoxGlyphPressedBrushKey = CommonControlsColors.CheckBoxGlyphPressedBrushKey; 
            CheckBoxTextBrushKey = CommonControlsColors.CheckBoxTextBrushKey; 
            CheckBoxTextDisabledBrushKey = CommonControlsColors.CheckBoxTextDisabledBrushKey; 
            CheckBoxTextHoverBrushKey = CommonControlsColors.CheckBoxTextHoverBrushKey; 
            CheckBoxTextPressedBrushKey = CommonControlsColors.CheckBoxTextPressedBrushKey; 
            CheckBoxBorderHoverBrushKey = CommonControlsColors.CheckBoxBorderHoverBrushKey; 
            CheckBoxBorderPressedBrushKey = CommonControlsColors.CheckBoxBorderPressedBrushKey; 

            var colorResources = GetColorResources();
            BackgroundBrushKey = colorResources["BackgroundBrushKey"];
            ContentMouseOverBrushKey = colorResources["ContentMouseOverBrushKey"];
            ContentMouseOverTextBrushKey = colorResources["ContentMouseOverTextBrushKey"];
            ContentInactiveSelectedBrushKey = colorResources["ContentInactiveSelectedBrushKey"];
            ContentInactiveSelectedTextBrushKey = colorResources["ContentInactiveSelectedTextBrushKey"];
            ContentSelectedBrushKey = colorResources["ContentSelectedBrushKey"];
            ContentSelectedTextBrushKey = colorResources["ContentSelectedTextBrushKey"];
            ContentBrushKey = colorResources["ContentBrushKey"];
        }

        private static IDictionary<string, ThemeResourceKey> GetColorResources()
        {
            // use colors of VisualStudio UI.
            var assembly = AppDomain.CurrentDomain.Load(
                "Microsoft.VisualStudio.ExtensionsExplorer.UI");
            var colorResources = assembly.GetType(
                "Microsoft.VisualStudio.ExtensionsExplorer.UI.ColorResources");

            var properties = colorResources.GetProperties(BindingFlags.Public | BindingFlags.Static);
            return properties
                .Where(p => p.PropertyType == typeof(ThemeResourceKey))
                .ToDictionary(p => p.Name, p => (ThemeResourceKey)p.GetValue(null));
        }
    }
}