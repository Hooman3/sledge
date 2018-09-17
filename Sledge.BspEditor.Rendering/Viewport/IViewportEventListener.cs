namespace Sledge.BspEditor.Rendering.Viewport
{
    public interface IViewportEventListener
    {
        string OrderHint { get; }
        MapViewport Viewport { get; set; }

        bool IsActive();

        void KeyUp(ViewportEvent e);
        void KeyDown(ViewportEvent e);

        void MouseMove(ViewportEvent e);
        void MouseWheel(ViewportEvent e);
        void MouseUp(ViewportEvent e);
        void MouseDown(ViewportEvent e);
        void MouseClick(ViewportEvent e);
        void MouseDoubleClick(ViewportEvent e);

        void DragStart(ViewportEvent e);
        void DragMove(ViewportEvent e);
        void DragEnd(ViewportEvent e);

        void MouseEnter(ViewportEvent e);
        void MouseLeave(ViewportEvent e);

        void ZoomChanged(ViewportEvent e);
        void PositionChanged(ViewportEvent e);

        void UpdateFrame(long frame);
    }
}