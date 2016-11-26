using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using FootballManager.App.Extensions;
using FootballManager.App.Utility;
using FootballManager.App.View;
using Dragablz;

namespace FootballManager.App.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private ObservableCollection<Player> players;
        private PlayerService playerService;
        private TeamService teamService;
        private ICommand openPlayerAddViewCommand;
        private ICommand playerInfoCommand;

        public PlayerViewModel()
        {
            this.playerService = new PlayerService();
            this.teamService = new TeamService();

            LoadData();     

            Messenger.Default.Register<Player>(this, OnPlayerObjReceived);
        }



        #region Properties
        public ICommand OpenPlayerAddViewCommand
        {
            get
            {
                if (openPlayerAddViewCommand == null)
                {
                    openPlayerAddViewCommand = new RelayCommand(OpenPlayerAddView);
                }
                return openPlayerAddViewCommand;
            }
        }

        public ICommand PlayerInfoCommand
        {
            get
            {
                if (playerInfoCommand == null)
                {
                    playerInfoCommand = new RelayCommand(PlayerInfo);
                }
                return playerInfoCommand;
            }
        }

        public ObservableCollection<Player> Players
        {
            get { return players; }
            set
            {
                players = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods

        private void OpenPlayerAddView(object obj)
        {
            var playerAddView = new PlayerAddView();
            playerAddView.ShowDialog();
        }

        private void PlayerInfo(object obj)
        {
            TabablzControl playerViewTabablzControl = (TabablzControl)obj;
            playerViewTabablzControl.SelectedIndex = 1;
        }

        private void OnPlayerObjReceived(Player player)
        {
            players.Add(player);
        }

        public void LoadData()
        {
            Players = playerService.GetAll().ToObservableCollection();
        }

        

//        at FootballManager.App.ViewModel.PlayerViewModel.PlayerInfo(Object sender) in C:\Users\Jimmy\Desktop\IT-Högskolan\Kurser\Programmering med.NET och C_sharp\Projekt\Team-Rocket\FootballManager.App\ViewModel\PlayerViewModel.cs:line 105

//at FootballManager.App.Utility.RelayCommand.Execute(Object parameter) in C:\Users\Jimmy\Desktop\IT-Högskolan\Kurser\Programmering med.NET och C_sharp\Projekt\Team-Rocket\FootballManager.App\Utility\RelayCommand.cs:line 32

//at MS.Internal.Commands.CommandHelpers.CriticalExecuteCommandSource(ICommandSource commandSource, Boolean userInitiated)
//   at System.Windows.Controls.Primitives.ButtonBase.OnClick()
//   at System.Windows.Controls.Button.OnClick()
//   at System.Windows.Controls.Primitives.ButtonBase.OnMouseLeftButtonUp(MouseButtonEventArgs e)
//   at System.Windows.UIElement.OnMouseLeftButtonUpThunk(Object sender, MouseButtonEventArgs e)
//   at System.Windows.Input.MouseButtonEventArgs.InvokeEventHandler(Delegate genericHandler, Object genericTarget)
//   at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)
//   at System.Windows.RoutedEventHandlerInfo.InvokeHandler(Object target, RoutedEventArgs routedEventArgs)
//   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised)
//   at System.Windows.UIElement.ReRaiseEventAs(DependencyObject sender, RoutedEventArgs args, RoutedEvent newEvent)
//   at System.Windows.UIElement.OnMouseUpThunk(Object sender, MouseButtonEventArgs e)
//   at System.Windows.Input.MouseButtonEventArgs.InvokeEventHandler(Delegate genericHandler, Object genericTarget)
//   at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)
//   at System.Windows.RoutedEventHandlerInfo.InvokeHandler(Object target, RoutedEventArgs routedEventArgs)
//   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised)
//   at System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args)
//   at System.Windows.UIElement.RaiseTrustedEvent(RoutedEventArgs args)
//   at System.Windows.UIElement.RaiseEvent(RoutedEventArgs args, Boolean trusted)
//   at System.Windows.Input.InputManager.ProcessStagingArea()
//   at System.Windows.Input.InputManager.ProcessInput(InputEventArgs input)
//   at System.Windows.Input.InputProviderSite.ReportInput(InputReport inputReport)
//   at System.Windows.Interop.HwndMouseInputProvider.ReportInput(IntPtr hwnd, InputMode mode, Int32 timestamp, RawMouseActions actions, Int32 x, Int32 y, Int32 wheel)
//   at System.Windows.Interop.HwndMouseInputProvider.FilterMessage(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
//   at System.Windows.Interop.HwndSource.InputFilterMessage(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
//   at MS.Win32.HwndWrapper.WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
//   at MS.Win32.HwndSubclass.DispatcherCallbackOperation(Object o)
//   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
//   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)
//   at System.Windows.Threading.Dispatcher.LegacyInvokeImpl(DispatcherPriority priority, TimeSpan timeout, Delegate method, Object args, Int32 numArgs)
//   at MS.Win32.HwndSubclass.SubclassWndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam)
//   at MS.Win32.UnsafeNativeMethods.DispatchMessage(MSG& msg)
//   at System.Windows.Threading.Dispatcher.PushFrameImpl(DispatcherFrame frame)
//   at System.Windows.Threading.Dispatcher.PushFrame(DispatcherFrame frame)
//   at System.Windows.Application.RunDispatcher(Object ignore)
//   at System.Windows.Application.RunInternal(Window window)
//   at System.Windows.Application.Run(Window window)
//   at System.Windows.Application.Run()
//   at FootballManager.App.App.Main()
//   at System.AppDomain._nExecuteAssembly(RuntimeAssembly assembly, String[] args)
//   at System.AppDomain.ExecuteAssembly(String assemblyFile, Evidence assemblySecurity, String[] args)
//   at Microsoft.VisualStudio.HostingProcess.HostProc.RunUsersAssembly()
//   at System.Threading.ThreadHelper.ThreadStart_Context(Object state)
//   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
//   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
//   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
//   at System.Threading.ThreadHelper.ThreadStart()

        #endregion

        #region Combobox population
        public IEnumerable<PlayerPosition> PlayerPositions
        {
            get { return Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>(); }
        }

        public IEnumerable<PlayerStatus> PlayerStatuses
        {
            get { return Enum.GetValues(typeof(PlayerStatus)).Cast<PlayerStatus>(); }

        }

        public IEnumerable<string> TeamNames
        {
            get { return teamService.GetAll().Select(x => x.Name.Value); } 
        }
        #endregion
    }
}
