namespace SearcherOfFiles.Components
{
    public class TreeViewExt : TreeView
    {
        public TreeViewExt()
        {
            SetStyle(/*ControlStyles.UserPaint | */ ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);
        }

    }
}
