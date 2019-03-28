using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void TestAddition()
        {
            {
                var command = "1+2";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(3, result, 1e-10);
            }
            {
                var command = "755 + 86";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(841, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestSubtraction()
        {
            {
                var command = "6-4";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(2, result, 1e-10);
            }
            {
                var command = "7890 - 987";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(6903, result, 1e-10);
            }
            {
                var command = "17 - 98";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(-81, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestAdditionSubtraction()
        {
            {
                var command = "2+2+7";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(11, result, 1e-10);
            }
            {
                var command = "1000 - 200 - 75";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(725, result, 1e-10);
            }
            {
                var command = "90 + 75 - 56 + 7 - 2";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(114, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestMultiplication()
        {
            {
                var command = "3*7";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(21, result, 1e-10);
            }
            {
                var command = "17 * 12";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(204, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestDivision()
        {
            {
                var command = "6/2";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(3, result, 1e-10);
            }
            {
                var command = "750 / 25";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(30, result, 1e-10);
            }
            {
                var command = "9 / 7";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(9.0 / 7.0, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestMultiplicationDivision()
        {
            {
                var command = "1*2*3";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(6, result, 1e-10);
            }
            {
                var command = "700 / 7 / 40";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(2.5, result, 1e-10);
            }
            {
                var command = "12 * 3 / 4 * 6 / 4";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(13.5, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestMultipleOperations()
        {
            {
                var command = "2 * 3 + 4";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(10, result, 1e-10);
            }
            {
                var command = "12 / 3 - 8";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(-4, result, 1e-10);
            }
            {
                var command = "18 * 3 / 9 + 100 - 97";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(9, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestOrderOfOperations()
        {
            {
                var command = "6/2+3*4";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(15, result, 1e-10);
            }
            {
                var command = "11 - 5/2";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(8.5, result, 1e-10);
            }
            {
                var command = "12-3 * 3 -8";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(-5, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestWhitespace()
        {
            {
                var command = "     10     +12    ";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(22, result, 1e-10);
            }
            {
                var command = " 5  *3   +2   -5*2 ";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(7, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestNegativeNumbers()
        {
            {
                var command = "-2 + 3";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(1, result, 1e-10);
            }
            {
                var command = "12 - -2";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(14, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestDecimals()
        {
            {
                var command = "2.1+3.3";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(5.4, result, 1e-10);
            }
            {
                var command = "2.891*21.67+10.0";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(72.64797, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestBrackets()
        {
            {
                var command = "(1+1)*2";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(4, result, 1e-10);
            }
            {
                var command = "(10+2) / (5-3)";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(6, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestNestedBrackets()
        {
            {
                var command = "((1+1))";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(2, result, 1e-10);
            }
            {
                var command = "((9-3)*2)+1";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(13, result, 1e-10);
            }
        }

        [TestMethod()]
        public void TestIntegrated()
        {
            {
                var command = "(2 *     (3+4.5)- 1.2 * (4 - 3.5)) /-3";
                var result = Double.Parse(Program.ProcessCommand(command));
                Assert.AreEqual(-4.8, result, 1e-10);
            }
        }
    }
}