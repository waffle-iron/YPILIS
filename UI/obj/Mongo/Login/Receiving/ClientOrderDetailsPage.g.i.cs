﻿#pragma checksum "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C471096D159C0F0E345E9D4301DFBFDE"
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
using YellowstonePathology.UI.Converter;
using YellowstonePathology.UI.CustomControls;
using YellowstonePathology.UI.ValidationRules;


namespace YellowstonePathology.UI.Login.Receiving {
    
    
    /// <summary>
    /// ClientOrderDetailsPage
    /// </summary>
    public partial class ClientOrderDetailsPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 70 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxOrderedAs;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxSpecimenId;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxAccessionAs;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxSpecimenSource;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxCollectionDate;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxContainerId;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckBoxAccession;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonBack;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
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
            System.Uri resourceLocater = new System.Uri("/UserInterface;component/login/receiving/clientorderdetailspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
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
            this.TextBoxOrderedAs = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.ComboBoxSpecimenId = ((System.Windows.Controls.ComboBox)(target));
            
            #line 74 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
            this.ComboBoxSpecimenId.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxSpecimenId_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TextBoxAccessionAs = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TextBoxSpecimenSource = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.TextBoxCollectionDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.TextBoxContainerId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 93 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClearContainerId_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.CheckBoxAccession = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            
            #line 110 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetailFresh_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 113 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetailFormalin_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 116 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetailBPlus_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 119 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetailCytolyte_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 122 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetail95IPA_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 125 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetailNotApplicable_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.ButtonBack = ((System.Windows.Controls.Button)(target));
            
            #line 150 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
            this.ButtonBack.Click += new System.Windows.RoutedEventHandler(this.ButtonBack_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.ButtonNext = ((System.Windows.Controls.Button)(target));
            
            #line 151 "..\..\..\..\Login\Receiving\ClientOrderDetailsPage.xaml"
            this.ButtonNext.Click += new System.Windows.RoutedEventHandler(this.ButtonNext_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

