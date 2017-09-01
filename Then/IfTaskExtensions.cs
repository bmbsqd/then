using System.Runtime.CompilerServices;

namespace System.Threading.Tasks {
	public static class IfTaskExtensions {
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Task If( this Task task, bool check, Action then )
			=> check ? task.Then( then ) : task;
	}
}