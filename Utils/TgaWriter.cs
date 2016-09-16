using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class TgaWriter : IDisposable
    {
        private FileStream _file;
        private BinaryWriter _writer;
        private bool _isDisposed = false;

        private readonly byte[] _header;

        public TgaWriter(string path, int height, int width)
        {
            if (string.IsNullOrWhiteSpace(path)) { throw new ArgumentNullException(nameof(path)); }

            _file = File.Open(path, FileMode.OpenOrCreate);
            _writer = new BinaryWriter(_file);
            _header = new byte[] {
            0, // ID length
            0, // no color map
            2, // uncompressed, true color
            0, 0, 0, 0,
            0,
            0, 0, 0, 0, // x and y origin
            (byte)(width & 0x00FF),
            (byte)((width & 0xFF00) >> 8),
            (byte)(height & 0x00FF),
            (byte)((height & 0xFF00) >> 8),
            32, // 32 bit bitmap
            0 };
            _writer.Write(_header);
        }

        public void WritePixel(Color c)
        {
            WriteByte(c.R);
            WriteByte(c.G);
            WriteByte(c.B);
            WriteByte(c.A);
        }

        public void WriteByte(byte b)
        {
            _writer.Write(b);
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _file.Dispose();
                _writer.Dispose();
            }
        }
    }
}
