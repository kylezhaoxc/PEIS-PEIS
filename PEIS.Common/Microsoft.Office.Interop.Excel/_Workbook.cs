using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	[System.Runtime.CompilerServices.CompilerGenerated, System.Runtime.InteropServices.Guid("000208DA-0000-0000-C000-000000000046"), TypeIdentifier]
	[System.Runtime.InteropServices.ComImport]
	public interface _Workbook
	{
		object ActiveSheet
		{
			[System.Runtime.InteropServices.DispId(307)]
			[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)]
			get;
		}

		bool Saved
		{
			[System.Runtime.InteropServices.DispId(298), System.Runtime.InteropServices.LCIDConversion(0)]
			get;
			[System.Runtime.InteropServices.DispId(298), System.Runtime.InteropServices.LCIDConversion(0)]
			set;
		}

		Sheets Worksheets
		{
			[System.Runtime.InteropServices.DispId(494)]
			[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Interface)]
			get;
		}

		void _VtblGap1_7();

		void _VtblGap2_12();

		[System.Runtime.InteropServices.DispId(277), System.Runtime.InteropServices.LCIDConversion(3)]
		void Close([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object SaveChanges = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object Filename = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object RouteWorkbook = null);

		void _VtblGap3_76();

		[System.Runtime.InteropServices.DispId(175), System.Runtime.InteropServices.LCIDConversion(1)]
		void SaveCopyAs([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object Filename = null);

		void _VtblGap4_24();

		void _VtblGap5_40();

		[System.Runtime.InteropServices.DispId(1925), System.Runtime.InteropServices.LCIDConversion(12)]
		void SaveAs([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object Filename = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object FileFormat = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object Password = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object WriteResPassword = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object ReadOnlyRecommended = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object CreateBackup = null, [System.Runtime.InteropServices.In] XlSaveAsAccessMode AccessMode = XlSaveAsAccessMode.xlNoChange, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object ConflictResolution = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object AddToMru = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object TextCodepage = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object TextVisualLayout = null, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object Local = null);
	}
}
