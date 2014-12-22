using System.Collections.Generic;
using System.Drawing;
using GearsCreator;
using GearsCreator.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GearsCreatorTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestKompasSketch1()
        {
            var a = new KompasSketch {CopiesCount = 3};
            Assert.AreEqual(3, a.CopiesCount);
        }
        [TestMethod]
        public void TestKompasSketch2()
        {
            var b = new KompasSketch {Radius = 43212};
            Assert.AreEqual(43212, b.Radius);
        }
         [TestMethod]
        public void TestKompasSketch3()
        {
             var d = new KompasSketch {SketchName = "asdas"};
             Assert.AreEqual("asdas", d.SketchName);
        }

        /// <summary>
        /// Проверяет на валидность параметр внешний диаметр первой шестерни
        /// </summary>
        [TestMethod]
        public void TestValidParameters()
        {
            var parameterData = new ModelParameters();
            var parameters = new Dictionary<Parameter, ParameterData>
            {
                // Внешний диаметр первой шестерни
                {
                    Parameter.FirstExternalDiameter,
                    new ParameterData(Parameter.FirstExternalDiameter.ToString(), 20, new PointF(18, 50))
                },
            };
            var error = parameterData.CheckData(parameters);
            if (error.Count != 0)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Проверяет на не валидность параметр внутренний  диаметр первой шестерни
        /// </summary>
        [TestMethod]
        public void TestNotValidParameters()
        {
            var parameterData = new ModelParameters();
            var parameters = new Dictionary<Parameter, ParameterData>
            {
                // Внутренний диаметр первой шестерни.
                {
                    Parameter.FirstInnerDiameter,
                    new ParameterData(Parameter.FirstInnerDiameter.ToString(), 2, new PointF(6, 50))
                },

            };
            var error = parameterData.CheckData(parameters);
            if (error.Count == 0)
            {
                Assert.Fail();
            }
        }
    }
} 