using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HeifConvert
{
    public class Converter
    {
        public void Start(string path, bool includeSubfolder, bool overwrite, bool deleteSource)
        {
            if (includeSubfolder)
            {
                var subDirectories = Directory.GetDirectories(path);
                if (subDirectories?.Length > 0)
                {
                    foreach (var subDirectory in subDirectories)
                        Start(subDirectory, includeSubfolder, overwrite, deleteSource);
                }
            }
            Parallel.ForEach(Directory.GetFiles(path), new ParallelOptions { MaxDegreeOfParallelism = 3 }, (file) =>
            {
                var extension = Path.GetExtension(file);
                if (extension.ToLower() == ".heic")
                {
                    var destination = Path.Combine(path, $"{Path.GetFileNameWithoutExtension(file)}.jpg");
                    if (overwrite || !File.Exists(Path.Combine(path, $"{Path.GetFileNameWithoutExtension(file)}.jpg")))
                    {
                        using (var image = new MagickImage(file))
                        {
                            image.Write(destination);
                        }
                    }
                    if (deleteSource)
                        File.Delete(file);
                }
            });
        }
    }
}
