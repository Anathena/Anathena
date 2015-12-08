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
        public long Length
        {
            get
            {
                return swag.Length;
            }
        }

        private byte[] swag = new byte[256];

        public event EventHandler Changed;
        public event EventHandler LengthChanged;

        public void ApplyChanges()
        {
            
        }

        public bool HasChanges()
        {
            return false;
        }
        
        public byte ReadByte(long index)
        {
            return swag[index];
        }

        public bool SupportsWriteByte()
        {
            return true;
        }

        public void WriteByte(long index, byte value)
        {
            swag[index] = value;
        }

        #region Irrelevant Features
        public void DeleteBytes(long index, long length)
        {
            throw new NotImplementedException();
        }

        public void InsertBytes(long index, byte[] bs)
        {
            throw new NotImplementedException();
        }

        public bool SupportsDeleteBytes()
        {
            return false;
        }

        public bool SupportsInsertBytes()
        {
            return false;
        }
        #endregion
    }
}
