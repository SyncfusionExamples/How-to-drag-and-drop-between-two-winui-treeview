# How to Drag and Drop Between Two WinUI TreeView?

This example describes how to drag and drop between two [WinUI TreeView](https://www.syncfusion.com/winui-controls/treeview) (SfTreeView).

You can customize the dragging operation between two treeview by using the [SfTreeView.ItemDragStarting](https://help.syncfusion.com/cr/winui/Syncfusion.UI.Xaml.TreeView.SfTreeView.html#Syncfusion_UI_Xaml_TreeView_SfTreeView_ItemDragStarting), [SfTreeView.ItemDropping](https://help.syncfusion.com/cr/winui/Syncfusion.UI.Xaml.TreeView.SfTreeView.html#Syncfusion_UI_Xaml_TreeView_SfTreeView_ItemDropping) and [SfTreeView.ItemDropped](https://help.syncfusion.com/cr/winui/Syncfusion.UI.Xaml.TreeView.SfTreeView.html#Syncfusion_UI_Xaml_TreeView_SfTreeView_ItemDropped) events.

``` c#
AssociatedObject.sfTreeView1.ItemDragStarting += SfTreeView1_ItemDragStarting;    
AssociatedObject.sfTreeView1.ItemDropping += SfTreeView1_ItemDropping;
AssociatedObject.sfTreeView2.ItemDropping += SfTreeView2_ItemDropping;
AssociatedObject.sfTreeView2.ItemDropped += SfTreeView1_ItemDropped;
    
/// <summary>
/// Customizing the ItemStarting event
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void SfTreeView1_ItemDragStarting(object sender, Syncfusion.UI.Xaml.TreeView.TreeViewItemDragStartingEventArgs e)
{
    //Restrict the dragging for certain node
    var record = e.DraggingNodes[0].Content as Model;
    if (record.Header == "Feature Schedule")
        e.Cancel = true;
}

/// <summary>
/// Customizing the ItemDropping event
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void SfTreeView1_ItemDropping(object sender, Syncfusion.UI.Xaml.TreeView.TreeViewItemDroppingEventArgs e)
{
    //Restrict the dropping in first treeview
    e.Handled = true;
}

/// <summary>
/// Customizing the ItemDropping event
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void SfTreeView2_ItemDropping(object sender, Syncfusion.UI.Xaml.TreeView.TreeViewItemDroppingEventArgs e)
{
    //Restrict the dropping for drop position as above
    if (e.DropPosition == Syncfusion.UI.Xaml.TreeView.DropPosition.DropAbove)
        e.Handled = true;

    //Restrict the dropping on certain nodes
    var record = e.TargetNode.Content as Model;
    if (record.Header == "My Folders")
        e.Handled = true;
}

/// <summary>
/// Customize the ItemDropped event
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void SfTreeView1_ItemDropped(object sender, Syncfusion.UI.Xaml.TreeView.TreeViewItemDroppedEventArgs e)
{
    var parentNode = e.TargetNode.ParentNode;
    var collection = parentNode.ChildNodes;
    var record = e.DraggingNodes[0].Content as Model;
    int count = 0;
    foreach (var child in parentNode.ChildNodes)
    {
        var childNode = child.Content as Model;
        if (childNode.Header == record.Header)
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
```

![Drag and drop item between two TreeView](DragAndDrop.png)