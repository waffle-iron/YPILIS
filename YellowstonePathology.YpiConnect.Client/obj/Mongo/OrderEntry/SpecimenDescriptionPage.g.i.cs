﻿#pragma checksum "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "70310842F997954F13BB407CA2202536"
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
    /// SpecimenDescriptionPage
    /// </summary>
    public partial class SpecimenDescriptionPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal YellowstonePathology.YpiConnect.Client.OrderEntry.SpecimenDescriptionPage OrderEntryControl;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ContentGrid;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid StepsGrid;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridPageContent;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxSpecimenDescription;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxSpecialInstructions;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxCollectionDate;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxOrderType;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckBoxOrderFrozenSection;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxCallbackNumber;
        
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
            System.Uri resourceLocater = new System.Uri("/YellowstonePathology.YpiConnect.Client;component/orderentry/specimendescriptionp" +
                    "age.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
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
            this.OrderEntryControl = ((YellowstonePathology.YpiConnect.Client.OrderEntry.SpecimenDescriptionPage)(target));
            return;
            case 2:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.ContentGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.StepsGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.GridPageContent = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.TextBoxSpecimenDescription = ((System.Windows.Controls.TextBox)(target));
            
            #line 88 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
            this.TextBoxSpecimenDescription.Loaded += new System.Windows.RoutedEventHandler(this.TextBoxSpecimenDescription_Loaded);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ComboBoxSpecialInstructions = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.TextBoxCollectionDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.ListBoxOrderType = ((System.Windows.Controls.ListBox)(target));
            
            #line 108 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
            this.ListBoxOrderType.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBoxOrderType_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.CheckBoxOrderFrozenSection = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 11:
            this.TextBoxCallbackNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            
            #line 144 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonBack_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 145 "..\..\..\OrderEntry\SpecimenDescriptionPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonNext_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

