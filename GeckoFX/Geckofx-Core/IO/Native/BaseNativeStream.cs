using System;
using System.Runtime.InteropServices;

namespace Gecko.IO.Native
{
	/// <summary>
	/// Base Native Gecko Stream implementation
	/// 
	/// </summary>
	public abstract unsafe class BaseNativeStream
	{
		private IntPtr _buffer;
		private byte* _currentPointer;
		private int _length;
		private uint _position;

		internal BaseNativeStream()
		{
			
		}

		internal IntPtr Buffer
		{
			get { return _buffer; }
		}

		internal byte* CurrentPointer
		{
			get { return _currentPointer; }
		}

		public int Length
		{
			get { return _length; }
		}

		public uint Position
		{
			get { return _position; }
		}

		/// <summary>
		/// Inits internal pointer and move it to MIN(current position,<paramref name="length"/>)
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="length"></param>
		internal void Init(IntPtr buffer, int length)
		{
			_buffer = buffer;
			_length = length;
			_position =(uint) Math.Min( _position, _length );
			_currentPointer = ((byte*)_buffer.ToPointer()) + _position;
		}

		/// <summary>
		/// Internal function
		/// Moves internal pointer forward to <paramref name="count"/> bytes
		/// WARNING: count must be checked before doing this
		/// </summary>
		/// <param name="count"></param>
		internal void MovePointer(uint count)
		{
			_currentPointer+= count;
			_position += count;
		}

		/// <summary>
		/// Reads all data from internal memory from 0 to Position
		/// </summary>
		/// <returns></returns>
		public byte[] ReadData()
		{
			byte[] ret = new byte[_position];
			Marshal.Copy(_buffer, ret, 0, (int)_position);
			return ret;
		}
		
	}
}