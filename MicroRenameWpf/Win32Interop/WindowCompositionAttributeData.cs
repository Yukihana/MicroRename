using System;
using System.Runtime.InteropServices;

namespace MicroRenameWpf.Win32Interop
{
    [StructLayout(LayoutKind.Sequential)]
	internal struct WindowCompositionAttributeData
	{
		public WindowCompositionAttribute Attribute;
		public IntPtr Data;
		public int SizeOfData;
	}
}