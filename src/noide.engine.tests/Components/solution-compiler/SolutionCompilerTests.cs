using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace noide.tests
{
	[TestFixture]
	public partial class SolutionCompilerTests
	{
		private class Resource : IResource<ITestingResult>
		{
			public bool Successful = true;

			public IntPtr[] Handles
			{
				get { return new[] { new IntPtr(20), new IntPtr(21) }; }
			}

            public bool Complete(IntPtr handle)
			{
				return handle == new IntPtr(20);
			}

			public bool IsSuccessful()
			{
				return this.Successful;
			}

			public void Release()
			{
			}

			public ITestingResult Payload
			{
				get { return new TestingResult(); }
			}
		}

		private class TestingResult : ITestingResult
		{
			public bool IsSuccessful()
			{
				return false;
			}

			public ITestingCase[] GetCases()
			{
				return new ITestingCase[0];
			}			
		}

		private class WaiterFactory : IWaiterFactory
		{
			public int TimeOuts = 0;
			public int Attempts = 0;

			public IWaiter<T> Create<T>()
			{
				return new Waiter<T> { Factory = this };
			}
		}

		private class Waiter<T> : IWaiter<T>
		{
			public WaiterFactory Factory;
			public IList<IWaitable<T>> Items = new List<IWaitable<T>>();

			public void Add(IWaitable<T> target)
			{
				this.Items.Add(target);
			}

			public void Remove(IntPtr handle)
			{
				for (int i = this.Items.Count - 1; i >= 0; i--)
				{
					if (this.Items[i].Handle == handle)
					{
						this.Items.RemoveAt(i);
					}
				}
			}

			public bool IsCompleted()
			{
				return this.Items.Count == 0;
			}

			public IHeartBeat<T> Next()
			{
				this.Factory.Attempts += 1;

				if (this.Items.Count == 0 || this.Factory.TimeOuts > 0)
				{
					this.Factory.TimeOuts -= 1;
					return new HeartBeat<T>();
				}

				IWaitable<T> item = this.Items[0];
				this.Items.RemoveAt(0);

				return new HeartBeat<T>
				{ 
					IsSuccessful = true,
					Payload = item.Payload
				};
			}
		}

		private class HeartBeat<T> : IHeartBeat<T>
		{
			public bool IsSuccessful { get; set; }

			public T Payload { get; set; }
		}

		private class ReporterFactory : IReporterFactory
		{
			public IReporter Create()
			{
				return new Reporter();
			}
		}

		private class Reporter : IReporter
		{
			public void Beat()
			{
			}

			public void Trigger(IProject project)
			{
			}

			public void BeginRestoring(int round, IPackage package)
			{
			}

			public void CompleteRestoring(IPackage package, bool isSuccessful)
			{
			}

			public void BeginCompiling(int round, IProject project)
			{				
			}

			public void CompleteCompiling(IProject project, bool isSuccessful)
			{
			}

			public void BeginTesting(int round, IProject project)
			{
			}

			public void CompleteTesting(IProject project, ITestingResult result)
			{
			}
		}
	}
}