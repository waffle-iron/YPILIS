﻿#pragma checksum "..\..\ScannedDocumentViewerV2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "768D49CD04ED0F5E52E890570F53D900"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
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


namespace YellowstonePathology.UI {
    
    
    /// <summary>
    /// ScannedDocumentViewerV2
    /// </summary>
    public partial class ScannedDocumentViewerV2 : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\ScannedDocumentViewerV2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBloxDocumentNotFound;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\ScannedDocumentViewerV2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImageDocument;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\ScannedDocumentViewerV2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxThumbnails;
        
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
            System.Uri resourceLocater = new System.Uri("/UserInterface;component/scanneddocumentviewerv2.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ScannedDocumentViewerV2.xaml"
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
            this.TextBloxDocumentNotFound = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.ImageDocument = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            
            #line 37 "..\..\ScannedDocumentViewerV2.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItemPrintPage_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 38 "..\..\ScannedDocumentViewerV2.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItemPrintAllPages_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 39 "..\..\ScannedDocumentViewerV2.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItemRotatePage_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 40 "..\..\ScannedDocumentViewerV2.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItemDeletePages_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ListBoxThumbnails = ((System.Windows.Controls.ListBox)(target));
            
            #line 45 "..\..\ScannedDocumentViewerV2.xaml"
            this.ListBoxThumbnails.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBoxThumbnails_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 48 "..\..\ScannedDocumentViewerV2.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItemRotateThumbnail_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 49 "..\..\ScannedDocumentViewerV2.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItemDeletePages_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

