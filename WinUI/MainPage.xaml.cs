using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Populating_Nodes_with_Bound_mode
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            treeView.ItemDragStarting += TreeView_ItemDragStarting;
            treeView.ItemDropping += TreeView_ItemDropping;
            treeView1.ItemDropping += TreeView1_ItemDropping;
            treeView1.ItemDropped += TreeView1_ItemDropped;
        }

        private void TreeView1_ItemDropped(object sender, Syncfusion.UI.Xaml.TreeView.TreeViewItemDroppedEventArgs e)
        {
            var parentNode = e.TargetNode.ParentNode;
            var collection = parentNode.ChildNodes;
            var record = e.DraggingNodes[0].Content as Folder;
            int count = 0;
            foreach (var child in parentNode.ChildNodes)
            {
                var childNode = child.Content as Folder;
                if (childNode.FileName == record.FileName)
                {
                    count++;
                    if (count > 1)
                    {
                        // Remove dropped node if the parent has the same node in it
                        collection.Remove(child);
                        return;
                    }

                }
            }
        }

        private void TreeView1_ItemDropping(object sender, Syncfusion.UI.Xaml.TreeView.TreeViewItemDroppingEventArgs e)
        {
            if (e.DropPosition == Syncfusion.UI.Xaml.TreeView.DropPosition.DropAbove)
                e.Handled = true;

            var record = e.TargetNode.Content as Folder;
            if (record.FileName == "Documents")
                e.Handled = true;
        }

        private void TreeView_ItemDragStarting(object sender, Syncfusion.UI.Xaml.TreeView.TreeViewItemDragStartingEventArgs e)
        {
            var record = e.DraggingNodes[0].Content as Folder;
            if (record.FileName == "Downloads")
                e.Cancel = true;
        }

        private void TreeView_ItemDropping(object sender, Syncfusion.UI.Xaml.TreeView.TreeViewItemDroppingEventArgs e)
        {
            e.Handled = true;
        }
    }
}
