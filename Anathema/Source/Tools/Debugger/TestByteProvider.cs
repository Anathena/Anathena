using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class TestByteProvider : IByteProvider
    {
        public Int64 Length
        {
            get
            {
                return Data.Length;
            }
        }

        private Byte[] Data = new Byte[256];

        public event EventHandler Changed;
        public event EventHandler LengthChanged;

        public void ApplyChanges()
        {

        }

        public bool HasChanges()
        {
            return false;
        }

        public byte ReadByte(Int64 index)
        {
            return Data[index];
        }

        public bool SupportsWriteByte()
        {
            return true;
        }

        public void WriteByte(Int64 index, Byte value)
        {
            Data[index] = value;
        }

        #region Irrelevant Features

        public void DeleteBytes(Int64 index, Int64 length) { throw new NotImplementedException(); }

        public void InsertBytes(Int64 index, Byte[] bs) { throw new NotImplementedException(); }

        public bool SupportsDeleteBytes() { return false; }

        public bool SupportsInsertBytes() { return false; }

        #endregion
    }
}
