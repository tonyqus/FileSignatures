using System.IO;
using System.Reflection;
using Xunit;

namespace FileSignatures.Tests
{
    public class FunctionalTests
    {
        [Theory]
        [InlineData("test.bmp", "image/bmp")]
        [InlineData("test.doc", "application/msword")]
        [InlineData("test.docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        [InlineData("test.docm", "application/vnd.ms-word.document.macroEnabled.12")]
        [InlineData("test.exe", "application/vnd.microsoft.portable-executable")]
        [InlineData("test.gif", "image/gif")]
        [InlineData("test.jfif", "image/jpeg")]
        [InlineData("test.exif", "image/jpeg")]
        [InlineData("saved.msg", "application/vnd.ms-outlook")]
        [InlineData("dragndrop.msg", "application/vnd.ms-outlook")]
        [InlineData("nonstandard.docx","application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        [InlineData("test.pdf", "application/pdf")]
        [InlineData("test_header_somewhere_in_1024_first_bytes.pdf", "application/pdf")]
        [InlineData("test_header_adobe.pdf", "application/pdf")]
        [InlineData("test.rtf", "application/rtf")]
        [InlineData("test.png", "image/png")]
        [InlineData("test.ppt", "application/vnd.ms-powerpoint")]
        [InlineData("test2.ppt", "application/vnd.ms-powerpoint")]
        [InlineData("test.pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation")]
        [InlineData("test.pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12")]
        [InlineData("test.spiff", "image/jpeg")]
        [InlineData("test.tif", "image/tiff")]
        [InlineData("test.xls", "application/vnd.ms-excel")]
        [InlineData("test.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [InlineData("test.xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12")]
        [InlineData("test.xps", "application/vnd.ms-xpsdocument")]
        [InlineData("test.zip", "application/zip")]
        [InlineData("test.dcm", "application/dicom")]
        [InlineData("test.odt", "application/vnd.oasis.opendocument.text")]
        [InlineData("test.ods", "application/vnd.oasis.opendocument.spreadsheet")]
        [InlineData("test.odp", "application/vnd.oasis.opendocument.presentation")]
        [InlineData("test.vsd", "application/vnd.visio")]
        [InlineData("test.vsdx", "application/vnd.visio")]
        [InlineData("test.webp", "image/webp")]
        [InlineData("test.mp4", "video/mp4")]
        [InlineData("test-v1.mp4", "video/mp4")]
        [InlineData("test.m4v", "video/mp4")]
        [InlineData("test.m4a", "audio/mp4")]
        [InlineData("test.mov", "video/quicktime")]
        [InlineData("test.3gp", "video/3gpp")]
        [InlineData("test.vcf", "text/vcard")]
        [InlineData("test.mp3", "audio/mpeg")]
        [InlineData("test.ogg", "audio/ogg")]
        [InlineData("test.amr", "audio/amr")]
        [InlineData("test.ico", "image/vnd.microsoft.icon")]
        public void SamplesAreRecognised(string sample, string expected)
        {
            var result = InspectSample(sample);

            Assert.NotNull(result);
            Assert.Equal(expected, result?.MediaType);
        }

        private static FileFormat InspectSample(string fileName)
        {
            var inspector = new FileFormatInspector();
            var buildDirectoryPath = Path.GetDirectoryName(typeof(FunctionalTests).GetTypeInfo().Assembly.Location);
            var sample = new FileInfo(Path.Combine(buildDirectoryPath, "Samples", fileName));
            FileFormat result;

            using (var stream = sample.OpenRead())
            {
                result = inspector.DetermineFileFormat(stream);
            }

            return result;
        }
    }
}
