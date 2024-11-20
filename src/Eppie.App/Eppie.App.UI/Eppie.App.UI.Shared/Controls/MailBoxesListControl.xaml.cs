using Tuvi.App.ViewModels;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using System;
using Microsoft.UI.Xaml.Controls;

#if WINDOWS_UWP
using Windows.UI.Xaml;
#else
using Microsoft.UI.Xaml;
#endif

namespace Tuvi.App.Shared.Controls
{
#if WINDOWS_UWP
    public sealed partial class MailBoxesListControl : Windows.UI.Xaml.Controls.UserControl
#else
    public sealed partial class MailBoxesListControl : UserControl
#endif    
    {
        public MailBoxesModel MailBoxesModel
        {
            get { return (MailBoxesModel)GetValue(MailBoxesModelProperty); }
            set { SetValue(MailBoxesModelProperty, value); }
        }
        public static readonly DependencyProperty MailBoxesModelProperty =
            DependencyProperty.Register(nameof(MailBoxesModel), typeof(MailBoxesModel), typeof(MailBoxesListControl), new PropertyMetadata(null));

        public MailBoxesListControl()
        {
            this.InitializeComponent();
        }

        private void MailBoxTreeView_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;

            if (sender is TreeView treeView)
            {
                var pointerPosition = e.GetPosition(treeView);

                var hoveredNode = GetTreeViewNodeAtPoint(treeView, pointerPosition);

                if (hoveredNode != null && hoveredNode.HasChildren)
                {
                    hoveredNode.IsExpanded = true;
                }
            }
        }

        private TreeViewNode GetTreeViewNodeAtPoint(TreeView treeView, Point position)
        {
            foreach (var item in treeView.RootNodes)
            {
                var node = FindNodeAtPoint(treeView, item, position);
                if (node != null)
                {
                    return node;
                }
            }

            return null;
        }

        private TreeViewNode FindNodeAtPoint(TreeView treeView, TreeViewNode node, Point position)
        {
            var container = treeView.ContainerFromNode(node) as TreeViewItem;

            if (container != null)
            {
                var bounds = container.TransformToVisual(treeView).TransformBounds(new Rect(0, 0, container.ActualWidth, container.ActualHeight));

                if (bounds.Contains(position))
                {
                    return node;
                }
            }

            foreach (var childNode in node.Children)
            {
                var result = FindNodeAtPoint(treeView, childNode, position);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }


        private void MailBoxTreeView_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                var deferral = e.GetDeferral();

                var targetNode = GetTreeViewNodeAtPoint(sender as TreeView, e.GetPosition(sender as TreeView));
                if (targetNode != null)
                {
                    var targetMailBoxItem = targetNode.Content as MailBoxItem;
                    MailBoxesModel.MoveMessages(targetMailBoxItem);
                }

                deferral.Complete();
            }
        }

    }
}
