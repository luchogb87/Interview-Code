using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Tests
{
    [TestClass()]
    public class JobLoggerTests
    {
        #region Testing Destinations
            [TestMethod]
            public void SavingInAllDestinations()
            {
                ConsoleApplication1.JobLogger JLoger = new ConsoleApplication1.JobLogger(true, true, true);
                JLoger.LogMessage("HolaMundo", true, false, false);
            }

            [TestMethod]
            public void SavingOnlyInDB()
            {
                ConsoleApplication1.JobLogger JLoger = new ConsoleApplication1.JobLogger(false, false, true);
                JLoger.LogMessage("HolaMundo", true, false, false);
            }
        #endregion

        #region Testing Message Types.
            [TestMethod]
            public void ChoosingWarningAndErrorMsgType()
            {
                ConsoleApplication1.JobLogger JLoger = new ConsoleApplication1.JobLogger(true, true, true);
                JLoger.LogMessage("HolaMundo", false, true, true);
            }

            [TestMethod]
            public void ChoosingOnlyErrorMsgType()
            {
                ConsoleApplication1.JobLogger JLoger = new ConsoleApplication1.JobLogger(true, true, true);
                JLoger.LogMessage("HolaMundo", false, false, true);
            }
        #endregion

        #region Testing Other Possible Errors.
            [TestMethod]
            public void NonSpecifiedMessageTypeError()
            {
                ConsoleApplication1.JobLogger JLoger = new ConsoleApplication1.JobLogger(true, false, false);
                JLoger.LogMessage("HolaMundo", false, false, false);
            }

            [TestMethod]
            public void NonSpecifiedMessageDestination()
            {
                ConsoleApplication1.JobLogger JLoger = new ConsoleApplication1.JobLogger(false, false, false);
                JLoger.LogMessage("HolaMundo", false, true, false);
            }
        #endregion
    }
}