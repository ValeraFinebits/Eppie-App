using Uno.UI.Runtime.Skia;

namespace Eppie.App
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var host = SkiaHostBuilder.Create()
                .App(() => new Eppie.App.Shared.App())
                .UseX11()
                .UseLinuxFrameBuffer()
                .UseMacOS()
                .UseWindows()
                .Build();

            host.Run();
        }
    }
}
