using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks {
	public static class ThenTaskExtensions {
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task<TResult> Then<T, TResult>( this Task<T> task, Func<T, Func<TResult>> selector )
		{
			var result = await task.ConfigureAwait( false );
			return selector( result ).Invoke();
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task<TResult> Then<T, TResult>( this Task<T> task, Func<T, Func<Task<TResult>>> selector )
		{
			var result = await task.ConfigureAwait( false );
			return await selector( result ).Invoke().ConfigureAwait( false );
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task<TResult> Then<T, TResult>( this Task<T> task, Func<T, TResult> then )
		{
			var result = await task.ConfigureAwait( false );
			return then( result );
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task<TResult> Then<T, TResult>( this Task<T> task, Func<T, Task<TResult>> then )
		{
			var result = await task.ConfigureAwait( false );
			return await then( result ).ConfigureAwait( false );
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task Then<T>( this Task<T> task, Func<T, Task> then )
		{
			var result = await task.ConfigureAwait( false );
			await then( result ).ConfigureAwait( false );
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task Then<T>( this Task<T> task, Action<T> then )
		{
			var result = await task.ConfigureAwait( false );
			then( result );
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task Then( this Task task, Func<Task> then )
		{
			await task.ConfigureAwait( false );
			await then().ConfigureAwait( false );
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task<T[]> Then<T>( this Task task, Func<IEnumerable<Task<T>>> then )
		{
			await task.ConfigureAwait( false );
			return await Task.WhenAll( then() ).ConfigureAwait( false );
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task Then( this Task task, Func<IEnumerable<Task>> then )
		{
			await task.ConfigureAwait( false );
			await Task.WhenAll( then() ).ConfigureAwait( false );
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task<TResult> Then<TResult>( this Task task, Func<Task<TResult>> then )
		{
			await task.ConfigureAwait( false );
			return await then().ConfigureAwait( false );
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task Then( this Task task, Action then )
		{
			await task.ConfigureAwait( false );
			then();
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static async Task<TResult> Then<TResult>( this Task task, Func<TResult> then )
		{
			await task.ConfigureAwait( false );
			return then();
		}
	}
}