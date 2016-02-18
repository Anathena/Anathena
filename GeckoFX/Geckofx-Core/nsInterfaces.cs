#region ***** BEGIN LICENSE BLOCK *****
/* Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Skybound Software code.
 *
 * The Initial Developer of the Original Code is Skybound Software.
 * Portions created by the Initial Developer are Copyright (C) 2008-2009
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 */
#endregion END LICENSE BLOCK

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Runtime.CompilerServices;

// PLZ remove ANY constants, that are automatically generated (located in Generated\ folder)

namespace Gecko
{	
	public enum nsDirection 
	{
		eDirNext = 0,
		eDirPrevious = 1
	};

	public static class nsIContextMenuListener2Constants
	{
		public const uint CONTEXT_NONE = 0;
		public const uint CONTEXT_LINK = 1;
		public const uint CONTEXT_IMAGE = 2;
		public const uint CONTEXT_DOCUMENT = 4;
		public const uint CONTEXT_TEXT = 8;
		public const uint CONTEXT_INPUT = 16;
		public const uint CONTEXT_BACKGROUND_IMAGE = 32;
	}


	public static class nsIEmbeddingSiteWindowConstants
	{
		public const int DIM_FLAGS_POSITION = 1;
		public const int DIM_FLAGS_SIZE_INNER = 2;
		public const int DIM_FLAGS_SIZE_OUTER = 4;
	}

	public static class nsIWebProgressListenerConstants
	{
		// State transition flags
		public const ulong STATE_START = 0x1;
		public const ulong STATE_REDIRECTING = 0x2;
		public const ulong STATE_TRANSFERRING = 0x4;
		public const ulong STATE_NEGOTIATING = 0x8;
		public const ulong STATE_STOP = 0x10;

		// State type flags
		public const ulong STATE_IS_REQUEST = 0x10000;
		public const ulong STATE_IS_DOCUMENT = 0x20000;
		public const ulong STATE_IS_NETWORK = 0x40000;
		public const ulong STATE_IS_WINDOW = 0x80000;

		// State modifier flags
		public const int STATE_RESTORING = 0x1000000;

		// State security flags
		public const ulong STATE_IS_INSECURE = 0x4;
		public const ulong STATE_IS_BROKEN = 0x1;
		public const ulong STATE_IS_SECURE = 0x2;

		// Security strength flags
		public const ulong STATE_SECURE_HIGH = 0x40000;
		public const ulong STATE_SECURE_MED = 0x10000;
		public const ulong STATE_SECURE_LOW = 0x20000;

		// State identity flags
		public const ulong LOCATION_CHANGE_SAME_DOCUMENT = 0x00000001;
		public const ulong LOCATION_CHANGE_ERROR_PAGE = 0x00000002;
	}

	public static class nsIPromptServiceConstants
	{
		public const int BUTTON_POS_0 = 1;
		public const int BUTTON_POS_1 = 256;
		public const int BUTTON_POS_2 = 65536;

		public const int BUTTON_TITLE_OK = 1;
		public const int BUTTON_TITLE_CANCEL = 2;
		public const int BUTTON_TITLE_YES = 3;
		public const int BUTTON_TITLE_NO = 4;
		public const int BUTTON_TITLE_SAVE = 5;
		public const int BUTTON_TITLE_DONT_SAVE = 6;
		public const int BUTTON_TITLE_REVERT = 7;
		public const int BUTTON_TITLE_IS_STRING = 127;

		public const int BUTTON_POS_0_DEFAULT = 0;
		public const int BUTTON_POS_1_DEFAULT = 16777216;
		public const int BUTTON_POS_2_DEFAULT = 33554432;
		
		public const int BUTTON_DELAY_ENABLE = 67108864;
		public const int STD_OK_CANCEL_BUTTONS = 513;
		public const int STD_YES_NO_BUTTONS = 1027;
	}
	
	public delegate int nsWriteSegmentFun(nsIInputStream aInStream, IntPtr aClosure, IntPtr aFromSegment, int aToOffset, int aCount, out int aWriteCount);


    /// <see cref="https://developer.mozilla.org/en/XPCOM_Interface_Reference/nsIHttpActivityObserver"/>
	public static class nsIHttpActivityObserverConstants
    {
        public const uint ACTIVITY_TYPE_SOCKET_TRANSPORT = 0x0001;
        public const uint ACTIVITY_TYPE_HTTP_TRANSACTION = 0x0002;
        public const uint ACTIVITY_SUBTYPE_REQUEST_HEADER = 0x5001;
        public const uint ACTIVITY_SUBTYPE_REQUEST_BODY_SENT = 0x5002;
        public const uint ACTIVITY_SUBTYPE_RESPONSE_START = 0x5003;
        public const uint ACTIVITY_SUBTYPE_RESPONSE_COMPLETE = 0x5005;
        public const uint ACTIVITY_SUBTYPE_TRANSACTION_CLOSE = 0x5006;
    }

    /// <see cref="https://developer.mozilla.org/en/XPCOM_Interface_Reference/nsISocketTransport"/>
	public static class nsISocketTransportConstants
    {
        public const uint STATUS_RESOLVING      = 0x804b0003;
        public const uint STATUS_RESOLVED       = 0x804b000b;
        public const uint STATUS_CONNECTING_TO  = 0x804b0007;
        public const uint STATUS_CONNECTED_TO   = 0x804b0004;
        public const uint STATUS_SENDING_TO     = 0x804b0005;
        public const uint STATUS_WAITING_FOR    = 0x804b000a;
        public const uint STATUS_RECEIVING_FROM = 0x804b0006;
    }

}
