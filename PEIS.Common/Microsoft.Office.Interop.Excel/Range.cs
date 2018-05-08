using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	[System.Runtime.CompilerServices.CompilerGenerated, System.Runtime.InteropServices.Guid("00020846-0000-0000-C000-000000000046"), System.Runtime.InteropServices.InterfaceType(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch), TypeIdentifier]
	[System.Runtime.InteropServices.ComImport]
	public interface Range
	{
		object this[[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object RowIndex = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object ColumnIndex = null]
		{
			[System.Runtime.InteropServices.DispId(0)]
			[System.Runtime.InteropServices.PreserveSig]
			[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)]
			get;
			[System.Runtime.InteropServices.DispId(0)]
			[System.Runtime.InteropServices.PreserveSig]
			[param: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)]
			set;
		}

		Range EntireColumn
		{
			[System.Runtime.InteropServices.DispId(246)]
			[System.Runtime.InteropServices.PreserveSig]
			[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Interface)]
			get;
		}

		[System.Runtime.InteropServices.DispId(237)]
		[System.Runtime.InteropServices.PreserveSig]
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)]
		object AutoFit();
	}
}
