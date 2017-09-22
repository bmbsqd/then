using System;
using System.Threading.Tasks;
using Xunit;

namespace Then.Tests {
	public class CatchTests {
		public class CustomException : Exception {
			public CustomException() : this( "Hello World" ) { }
			public CustomException( string message ) : base( message ) { }
		}

		public class OtherException : Exception {
			public OtherException() : this( "Hello World" ) { }
			public OtherException( string message ) : base( message ) { }
		}

		[Fact]
		public async Task CatchTestSimple()
		{
			await Task
				.FromException( new CustomException() )
				.Catch();
		}

		[Fact]
		public async Task CatchTestSimple2()
		{
			var result = await Task
				.FromException<string>( new CustomException() )
				.Catch();

			Assert.Null( result );
		}


		[Fact]
		public async Task SimpleUntypedExceptionHandler()
		{
			var result = await Task
				.FromException<string>( new CustomException() )
				.Catch( e => "hello" );

			Assert.Equal( "hello", result );
		}

		[Fact]
		public async Task SimpleTypedExceptionHandler()
		{
			var result = await Task
				.FromException<string>( new CustomException() )
				.Catch<string, CustomException>( e => "hello" );

			Assert.Equal( "hello", result );
		}

		[Fact]
		public async Task SimpleUntypedAsyncExceptionHandler()
		{
			var result = await Task
				.FromException<string>( new CustomException() )
				.Catch( async e => "hello" );

			Assert.Equal( "hello", result );
		}

		[Fact]
		public async Task SimpleTypedAsyncExceptionHandler()
		{
			var result = await Task
				.FromException<string>( new CustomException() )
				.Catch<string, CustomException>( async e => "hello" );

			Assert.Equal( "hello", result );
		}

		[Fact]
		public async Task SimpleTypedExceptionHandlerWithNoResult()
		{
			await Task
				.FromException( new CustomException() )
				.Catch<CustomException>( e => { } );
		}

		[Fact]
		public async Task SimpleUntypedExceptionHandlerWithNoResult()
		{
			await Task
				.FromException( new CustomException() )
				.Catch( e => { } );
		}

		[Fact]
		public async Task SimpleTypedAsyncExceptionHandlerWithNoResult()
		{
			await Task
				.FromException( new CustomException() )
				.Catch<CustomException>( async e => { } );
		}

		[Fact]
		public async Task SimpleUntypedAsyncExceptionHandlerWithNoResult()
		{
			await Task
				.FromException( new CustomException() )
				.Catch( async e => { } );
		}


		[Fact]
		public async Task FilteredExceptionHandlerShouldNotCatchOtherExceptions()
		{
			await Assert.ThrowsAsync<OtherException>( async () => {
				await Task
					.FromException<string>( new OtherException() )
					.Catch<string, CustomException>( e => "hello" );
			} );
		}
	}
}