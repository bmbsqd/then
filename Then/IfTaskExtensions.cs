using System.Runtime.CompilerServices;

namespace System.Threading.Tasks;

public static class IfTaskExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task If(this Task task, bool check, Action then)
		=> check ? task.Then(then) : task;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task If<TArg>(this Task task, bool check, Action<TArg> then, TArg arg)
		=> check ? task.Then(then, arg) : task;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task If<TArg>(this Task task, bool check, Func<TArg, Task> then, TArg arg)
		=> check ? task.Then(then, arg) : task;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ValueTask If<TArg>(this ValueTask task, bool check, Func<TArg, ValueTask> then, TArg arg)
		=> check ? task.Then(then, arg) : task;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task If(this Task task, bool check, Func<Task> then)
		=> check ? task.Then(then) : task;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ValueTask If(this ValueTask task, bool check, Func<ValueTask> then)
		=> check ? task.Then(then) : task;
}
