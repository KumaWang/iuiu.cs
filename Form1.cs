using engine.framework.graphics;
using System.Numerics;
using System.Windows.Forms;

namespace engine.framework
{
    public partial class Form1 : Form
    {
        private GLRenderer renderer;

        public Form1()
        {
            InitializeComponent();

            renderer = new GLRenderer(this.Handle, () => this.ClientSize);
            renderer.Paint += Renderer_OnPaint;
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            renderer.Frame();
        }

        private void Renderer_OnPaint(GLRenderer render)
        {
            //render.GraphicsDevice.Context.swapBuffers();
            //render.GraphicsDevice.Clear(Color2D.CornflowerBlue);

            //render.Begin();

            render.DrawLine(new Vector2(0, 0), new Vector2(100, 100), Color.Red, 1);

            //render.End();
        }
    }
}
