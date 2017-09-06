using System.Runtime.CompilerServices;

namespace System.Threading.Tasks {
	public static class CatchTaskExtensions {
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task<T> Catch<T>( this Task<T> task, Func<Exception, Task<T>> exceptionHandler )
		{
			try {
				return await task.ConfigureAwait( false );
			}
			catch( Exception e ) {
				return await exceptionHandler( e ).ConfigureAwait( false );
			}
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task<T> Catch<T>( this Task<T> task, Func<Exception, T> exceptionHandler )
		{
			try {
				return await task.ConfigureAwait( false );
			}
			catch( Exception e ) {
				return exceptionHandler( e );
			}
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Task<T> Catch<T>( this Task<T> task ) where T : class => task.Catch( e => default(T) );

		private static readonly Action<Exception> s_emptyHandler = e => { };

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Task Catch( this Task task ) => task.Catch( s_emptyHandler );

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task Catch( this Task task, Action<Exception> exceptionHandler )
		{
			try {
				await task.ConfigureAwait( false );
			}
			catch( Exception e ) {
				exceptionHandler( e );
			}
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task Catch<TException>( this Task task, Action<TException> exceptionHandler ) where TException : Exception
		{
			try {
				await task.ConfigureAwait( false );
			}
			catch( Exception e ) when( e is TException ) {
				exceptionHandler( (TException)e );
			}
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task Catch( this Task task, Func<Exception, Task> exceptionHandler )
		{
			try {
				await task.ConfigureAwait( false );
			}
			catch( Exception e ) {
				await exceptionHandler( e ).ConfigureAwait( false );
			}
		}
	}
}