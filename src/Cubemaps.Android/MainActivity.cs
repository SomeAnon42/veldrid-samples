using Android.App;
using Android.OS;
using Veldrid;
using Android.Content.PM;

namespace Cubemaps.Android
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

            var options = new GraphicsDeviceOptions(true, PixelFormat.R16_Float, true);
            var device = GraphicsDevice.CreateVulkan(options);

            var factory = device.ResourceFactory;
            const uint texSize = 512;
            var cubemap = factory.CreateTexture(TextureDescription.Texture2D(
                texSize, texSize, 1, 1, PixelFormat.R8_G8_B8_A8_UNorm, TextureUsage.Cubemap));

            // You can initialize the texture with some data, but it won't make a difference here.
            var sampled = factory.CreateTexture(TextureDescription.Texture2D(
                texSize, texSize, 1, 1, PixelFormat.B8_G8_R8_A8_UNorm, TextureUsage.Sampled));

            var cl = factory.CreateCommandList();
            cl.Begin();
            cl.CopyTexture(sampled, 0, 0, 0, 0, 0, cubemap, 0, 0, 0, 0, 0, texSize, texSize, 0, 1);
            cl.End();

            device.SubmitCommands(cl);
            device.WaitForIdle();

            cl.Dispose();
            cubemap.Dispose();
            sampled.Dispose();
            device.Dispose();
        }
    }
}
