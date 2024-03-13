namespace SearcherOfFiles.Components
{
    /// <summary>
    /// Описание расширения класса TreeView 
    /// </summary>
    public class TreeViewExt : TreeView
    {
        public TreeViewExt()
        {
            SetStyle(ControlStyles.UserPaint |  ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        /// <summary>
        /// Реализация перекрытия родительского метода OnPaint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint))
            {
                // создать новое сообщение
                Message m = new Message();

                // дескриптор содержимого
                m.HWnd = this.Handle;

                // само сообщение
                m.Msg = 0x0318; // WM_PRINTCLIENT сообщение

                // параметры
                m.WParam = e.Graphics.GetHdc();
                m.LParam = (IntPtr)4; // PRF_CLIENT message

                // отправить это сообщение
                DefWndProc(ref m);

                // Освобождает дескриптор контекста устройства, полученный предыдущим вызовом метода GetHdc
                e.Graphics.ReleaseHdc(m.WParam);
            }

            // выполним базовый метод
            base.OnPaint(e);
        }

    }
}
