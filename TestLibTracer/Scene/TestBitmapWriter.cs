using System.IO;
using libTracer.Common;
using libTracer.Scene;
using NUnit.Framework;

namespace TestLibTracer.Scene
{
    /// <summary>
    /// More of an integration test for now
    /// </summary>
    internal class TestBitmapWriter
    {
        private const string SOME_PATH = "SomeFile.png";

        [SetUp]
        public void Setup()
        {
            if (File.Exists(SOME_PATH))
            {
                File.Delete(SOME_PATH);
            }
        }

        [TearDown]
        public void Teardown()
        {
            if (File.Exists(SOME_PATH))
            {
                //File.Delete(SOME_PATH);
            }
        }

        [Test]
        public void SaveToBitmap_WritesFile_ToPath()
        {
            var canvas = new Canvas(10, 20);
            var write = new BitmapWriter();

            write.SaveToBitmap(canvas, SOME_PATH);

            Assert.That(File.Exists(SOME_PATH));
        }

        [Test]
        public void SaveToBitmap_WritesFileToPath_WhenColourChannelLessThanZero()
        {
            var canvas = new Canvas(10, 20);
            canvas.SetPixel(0, 0, new TColour(-1, 0, 0));
            var write = new BitmapWriter();

            write.SaveToBitmap(canvas, SOME_PATH);

            Assert.That(File.Exists(SOME_PATH));
        }

        [Test]
        public void SaveToBitmap_WritesFileToPath_WhenColourChannelGreaterThan255()
        {
            var canvas = new Canvas(10, 20);
            canvas.SetPixel(0, 0, new TColour(0, 999, 0));
            var write = new BitmapWriter();

            write.SaveToBitmap(canvas, SOME_PATH);

            Assert.That(File.Exists(SOME_PATH));
        }
    }
}
