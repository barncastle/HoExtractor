using HoLib.Models;
using HoLib.Static;
using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace HoLib.Helpers
{
    public sealed class EndianAwareBinaryWriter : IDisposable
    {
        public bool IsBigEndian { get; set; }
        public Stream BaseStream { get; }

        private readonly byte[] Buffer;

        public EndianAwareBinaryWriter(Stream stream)
        {
            BaseStream = stream;
            Buffer = new byte[8];
        }

        public long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        public void WriteBytes(byte[] value)
        {
            BaseStream.Write(value);
        }

        public void WriteBytes(byte value, int length)
        {
            if (length > 0)
            {
                var buffer = new byte[length];
                Array.Fill(buffer, value);
                BaseStream.Write(buffer);
            }
        }

        public void WriteInt16(short value)
        {
            if (IsBigEndian)
                BinaryPrimitives.WriteInt16BigEndian(Buffer, value);
            else
                BinaryPrimitives.WriteInt16LittleEndian(Buffer, value);

            BaseStream.Write(Buffer, 0, 2);
        }

        public void WriteUInt16(ushort value)
        {
            WriteInt16(unchecked((short)value));
        }

        public void WriteInt32(int value)
        {
            if (IsBigEndian)
                BinaryPrimitives.WriteInt32BigEndian(Buffer, value);
            else
                BinaryPrimitives.WriteInt32LittleEndian(Buffer, value);

            BaseStream.Write(Buffer, 0, 4);
        }

        public void WriteUInt32(uint value)
        {
            WriteInt32(unchecked((int)value));
        }

        public void WriteInt64(long value)
        {
            if (IsBigEndian)
                BinaryPrimitives.WriteInt64BigEndian(Buffer, value);
            else
                BinaryPrimitives.WriteInt64LittleEndian(Buffer, value);

            BaseStream.Write(Buffer, 0, 8);
        }

        public void WriteUInt64(ulong value)
        {
            WriteInt64(unchecked((long)value));
        }

        public void WriteString(string value, int length)
        {
            var buffer = Encoding.Latin1.GetBytes(value);

            if (buffer.Length < length)
            {
                BaseStream.Write(buffer);
                BaseStream.Position += length - buffer.Length;
            }
            else
            {
                BaseStream.Write(buffer, 0, length);
            }
        }

        public void WriteCString(string value)
        {
            BaseStream.Write(Encoding.Latin1.GetBytes(value));
            BaseStream.Position++;
        }

        public void WriteFlags(Flags value)
        {
            BinaryPrimitives.WriteUInt32BigEndian(Buffer, value);
            BaseStream.Write(Buffer, 0, 4);
        }

        public void WriteTag(BlockType tag)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(Buffer, (uint)tag);
            BaseStream.Write(Buffer, 0, 4);
        }

        public int Align(int mod)
        {
            var rem = (int)(BaseStream.Position % mod);
            if (rem != 0)
            {
                WriteBytes(0x33, mod - rem);
                return mod - rem;
            }

            return 0;
        }

        public int GetOffset(long start)
        {
            return (int)(BaseStream.Position - start);
        }

        public void Dispose()
        {
            BaseStream.Dispose();
        }
    }
}
