﻿namespace IntegrationTests
{
    using System;
    using System.IO;
    using System.Windows.Media.Imaging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OnlyM.Slides;

    [TestClass]
    public class TestSlides
    {
        [TestMethod]
        public void TestSlideCreation()
        {
            string slideFilePath = "test" + SlideFile.FileExtension;

            var fb = new SlideFileBuilder(1920, 1080);
            fb.AddSlide(GetAbsolutePath(@"TestImages\001.jpg"), true, true, true, true);
            fb.AddSlide(GetAbsolutePath(@"TestImages\002.jpg"), true, true, true, true);
            fb.AddSlide(GetAbsolutePath(@"TestImages\003.jpg"), true, true, true, true);

            fb.AddSlide(GetAbsolutePath(@"TestImages\001.jpg"), false, false, false, false);
            fb.AddSlide(GetAbsolutePath(@"TestImages\002.jpg"), false, false, false, false);
            fb.AddSlide(GetAbsolutePath(@"TestImages\003.jpg"), false, false, false, false);

            fb.Loop = true;
            fb.AutoPlay = true;
            fb.DwellTimeMilliseconds = 5000;

            fb.Build(slideFilePath, true);

            var file = new SlideFile(slideFilePath);

            for (int n = 0; n < file.SlideCount; ++n)
            {
                var slide = file.GetSlide(n);

                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(slide.Image));

                using (var fileStream = new System.IO.FileStream(slide.ArchiveEntryName, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }
        }

        private string GetAbsolutePath(string relativePath)
        {
            return Path.GetFullPath(relativePath);
        }
    }
}
