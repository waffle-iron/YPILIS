﻿#pragma checksum "..\..\..\OrderEntry\OrderEntryPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9C9669907C5ACC7141CF11C32AF5EF97"
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
using YellowstonePathology.YpiConnect.Client;
using YellowstonePathology.YpiConnect.Client.Converter;


namespace YellowstonePathology.YpiConnect.Client.OrderEntry {
    
    
    /// <summary>
    /// OrderEntryPage
    /// </summary>
    public partial class OrderEntryPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 6 "..\..\..\OrderEntry\OrderEntryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal YellowstonePathology.YpiConnect.Client.OrderEntry.OrderEntryPage OrderEntryControl;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\OrderEntry\OrderEntryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Hyperlink HyperlinkBack;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\OrderEntry\OrderEntryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Hyperlink HyperlinkAddSpecimen;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\OrderEntry\OrderEntryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridMainContent;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\OrderEntry\OrderEntryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxSvhMrn;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\OrderEntry\OrderEntryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxSvhAccountNo;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\OrderEntry\OrderEntryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxPSex;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\OrderEntry\OrderEntryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ComboBoxProviderName;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\OrderEntry\OrderEntryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBoxOrderDate;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\OrderEntry\OrderEntryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewSpecimen;
        
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
            System.Uri resourceLocater = new System.Uri("/YellowstonePathology.YpiConnect.Client;component/orderentry/orderentrypage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\OrderEntry\OrderEntryPage.xaml"
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
            this.OrderEntryControl = ((YellowstonePathology.YpiConnect.Client.OrderEntry.OrderEntryPage)(target));
            return;
            case 2:
            this.HyperlinkBack = ((System.Windows.Documents.Hyperlink)(target));
            
            #line 49 "..\..\..\OrderEntry\OrderEntryPage.xaml"
            this.HyperlinkBack.Click += new System.Windows.RoutedEventHandler(this.HyperlinkBack_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 53 "..\..\..\OrderEntry\OrderEntryPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkOwnership_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.HyperlinkAddSpecimen = ((System.Windows.Documents.Hyperlink)(target));
            
            #line 57 "..\..\..\OrderEntry\OrderEntryPage.xaml"
            this.HyperlinkAddSpecimen.Click += new System.Windows.RoutedEventHandler(this.HyperlinkAddSpecimen_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 61 "..\..\..\OrderEntry\OrderEntryPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkSubmit_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.GridMainContent = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.TextBoxSvhMrn = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.TextBoxSvhAccountNo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.ComboBoxPSex = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.ComboBoxProviderName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.TextBoxOrderDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.ListViewSpecimen = ((System.Windows.Controls.ListView)(target));
            
            #line 162 "..\..\..\OrderEntry\OrderEntryPage.xaml"
            this.ListViewSpecimen.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListViewSpecimen_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 13:
            
            #line 175 "..\..\..\OrderEntry\OrderEntryPage.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.HyperlinkDeleteSpecimen_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

