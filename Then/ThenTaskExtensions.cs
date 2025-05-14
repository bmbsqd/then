using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks;

public static class ThenTaskExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<T, TResult>(this Task<T> task, Func<T, Func<TResult>> selector) {
		var result = await task.ConfigureAwait(false);
		return selector(result).Invoke();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<T, TArg, TResult>(this Task<T> task, Func<T, Func<TArg, TResult>> selector, TArg arg) {
		var result = await task.ConfigureAwait(false);
		return selector(result).Invoke(arg);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<TResult> Then<T, TResult>(this ValueTask<T> task, Func<T, Func<TResult>> selector) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		return selector(result).Invoke();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<TResult> Then<T, TArg, TResult>(this ValueTask<T> task, Func<T, Func<TArg, TResult>> selector, TArg arg) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		return selector(result).Invoke(arg);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<TResult> Then<T, TArg, TResult>(this ValueTask<T> task, Func<T, TArg, Func<TResult>> selector, TArg arg) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		return selector(result, arg).Invoke();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<TResult> Then<T, TArg, TResult>(this ValueTask<T> task, Func<T, TArg, Func<TArg, TResult>> selector, TArg arg) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		return selector(result, arg).Invoke(arg);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<T, TResult>(this Task<T> task, Func<T, Func<Task<TResult>>> selector) {
		var result = await task.ConfigureAwait(false);
		return await selector(result).Invoke().ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<T, TArg, TResult>(this Task<T> task, Func<T, Func<TArg, Task<TResult>>> selector, TArg arg) {
		var result = await task.ConfigureAwait(false);
		return await selector(result).Invoke(arg).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<TResult> Then<T, TResult>(this ValueTask<T> task, Func<T, Func<ValueTask<TResult>>> selector) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);

		var selectorTask = selector(result).Invoke();
		return selectorTask.IsCompletedSuccessfully ? selectorTask.Result : await selectorTask.ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<TResult> Then<T, TArg, TResult>(this ValueTask<T> task, Func<T, Func<TArg, ValueTask<TResult>>> selector, TArg arg) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);

		var selectorTask = selector(result).Invoke(arg);
		return selectorTask.IsCompletedSuccessfully ? selectorTask.Result : await selectorTask.ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<T, TResult>(this Task<T> task, Func<T, TResult> then) {
		var result = await task.ConfigureAwait(false);
		return then(result);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<T, TArg, TResult>(this Task<T> task, Func<T, TArg, TResult> then, TArg arg) {
		var result = await task.ConfigureAwait(false);
		return then(result, arg);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<TResult> Then<T, TResult>(this ValueTask<T> task, Func<T, TResult> then) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		return then(result);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<TResult> Then<T, TArg, TResult>(this ValueTask<T> task, Func<T, TArg, TResult> then, TArg arg) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		return then(result, arg);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<T, TResult>(this Task<T> task, Func<T, Task<TResult>> then) {
		var result = await task.ConfigureAwait(false);
		return await then(result).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<T, TArg, TResult>(this Task<T> task, Func<T, TArg, Task<TResult>> then, TArg arg) {
		var result = await task.ConfigureAwait(false);
		return await then(result, arg).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<TResult> Then<T, TResult>(this ValueTask<T> task, Func<T, ValueTask<TResult>> then) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		var thenTask = then(result);
		return thenTask.IsCompletedSuccessfully ? thenTask.Result : await thenTask.ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask Then(this ValueTask task, Func<ValueTask> then) {
		if( !task.IsCompletedSuccessfully ){ await task.ConfigureAwait(false);}
		await then().ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask<TResult> Then<T, TArg, TResult>(this ValueTask<T> task, Func<T, TArg, ValueTask<TResult>> then, TArg arg) {
		var result = task.IsCompletedSuccessfully ? task.Result : await task.ConfigureAwait(false);
		var thenTask = then(result, arg);
		return thenTask.IsCompletedSuccessfully ? thenTask.Result : await thenTask.ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async ValueTask Then<TArg>(this ValueTask task, Func<TArg, ValueTask> then, TArg arg) {
		if( !task.IsCompletedSuccessfully ) {
			await task.ConfigureAwait(false);}

		await then(arg).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task Then<T>(this Task<T> task, Func<T, Task> then) {
		var result = await task.ConfigureAwait(false);
		await then(result).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task Then<T, TArg>(this Task<T> task, Func<T, TArg, Task> then, TArg arg) {
		var result = await task.ConfigureAwait(false);
		await then(result, arg).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task Then<T>(this Task<T> task, Action<T> then) {
		var result = await task.ConfigureAwait(false);
		then(result);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task Then<T, TArg>(this Task<T> task, Action<T, TArg> then, TArg arg) {
		var result = await task.ConfigureAwait(false);
		then(result, arg);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task Then(this Task task, Func<Task> then) {
		await task.ConfigureAwait(false);
		await then().ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task Then<TArg>(this Task task, Func<TArg, Task> then, TArg arg) {
		await task.ConfigureAwait(false);
		await then(arg).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<T[]> Then<T>(this Task task, Func<IEnumerable<Task<T>>> then) {
		await task.ConfigureAwait(false);
		return await Task.WhenAll(then()).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<T[]> Then<T, TArg>(this Task task, Func<TArg, IEnumerable<Task<T>>> then, TArg arg) {
		await task.ConfigureAwait(false);
		return await Task.WhenAll(then(arg)).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task Then(this Task task, Func<IEnumerable<Task>> then) {
		await task.ConfigureAwait(false);
		await Task.WhenAll(then()).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task Then<TArg>(this Task task, Func<TArg, IEnumerable<Task>> then, TArg arg) {
		await task.ConfigureAwait(false);
		await Task.WhenAll(then(arg)).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<TResult>(this Task task, Func<Task<TResult>> then) {
		await task.ConfigureAwait(false);
		return await then().ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<TResult, TArg>(this Task task, Func<TArg, Task<TResult>> then, TArg arg) {
		await task.ConfigureAwait(false);
		return await then(arg).ConfigureAwait(false);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task Then(this Task task, Action then) {
		await task.ConfigureAwait(false);
		then();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task Then<TArg>(this Task task, Action<TArg> then, TArg arg) {
		await task.ConfigureAwait(false);
		then(arg);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<TResult>(this Task task, Func<TResult> then) {
		await task.ConfigureAwait(false);
		return then();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Then<TResult, TArg>(this Task task, Func<TArg, TResult> then, TArg arg) {
		await task.ConfigureAwait(false);
		return then(arg);
	}
}