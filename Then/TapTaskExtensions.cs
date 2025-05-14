using System.Runtime.CompilerServices;

namespace System.Threading.Tasks;

public static class TapTaskExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<T> Tap<T>(this Task<T> task, Func<Task> then) {
		var result = await task.ConfigureAwait(false);
		await then().ConfigureAwait(false);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<T> Tap<T, TArg>(this Task<T> task, Func<TArg, Task> then, TArg arg) {
		var result = await task.ConfigureAwait(false);
		await then(arg).ConfigureAwait(false);
		return result;
	}

	public static async ValueTask<T> Tap<T>(this ValueTask<T> task, Func<ValueTask> then) {
		var result = task.IsCompletedSuccessfully
			? task.Result
			: await task.ConfigureAwait(false);

		var thenTask = then();
		if( !thenTask.IsCompletedSuccessfully ) {
			await thenTask.ConfigureAwait(false);
		}

		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> Tap<T, TArg>(this ValueTask<T> task, Func<TArg, Task> then, TArg arg) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		await then(arg).ConfigureAwait(false);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<T> Tap<T>(this Task<T> task, Action<T> then) {
		var result = await task.ConfigureAwait(false);
		then(result);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<T> Tap<T, TArg>(this Task<T> task, Action<T, TArg> then, TArg arg) {
		var result = await task.ConfigureAwait(false);
		then(result, arg);
		return result;
	}

	public static async ValueTask<T> Tap<T>(this ValueTask<T> task, Action<T> then) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		then(result);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> Tap<T, TArg>(this ValueTask<T> task, Action<T, TArg> then, TArg arg) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		then(result, arg);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<T> Tap<T>(this Task<T> task, Action then) {
		var result = await task.ConfigureAwait(false);
		then();
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<T> Tap<T, TArg>(this Task<T> task, Action<TArg> then, TArg arg) {
		var result = await task.ConfigureAwait(false);
		then(arg);
		return result;
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> Tap<T>(this ValueTask<T> task, Action then) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		then();
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> Tap<T, TArg>(this ValueTask<T> task, Action<TArg> then, TArg arg) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		then(arg);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task Tap<TArg>(this Task task, Action<TArg> then, TArg arg) {
		await task.ConfigureAwait(false);
		then(arg);
	}

	public static async Task Tap(this Task task, Action then) {
		await task.ConfigureAwait(false);
		then();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<T> Tap<T>(this Task<T> task, Func<T, Task> then) {
		var result = await task.ConfigureAwait(false);
		await then(result).ConfigureAwait(false);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<T> Tap<T, TArg>(this Task<T> task, Func<T, TArg, Task> then, TArg arg) {
		var result = await task.ConfigureAwait(false);
		await then(result, arg).ConfigureAwait(false);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> Tap<T>(this ValueTask<T> task, Func<T, Task> then) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		await then(result).ConfigureAwait(false);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<T> Tap<T, TArg>(this ValueTask<T> task, Func<T, TArg, Task> then, TArg arg) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		await then(result, arg).ConfigureAwait(false);
		return result;
	}
}
