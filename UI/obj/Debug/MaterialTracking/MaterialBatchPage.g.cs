﻿#pragma checksum "..\..\..\MaterialTracking\MaterialBatchPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4A737A32C520CF43AE7018AF59D825AD"
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


namespace YellowstonePathology.UI.MaterialTracking {
    
    
    /// <summary>
    /// MaterialBatchPage
    /// </summary>
    public partial class MaterialBatchPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 76 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewMaterialTrackingLog;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonBack;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPrintUpdateScans;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPrintTrackingDocument;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonShowTrackingDocument;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonFinish;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
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
            System.Uri resourceLocater = new System.Uri("/UserInterface;component/materialtracking/materialbatchpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
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
            this.ListViewMaterialTrackingLog = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            
            #line 80 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItemDelete_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonBack = ((System.Windows.Controls.Button)(target));
            
            #line 124 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
            this.ButtonBack.Click += new System.Windows.RoutedEventHandler(this.ButtonBack_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonPrintUpdateScans = ((System.Windows.Controls.Button)(target));
            
            #line 128 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
            this.ButtonPrintUpdateScans.Click += new System.Windows.RoutedEventHandler(this.ButtonPrintUpdateScans_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonPrintTrackingDocument = ((System.Windows.Controls.Button)(target));
            
            #line 129 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
            this.ButtonPrintTrackingDocument.Click += new System.Windows.RoutedEventHandler(this.ButtonPrintTrackingDocument_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonShowTrackingDocument = ((System.Windows.Controls.Button)(target));
            
            #line 130 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
            this.ButtonShowTrackingDocument.Click += new System.Windows.RoutedEventHandler(this.ButtonShowTrackingDocument_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonFinish = ((System.Windows.Controls.Button)(target));
            
            #line 131 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
            this.ButtonFinish.Click += new System.Windows.RoutedEventHandler(this.ButtonFinish_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ButtonNext = ((System.Windows.Controls.Button)(target));
            
            #line 132 "..\..\..\MaterialTracking\MaterialBatchPage.xaml"
            this.ButtonNext.Click += new System.Windows.RoutedEventHandler(this.ButtonNext_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

