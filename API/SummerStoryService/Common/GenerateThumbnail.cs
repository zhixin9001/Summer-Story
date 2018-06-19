using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThumbnailSharp;

namespace Common
{
    public class GenerateThumbnail
    {
        private const uint thumbnailSize = 250;
        public static Stream Generate(Stream sourceStream)
        {
            Stream resultStream = null;
            try
            {
                resultStream = new ThumbnailCreator().CreateThumbnailStream(GenerateThumbnail.thumbnailSize, sourceStream, Format.Jpeg);
            }
            catch (Exception e)
            {
                resultStream = sourceStream;
            }
            return resultStream;
        }
    }
}
