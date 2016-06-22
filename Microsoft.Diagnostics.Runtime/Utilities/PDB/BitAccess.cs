// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text;
using System.IO;

namespace Microsoft.Diagnostics.Runtime.Utilities.Pdb
{
    internal class BitAccess
    {
        internal BitAccess(int capacity)
        {
            _buffer = new byte[capacity];
        }

        internal byte[] Buffer
        {
            get { return _buffer; }
        }
        private byte[] _buffer;

        internal void FillBuffer(Stream stream, int capacity)
        {
            MinCapacity(capacity);
            stream.Read(_buffer, 0, capacity);
            _offset = 0;
        }

        internal void Append(Stream stream, int count)
        {
            int newCapacity = _offset + count;
            if (_buffer.Length < newCapacity)
            {
                byte[] newBuffer = new byte[newCapacity];
                Array.Copy(_buffer, newBuffer, _buffer.Length);
                _buffer = newBuffer;
            }
            stream.Read(_buffer, _offset, count);
            _offset += count;
        }

        internal int Position
        {
            get { return _offset; }
            set { _offset = value; }
        }
        private int _offset;

        internal void MinCapacity(int capacity)
        {
            if (_buffer.Length < capacity)
            {
                _buffer = new byte[capacity];
            }
            _offset = 0;
        }

        internal void Align(int alignment)
        {
            while ((_offset % alignment) != 0)
            {
                _offset++;
            }
        }

        internal void ReadInt16(out short value)
        {
            value = (short)((_buffer[_offset + 0] & 0xFF) |
                                    (_buffer[_offset + 1] << 8));
            _offset += 2;
        }

        internal void ReadInt8(out sbyte value)
        {
            value = (sbyte)_buffer[_offset];
            _offset += 1;
        }

        internal void ReadInt32(out int value)
        {
            value = (int)((_buffer[_offset + 0] & 0xFF) |
                                (_buffer[_offset + 1] << 8) |
                                (_buffer[_offset + 2] << 16) |
                                (_buffer[_offset + 3] << 24));
            _offset += 4;
        }

        internal void ReadInt64(out long value)
        {
            value = (long)(((ulong)_buffer[_offset + 0] & 0xFF) |
                                    ((ulong)_buffer[_offset + 1] << 8) |
                                    ((ulong)_buffer[_offset + 2] << 16) |
                                    ((ulong)_buffer[_offset + 3] << 24) |
                                    ((ulong)_buffer[_offset + 4] << 32) |
                                    ((ulong)_buffer[_offset + 5] << 40) |
                                    ((ulong)_buffer[_offset + 6] << 48) |
                                    ((ulong)_buffer[_offset + 7] << 56));
            _offset += 8;
        }

        internal void ReadUInt16(out ushort value)
        {
            value = (ushort)((_buffer[_offset + 0] & 0xFF) |
                                    (_buffer[_offset + 1] << 8));
            _offset += 2;
        }

        internal void ReadUInt8(out byte value)
        {
            value = (byte)((_buffer[_offset + 0] & 0xFF));
            _offset += 1;
        }

        internal void ReadUInt32(out uint value)
        {
            value = (uint)((_buffer[_offset + 0] & 0xFF) |
                                    (_buffer[_offset + 1] << 8) |
                                    (_buffer[_offset + 2] << 16) |
                                    (_buffer[_offset + 3] << 24));
            _offset += 4;
        }

        internal void ReadUInt64(out ulong value)
        {
            value = (ulong)(((ulong)_buffer[_offset + 0] & 0xFF) |
                                    ((ulong)_buffer[_offset + 1] << 8) |
                                    ((ulong)_buffer[_offset + 2] << 16) |
                                    ((ulong)_buffer[_offset + 3] << 24) |
                                    ((ulong)_buffer[_offset + 4] << 32) |
                                    ((ulong)_buffer[_offset + 5] << 40) |
                                    ((ulong)_buffer[_offset + 6] << 48) |
                                    ((ulong)_buffer[_offset + 7] << 56));
            _offset += 8;
        }

        internal void ReadInt32(int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                ReadInt32(out values[i]);
            }
        }

        internal void ReadUInt32(uint[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                ReadUInt32(out values[i]);
            }
        }

        internal void ReadBytes(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = _buffer[_offset++];
            }
        }

        internal float ReadFloat()
        {
            float result = BitConverter.ToSingle(_buffer, _offset);
            _offset += 4;
            return result;
        }

        internal double ReadDouble()
        {
            double result = BitConverter.ToDouble(_buffer, _offset);
            _offset += 8;
            return result;
        }

        internal decimal ReadDecimal()
        {
            int[] bits = new int[4];
            this.ReadInt32(bits);
            return new decimal(bits[2], bits[3], bits[1], bits[0] < 0, (byte)((bits[0] & 0x00FF0000) >> 16));
        }

        internal void ReadBString(out string value)
        {
            ushort len;
            this.ReadUInt16(out len);
            value = Encoding.UTF8.GetString(_buffer, _offset, len);
            _offset += len;
        }

        internal void ReadCString(out string value)
        {
            int len = 0;
            while (_offset + len < _buffer.Length && _buffer[_offset + len] != 0)
            {
                len++;
            }
            value = Encoding.UTF8.GetString(_buffer, _offset, len);
            _offset += len + 1;
        }

        internal void SkipCString(out string value)
        {
            int len = 0;
            while (_offset + len < _buffer.Length && _buffer[_offset + len] != 0)
            {
                len++;
            }
            _offset += len + 1;
            value = null;
        }

        internal void ReadGuid(out Guid guid)
        {
            uint a;
            ushort b;
            ushort c;
            byte d;
            byte e;
            byte f;
            byte g;
            byte h;
            byte i;
            byte j;
            byte k;

            ReadUInt32(out a);
            ReadUInt16(out b);
            ReadUInt16(out c);
            ReadUInt8(out d);
            ReadUInt8(out e);
            ReadUInt8(out f);
            ReadUInt8(out g);
            ReadUInt8(out h);
            ReadUInt8(out i);
            ReadUInt8(out j);
            ReadUInt8(out k);

            guid = new Guid(a, b, c, d, e, f, g, h, i, j, k);
        }

        internal string ReadString()
        {
            int len = 0;
            while (_offset + len < _buffer.Length && _buffer[_offset + len] != 0)
            {
                len += 2;
            }
            string result = Encoding.Unicode.GetString(_buffer, _offset, len);
            _offset += len + 2;
            return result;
        }
    }
}
