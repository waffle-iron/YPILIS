﻿#pragma checksum "..\..\..\..\Login\FinalizeAccession\DocumentScanningPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8EF0BC8C3874ADFDBB11F2AE0EA70872"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using YellowstonePathology.UI;
using YellowstonePathology.UI.Converter;
using YellowstonePathology.UI.CustomControls;
using YellowstonePathology.UI.ValidationRules;


namespace YellowstonePathology.UI.Login.FinalizeAccession {
    
    
    /// <summary>
    /// DocumentScanningPage
    /// </summary>
    public partial class DocumentScanningPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 49 "..\..\..\..\Login\FinalizeAccession\DocumentScanningPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer ScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Login\FinalizeAccession\DocumentScanningPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackPanelImages;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Login\FinalizeAccession\DocumentScanningPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonBack;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Login\FinalizeAccession\DocumentScanningPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonNext;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UserInterface;component/login/finalizeaccession/documentscanningpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Login\FinalizeAccession\DocumentScanningPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 40 "..\..\..\..\Login\FinalizeAccession\DocumentScanningPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonScan_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 44 "..\..\..\..\Login\FinalizeAccession\DocumentScanningPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonPrint_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 45 "..\..\..\..\Login\FinalizeAccession\DocumentScanningPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonDelete_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 5:
            this.StackPanelImages = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.ButtonBack = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\Login\FinalizeAccession\DocumentScanningPage.xaml"
            this.ButtonBack.Click += new System.Windows.RoutedEventHandler(this.ButtonBack_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonNext = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\..\Login\FinalizeAccession\DocumentScanningPage.xaml"
            this.ButtonNext.Click += new System.Windows.RoutedEventHandler(this.ButtonNext_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

