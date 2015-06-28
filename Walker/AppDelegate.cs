using System;

using Foundation;
using AppKit;

namespace Walker
{
    public partial class AppDelegate : NSApplicationDelegate
    {
        MainWindowController mainWindowController;

        public AppDelegate ()
        {
        }

        public override void DidFinishLaunching (NSNotification notification)
        {
            mainWindowController = new MainWindowController ();
            mainWindowController.Window.MakeKeyAndOrderFront (this);
        }

        partial void openDialog (NSObject sender)
        {
            NSOpenPanel panel = NSOpenPanel.OpenPanel;
            panel.Begin ((nint result) => {
                if (result == (int)NSPanelButtonType.Cancel) {
                    return;
                }

                Console.WriteLine ("filename: {0}", panel.Filename);
                Map.Map map = Map.Map.LoadFromFile (panel.Filename);
                ((MainWindow)mainWindowController.Window).Map = map;
            });
        }
    }
}

