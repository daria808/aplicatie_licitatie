﻿#pragma checksum "..\..\..\Views\PasswordChangeDialog.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "587E1AD75A893CB3F1CCA6361E33755AE4212CD0F6CA7639A4653F13717DD1E1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Client_ADBD.Views;
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


namespace Client_ADBD.Views {
    
    
    /// <summary>
    /// PasswordChangeDialog
    /// </summary>
    public partial class PasswordChangeDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Views\PasswordChangeDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox NewPasswordBox;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Views\PasswordChangeDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox ConfirmPasswordBox;
        
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
            System.Uri resourceLocater = new System.Uri("/Client_ADBD;component/views/passwordchangedialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\PasswordChangeDialog.xaml"
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
            this.NewPasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 14 "..\..\..\Views\PasswordChangeDialog.xaml"
            this.NewPasswordBox.PasswordChanged += new System.Windows.RoutedEventHandler(this.NewPasswordBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ConfirmPasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 20 "..\..\..\Views\PasswordChangeDialog.xaml"
            this.ConfirmPasswordBox.PasswordChanged += new System.Windows.RoutedEventHandler(this.ConfirmPasswordBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

