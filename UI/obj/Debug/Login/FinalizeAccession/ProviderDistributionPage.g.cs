﻿#pragma checksum "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C0EC65244E4D5735BE775CB37C274A1E"
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


namespace YellowstonePathology.UI.Login.FinalizeAccession {
    
    
    /// <summary>
    /// ProviderDistributionPage
    /// </summary>
    public partial class ProviderDistributionPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxPanelSets;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonProviderLookup;
        
        #line default
        #line hidden
        
        
        #line 181 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewTestOrderReportDistribution;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewTestOrderReportDistributionLog;
        
        #line default
        #line hidden
        
        
        #line 214 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonBack;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonNext;
        
        #line default
        #line hidden
        
        
        #line 216 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonClose;
        
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
            System.Uri resourceLocater = new System.Uri("/UserInterface;component/login/finalizeaccession/providerdistributionpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
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
            this.ComboBoxPanelSets = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.ButtonProviderLookup = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            this.ButtonProviderLookup.Click += new System.Windows.RoutedEventHandler(this.HyperLinkShowProviderLookup_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 65 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkSetDistribution_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 69 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkAddCopyTo_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 72 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkEditDistribution_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 75 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkDeleteDistribution_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 79 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkPublish_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 82 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkOpenDocumentFolder_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 87 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkScheduleDistributionImmediate_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 90 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkScheduleDistribution15Minute_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 94 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkUnScheduleDistribution_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 99 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkAddMTDOHDistribution_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 102 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkAddWYDOHDistribution_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 105 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkAddFaxDistribution_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 108 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkAddPrintDistribution_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 112 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperLinkAddComplete_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 150 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.CheckBoxDistribute_Unchecked);
            
            #line default
            #line hidden
            return;
            case 18:
            this.ListViewTestOrderReportDistribution = ((System.Windows.Controls.ListView)(target));
            return;
            case 19:
            this.ListViewTestOrderReportDistributionLog = ((System.Windows.Controls.ListView)(target));
            return;
            case 20:
            this.ButtonBack = ((System.Windows.Controls.Button)(target));
            
            #line 214 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            this.ButtonBack.Click += new System.Windows.RoutedEventHandler(this.ButtonBack_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            this.ButtonNext = ((System.Windows.Controls.Button)(target));
            
            #line 215 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            this.ButtonNext.Click += new System.Windows.RoutedEventHandler(this.ButtonNext_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            this.ButtonClose = ((System.Windows.Controls.Button)(target));
            
            #line 216 "..\..\..\..\Login\FinalizeAccession\ProviderDistributionPage.xaml"
            this.ButtonClose.Click += new System.Windows.RoutedEventHandler(this.ButtonClose_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

