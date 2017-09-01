using System.Runtime.CompilerServices;

namespace System.Threading.Tasks {
	public static class AssertTaskExtensions {
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task<T> Assert<T>( this Task<T> task, Func<T, bool> predicate, Action<T> reaction )
		{
			var result = await task.ConfigureAwait( false );
			if( !predicate( result ) ) {
				reaction( result );
			}
			return result;
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task<T> Assert<T, TException>( this Task<T> task, Func<T, bool> predicate, Func<T, TException> reaction ) where TException : Exception
		{
			var result = await task.ConfigureAwait( false );
			if( !predicate( result ) ) {
				throw reaction( result );
			}
			return result;
		}
	}
}