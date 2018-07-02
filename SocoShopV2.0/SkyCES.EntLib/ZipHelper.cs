namespace SkyCES.EntLib
{
    using ICSharpCode.SharpZipLib.Checksums;
    using ICSharpCode.SharpZipLib.Zip;
    using System;
    using System.IO;

    public sealed class ZipHelper
    {
        public static void UnZip(string zipfilepath, string unzippath)
        {
            ZipEntry entry;
            ZipInputStream stream = new ZipInputStream(File.OpenRead(zipfilepath));
            while ((entry = stream.GetNextEntry()) != null)
            {
                string name = entry.Name;
                if (name != string.Empty) name = entry.Name.Substring(entry.Name.IndexOf("/"));
                string directoryName = Path.GetDirectoryName(unzippath);
                if (Path.GetFileName(name) == string.Empty) break;
                if (entry.CompressedSize == 0) break;
                Directory.CreateDirectory(Path.GetDirectoryName(unzippath + name));
                FileStream fs = File.Create(unzippath + name);
                int count = 0x800;
                byte[] buffer = new byte[0x800];
                count = stream.Read(buffer, 0, buffer.Length);
                if (count > 0)
                    fs.Write(buffer, 0, count);
                fs.Close();
            }
            stream.Close();
        }

        private static void zip(string strFile, ZipOutputStream s, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile = strFile + Path.DirectorySeparatorChar;
            Crc32 crc = new Crc32();
            string[] fileSystemEntries = Directory.GetFileSystemEntries(strFile);
            foreach (string str in fileSystemEntries)
            {
                if (Directory.Exists(str))
                    zip(str, s, staticFile);
                else
                {
                    FileStream stream = File.OpenRead(str);
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    int startIndex = staticFile.LastIndexOf(".") + 1;
                    ZipEntry entry = new ZipEntry(str.Substring(startIndex));
                    entry.DateTime = DateTime.Now;
                    entry.Size = stream.Length;
                    stream.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public static void ZipFile(string strFile, string strZip)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile = strFile + Path.DirectorySeparatorChar;
            ZipOutputStream s = new ZipOutputStream(File.Create(strZip));
            s.SetLevel(6);
            zip(strFile, s, strZip);
            s.Finish();
            s.Close();
        }
    }
}

