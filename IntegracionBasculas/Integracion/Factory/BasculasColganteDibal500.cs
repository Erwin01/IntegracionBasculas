using Integracion.Adapter;
using System;

namespace Integracion.Factory
{
	public class BasculasColganteDibal500 : BasculaColganteDibal500Adapter
    {
        private void ItemSend() 
        {
			try
			{
				DibalItem2[] myItems = new DibalItem2[0];
				DibalScale[] myScales = new DibalScale[10];
				string Result = string.Empty;

				if (myItems == null)
				{
					throw new Exception(string.Format("No posee una conexión. Se recibió: {0}\n"));
				}

				myScales = GetScales([10]);
				myItems = GetItems();

				IntPtr ptrResult = ItemsSend2(myScales, myScales.Length, myItems, myItems.Length, Convert.ToInt32(cboShowWindow.SelectedValue), Convert.ToInt32(cboCloseTime.SelectedValue));

				Result = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptrResult);

			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
