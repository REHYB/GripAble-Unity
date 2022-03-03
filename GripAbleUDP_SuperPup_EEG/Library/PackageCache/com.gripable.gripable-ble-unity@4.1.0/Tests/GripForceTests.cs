using NUnit.Framework;
using Gripable;
using Protos;

namespace GripForceTests
{
    public class GripForceTests
    {
        [Test]
        public void CalculateNormalizeGripForce_Returns0When0()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedGripForce(0, 0, 0, 0, 1);
            Assert.AreEqual(0, normalized);
        }

        [Test]
        public void CalculateNormalizeGripForce_Force5_Min0_Neutral0_Max10_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedGripForce(5, 0, 0, 10, 1);
            Assert.AreEqual(0.5f, normalized);
        }

        [Test]
        public void CalculateNormalizeGripForce_Force1_Min0_Max20_Scale0f2()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedGripForce(1, 0, 0, 10, 0.2f);
            Assert.AreEqual(0.5f, normalized);
        }

        [Test]
        public void CalculateNormalizeGripForce_Force15_Min10_Neutral10_Max30_Scale0f5()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedGripForce(15, 10, 10, 30, 0.5f);
            Assert.AreEqual(0.5f, normalized);
        }

        [Test]
        public void CalculateNormalizeGripForce_Force0_Min20_Max40_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedGripForce(0, 20, 0, 40, 1);
            Assert.AreEqual(0, normalized);
        }

        [Test]
        public void CalculateNormalizeGripForce_Force0_Min20_Max40_Scale0f2()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedGripForce(0, 20, 0, 40, 0.2f);
            Assert.AreEqual(0, normalized);
        }

        [Test]
        public void CalculateNormalizeGripForce_Force15_Min10_Neutral20_Max40_Scale0f5()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedGripForce(15, 10, 20, 40, 0.5f);
            Assert.AreEqual(0, normalized);
        }

        [Test]
        public void CalculateNormalizeGripForce_Force18_Min10_Neutral20_Max40_Scale0f5()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedGripForce(18, 10, 20, 40, 0.5f);
            Assert.AreEqual(0.2f, normalized);
        }
    }
}
