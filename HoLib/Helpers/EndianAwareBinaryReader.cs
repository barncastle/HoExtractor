using HoLib.Models;
using HoLib.Static;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HoLib.Helpers
{
    public sealed class EndianAwareBinaryReader : IDisposable
    {
        public bool IsBigEndian { get; set; }
        public Stream BaseStream { get; }

        private readonly BinaryReader Reader;
        private readonly byte[] Buffer;

        public EndianAwareBinaryReader(Stream stream)
        {
            BaseStream = stream;
            Reader = new BinaryReader(stream);
            Buffer = new byte[8];
        }

        public long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        public int PeekChar()
        {
            return Reader.PeekChar();
        }

        public byte[] ReadBytes(int count)
        {
            return Reader.ReadBytes(count);
        }

        public short ReadInt16()
        {
            BaseStream.Read(Buffer, 0, 2);

            if (IsBigEndian)
                return BinaryPrimitives.ReadInt16BigEndian(Buffer);
            else
                return BinaryPrimitives.ReadInt16LittleEndian(Buffer);
        }

        public ushort ReadUInt16()
        {
            return unchecked((ushort)ReadInt16());
        }

        public int ReadInt32()
        {
            BaseStream.Read(Buffer, 0, 4);

            if (IsBigEndian)
                return BinaryPrimitives.ReadInt32BigEndian(Buffer);
            else
                return BinaryPrimitives.ReadInt32LittleEndian(Buffer);
        }

        public uint ReadUInt32()
        {
            return unchecked((uint)ReadInt32());
        }

        public long ReadInt64()
        {
            BaseStream.Read(Buffer, 0, 8);

            if (IsBigEndian)
                return BinaryPrimitives.ReadInt64BigEndian(Buffer);
            else
                return BinaryPrimitives.ReadInt64LittleEndian(Buffer);
        }

        public ulong ReadUInt64()
        {
            return unchecked((ulong)ReadInt64());
        }

        public string ReadString(int length)
        {
            return Encoding.Latin1.GetString(Reader.ReadBytes(length));
        }

        public string ReadCString()
        {
            byte b;
            var sb = new List<byte>(0x40);

            while ((b = Reader.ReadByte()) != 0)
                sb.Add(b);

            return Encoding.Latin1.GetString(sb.ToArray());
        }

        public Flags ReadFlags()
        {
            // flags are always BE
            BaseStream.Read(Buffer, 0, 4);
            return BinaryPrimitives.ReadUInt32BigEndian(Buffer);
        }

        public BlockType AssertTag(params BlockType[] token)
        {
            var pos = BaseStream.Position;
            var tag = (BlockType)Reader.ReadInt32();

            if(token.Length == 1 && token[0] != tag)
                throw new Exception($"Expected {token[0]} at {pos}");
            else if (Array.IndexOf(token, tag) == -1)
                throw new Exception($"Expected {string.Join("/", token)} at {pos}");

            return tag;
        }

        public void Dispose()
        {
            Reader.Dispose();
        }
    }
}
