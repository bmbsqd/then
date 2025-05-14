using System.Runtime.CompilerServices;

namespace System.Threading.Tasks;

public static class CatchTaskExtensions {
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<T> Catch<T>(this Task<T> task) where T : class => task.Catch(e => default(T));

	private static readonly Action<Exception> EmptyHandler = e => { };

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task Catch(this Task task) => task.Catch(EmptyHandler);

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static Task<T> Catch<T>( this Task<T> task, Func<Exception, Task<T>> exceptionHandler ) => task.Catch<T, Exception>( exceptionHandler );

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static async Task<T> Catch<T, TException>( this Task<T> task, Func<TException, Task<T>> exceptionHandler ) where TException : Exception
	{
		try {
			return await task.ConfigureAwait( false );
		}
		catch( TException e ) {
			return await exceptionHandler( e ).ConfigureAwait( false );
		}
	}

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static async Task<T> Catch<T, TException, TArg>( this Task<T> task, Func<TException, TArg, Task<T>> exceptionHandler, TArg arg ) where TException : Exception
	{
		try {
			return await task.ConfigureAwait( false );
		}
		catch( TException e ) {
			return await exceptionHandler( e, arg ).ConfigureAwait( false );
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> Catch<T, TException>(this ValueTask<T> task, Func<TException, ValueTask<T>> exceptionHandler) where TException : Exception
	{
		try
		{
			return task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		}
		catch (TException e) {
			var exceptionTask = exceptionHandler( e );
			return exceptionTask.IsCompletedSuccessfully ? exceptionTask.Result : await exceptionTask.ConfigureAwait( false );
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> Catch<T, TException, TArg>(this ValueTask<T> task, Func<TException, TArg, ValueTask<T>> exceptionHandler, TArg arg) where TException : Exception
	{
		try
		{
			return task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		}
		catch (TException e) {
			var exceptionTask = exceptionHandler( e, arg );
			return exceptionTask.IsCompletedSuccessfully ? exceptionTask.Result : await exceptionTask.ConfigureAwait( false );
		}
	}

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static Task<T> Catch<T>( this Task<T> task, Func<Exception, T> exceptionHandler ) => task.Catch<T, Exception>( exceptionHandler );

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static async Task<T> Catch<T, TException>( this Task<T> task, Func<TException, T> exceptionHandler ) where TException : Exception
	{
		try {
			return await task.ConfigureAwait( false );
		}
		catch( TException e ) {
			return exceptionHandler( e );
		}
	}

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static async Task<T> Catch<T, TException, TArg>( this Task<T> task, Func<TException, TArg, T> exceptionHandler, TArg arg ) where TException : Exception
	{
		try {
			return await task.ConfigureAwait( false );
		}
		catch( TException e ) {
			return exceptionHandler( e, arg );
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> Catch<T, TException>(this ValueTask<T> task, Func<TException, T> exceptionHandler) where TException : Exception
	{
		try {
			return task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait( false );
		}
		catch (TException e)
		{
			return exceptionHandler(e);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> Catch<T, TException, TArg>(this ValueTask<T> task, Func<TException, TArg, T> exceptionHandler, TArg arg) where TException : Exception
	{
		try {
			return task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait( false );
		}
		catch (TException e)
		{
			return exceptionHandler(e, arg);
		}
	}

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static Task Catch( this Task task, Action<Exception> exceptionHandler ) => task.Catch<Exception>( exceptionHandler );

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static Task Catch<TArg>( this Task task, Action<Exception, TArg> exceptionHandler, TArg arg ) => task.Catch<Exception,TArg>( exceptionHandler, arg );

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static async Task Catch<TException>( this Task task, Action<TException> exceptionHandler ) where TException : Exception
	{
		try {
			await task.ConfigureAwait( false );
		}
		catch( TException e ) {
			exceptionHandler( e );
		}
	}

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static async Task Catch<TException, TArg>( this Task task, Action<TException, TArg> exceptionHandler, TArg arg ) where TException : Exception
	{
		try {
			await task.ConfigureAwait( false );
		}
		catch( TException e ) {
			exceptionHandler( e, arg );
		}
	}

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static Task Catch( this Task task, Func<Exception, Task> exceptionHandler ) => task.Catch<Exception>( exceptionHandler );

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task Catch<TArg>(this Task task, Func<Exception, TArg, Task> exceptionHandler, TArg arg) => task.Catch<Exception, TArg>(exceptionHandler, arg);

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static async Task Catch<TException>( this Task task, Func<TException, Task> exceptionHandler ) where TException : Exception
	{
		try {
			await task.ConfigureAwait( false );
		}
		catch( TException e ) {
			await exceptionHandler( e ).ConfigureAwait( false );
		}
	}

	[MethodImpl( MethodImplOptions.AggressiveInlining )]
	public static async Task Catch<TException, TArg>( this Task task, Func<TException, TArg, Task> exceptionHandler, TArg arg ) where TException : Exception
	{
		try {
			await task.ConfigureAwait( false );
		}
		catch( TException e ) {
			await exceptionHandler( e, arg ).ConfigureAwait( false );
		}
	}
}