﻿// Copyright (c) 2015 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System.Reflection;
using NUnit.Runner.View;
using NUnit.Runner.ViewModel;
using Xamarin.Forms;

namespace NUnit.Runner
{
    /// <summary>
    /// The NUnit Xamarin test runner
    /// </summary>
	public partial class App : Application
    {
        private readonly SummaryViewModel _model;

        /// <summary>
        /// Constructs a new app adding the current assembly to be tested
        /// </summary>
		public App ()
		{
            InitializeComponent ();

            // OnPlatform only reports WinPhone for WinPhone Silverlight, so swap
            // out the background color in code instead
            if(Device.OS == TargetPlatform.Windows)
            {
                Resources["defaultBackground"] = Resources["windowsBackground"];
            }

            _model = new SummaryViewModel();
            MainPage = new NavigationPage(new SummaryView(_model));
#if !WINDOWS_PHONE_APP
            AddTestAssembly(Assembly.GetCallingAssembly());
#endif
        }

        /// <summary>
        /// Adds an assembly to be tested.
        /// </summary>
        /// <param name="testAssembly">The test assembly.</param>
        public void AddTestAssembly(Assembly testAssembly)
        {
            _model.AddTest(testAssembly);
        }

        /// <summary>
        /// If True, the tests will run automatically when the app starts
        /// otherwise you must run them manually.
        /// </summary>
        public bool AutoRun
        {
            get { return _model.AutoRun; }
            set { _model.AutoRun = value; }
        }
    }
}
