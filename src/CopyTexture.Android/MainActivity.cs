using Android.App;
using Android.OS;
using Veldrid;
using Android.Content.PM;

namespace CopyTexture.Android
{
    [Activity(
        MainLauncher = true,
        Label = "Sample Text",
        ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation | ConfigChanges.ScreenSize
        )]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            var options = new GraphicsDeviceOptions(true, PixelFormat.R16_UNorm, true);
            var device = GraphicsDevice.CreateVulkan(options);

            var factory = device.ResourceFactory;
            const uint texSize = 512;
            var src = factory.CreateTexture(TextureDescription.Texture2D(
                texSize, texSize, 1, 1, PixelFormat.R8_G8_B8_A8_UNorm, TextureUsage.Sampled));
            var dst = factory.CreateTexture(TextureDescription.Texture2D(
                texSize, texSize, 1, 1, PixelFormat.R8_G8_B8_A8_UNorm, TextureUsage.Sampled));

            var cl = factory.CreateCommandList();
            cl.Begin();
            cl.CopyTexture(src, dst);
            cl.End();

            device.SubmitCommands(cl);
            device.WaitForIdle();

            cl.Dispose();
            src.Dispose();
            dst.Dispose();
            device.Dispose();
        }
    }
}
