﻿#pragma checksum "..\..\..\View\PunishmentLogPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F33BADB84988B9B1474398ADBF24AF8B41C80AEF"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using DormitoryGUI.View;
using DormitoryGUI.ViewModel;
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


namespace DormitoryGUI.View {
    
    
    /// <summary>
    /// PunishmentLogPage
    /// </summary>
    public partial class PunishmentLogPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 375 "..\..\..\View\PunishmentLogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DormitoryGUI.View.SideMenuButton BackButton;
        
        #line default
        #line hidden
        
        
        #line 397 "..\..\..\View\PunishmentLogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchCommand;
        
        #line default
        #line hidden
        
        
        #line 416 "..\..\..\View\PunishmentLogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 471 "..\..\..\View\PunishmentLogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView StudentList;
        
        #line default
        #line hidden
        
        
        #line 517 "..\..\..\View\PunishmentLogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label StudentName;
        
        #line default
        #line hidden
        
        
        #line 520 "..\..\..\View\PunishmentLogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ClassNumber;
        
        #line default
        #line hidden
        
        
        #line 530 "..\..\..\View\PunishmentLogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TotalGoodPoint;
        
        #line default
        #line hidden
        
        
        #line 540 "..\..\..\View\PunishmentLogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TotalBadPoint;
        
        #line default
        #line hidden
        
        
        #line 550 "..\..\..\View\PunishmentLogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TotalPunishStep;
        
        #line default
        #line hidden
        
        
        #line 556 "..\..\..\View\PunishmentLogPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Timeline;
        
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
            System.Uri resourceLocater = new System.Uri("/DormitoryGUI;component/view/punishmentlogpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\PunishmentLogPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.BackButton = ((DormitoryGUI.View.SideMenuButton)(target));
            return;
            case 2:
            this.SearchCommand = ((System.Windows.Controls.TextBox)(target));
            
            #line 403 "..\..\..\View\PunishmentLogPage.xaml"
            this.SearchCommand.KeyUp += new System.Windows.Input.KeyEventHandler(this.SearchCommand_OnKeyUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 420 "..\..\..\View\PunishmentLogPage.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.SearchButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.StudentList = ((System.Windows.Controls.ListView)(target));
            
            #line 476 "..\..\..\View\PunishmentLogPage.xaml"
            this.StudentList.SizeChanged += new System.Windows.SizeChangedEventHandler(this.StudentList_SizeChanged);
            
            #line default
            #line hidden
            
            #line 478 "..\..\..\View\PunishmentLogPage.xaml"
            this.StudentList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.StudentList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.StudentName = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.ClassNumber = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.TotalGoodPoint = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.TotalBadPoint = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.TotalPunishStep = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.Timeline = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

