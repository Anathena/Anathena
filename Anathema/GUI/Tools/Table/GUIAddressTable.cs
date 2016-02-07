using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anathema.Properties;

namespace Anathema
{
    public partial class GUIAddressTable : UserControl, IAddressTableView
    {
        private AddressTablePresenter AddressTablePresenter;
        
        public GUIAddressTable()
        {
            InitializeComponent();

            AddressTablePresenter = new AddressTablePresenter(this, AddressTable.GetInstance());
        }

        public void UpdateAddressTableItemCount(Int32 ItemCount)
        {
            ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
            {
                AddressTableListView.BeginUpdate();
                AddressTableListView.VirtualListSize = ItemCount;
                AddressTableListView.EndUpdate();
            });
        }

        public void ReadValues()
        {
            UpdateReadBounds();

            ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
            {
                AddressTableListView.BeginUpdate();
                AddressTableListView.EndUpdate();
            });
        }

        private void UpdateReadBounds()
        {
            ControlThreadingHelper.InvokeControlAction(AddressTableListView, () =>
            {
                Tuple<Int32, Int32> ReadBounds = AddressTableListView.GetReadBounds();
                AddressTablePresenter.UpdateReadBounds(ReadBounds.Item1, ReadBounds.Item2);
            });
        }

    } // End class

} // End namespace