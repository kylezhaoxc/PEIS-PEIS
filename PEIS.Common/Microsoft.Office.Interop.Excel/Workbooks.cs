using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel
{
	[System.Reflection.DefaultMember("_Default"), System.Runtime.CompilerServices.CompilerGenerated, System.Runtime.InteropServices.Guid("000208DB-0000-0000-C000-000000000046"), TypeIdentifier]
	[System.Runtime.InteropServices.ComImport]
	public interface Workbooks : System.Collections.IEnumerable
	{
		void _VtblGap1_3();

		[System.Runtime.InteropServices.DispId(181), System.Runtime.InteropServices.LCIDConversion(1)]
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Interface)]
		Workbook Add([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Struct)] [System.Runtime.InteropServices.In] object Template = null);
	}
}
