﻿#pragma checksum "..\..\FileDownloadPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C2A2112A1A0C465F36309293AB7A236B"
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
using YellowstonePathology.YpiConnect.Client;


namespace YellowstonePathology.YpiConnect.Client {
    
    
    /// <summary>
    /// FileDownloadPage
    /// </summary>
    public partial class FileDownloadPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 75 "..\..\FileDownloadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Hyperlink HyperlinkSignOut;
        
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
            System.Uri resourceLocater = new System.Uri("/YellowstonePathology.YpiConnect.Client;component/filedownloadpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FileDownloadPage.xaml"
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
            
            #line 43 "..\..\FileDownloadPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkHome_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 47 "..\..\FileDownloadPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkReportBrowser_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 51 "..\..\FileDownloadPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkOrderBrowser_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 55 "..\..\FileDownloadPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkBillingBrowser_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 59 "..\..\FileDownloadPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkFileDownload_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 63 "..\..\FileDownloadPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkFileUpload_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 67 "..\..\FileDownloadPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkContactUs_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 71 "..\..\FileDownloadPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkDesktopShortcuts_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.HyperlinkSignOut = ((System.Windows.Documents.Hyperlink)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

