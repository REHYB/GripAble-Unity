using NUnit.Framework;
using Gripable;
using Protos;

namespace WristRpyTests
{
    public class WristRpyTests
    {

        [Test]
        public void CalculateNormalizeRpy_Degrees0_Min350_Max10_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(0, 350, 10, 1);
            Assert.AreEqual(0, normalized);
        }

        [Test]
        public void CalculateNormalizeRpy_Degrees10_Min350_Max10_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(10, 350, 10, 1);
            Assert.AreEqual(1f, normalized);
        }

        [Test]
        public void CalculateNormalizeRpy_Degrees350_Min350_Max10_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(350, 350, 10, 1);
            Assert.AreEqual(-1, normalized);
        }

        [Test]
        public void CalculateNormalizeRpy_Degrees10_Min350_Max20_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(10, 350, 20, 1);
            Assert.AreEqual(0.5f, normalized);
        }

        [Test]
        public void CalculateNormalizeRpy_Degrees350_Min310_Max10_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(350, 310, 10, 1);
            Assert.AreEqual(-0.2f, normalized);
        }

        [Test]
        public void CalculateNormalizeRpy_Degrees0_Min0_Max0_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(0, 0, 0, 1);
            Assert.AreEqual(0, normalized);
        }

        [Test]
        public void CalculateNormalizeRpy_Degrees5_Min0_Max0_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(5, 0, 0, 1);
            Assert.AreEqual(1, normalized);
        }

        [Test]
        public void CalculateNormalizeRpy_Degrees355_Min0_Max0_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(355, 0, 0, 1);
            Assert.AreEqual(-1, normalized);
        }

        [Test]
        public void CalculateNormalizeRpy_Degrees5_Min359f9_Max0f1_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(5, 359.9f, 0.1f, 1);
            Assert.AreEqual(1, normalized);
        }

        [Test]
        public void CalculateNormalizeRpy_Degrees355_Min359f9_Max0f1_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(355, 359.9f, 0.1f, 1);
            Assert.AreEqual(-1, normalized);
        }

        [Test]
        public void CalculateNormalizeRpy_Degrees0f025_Min359f9_Max0f1_Scale0f5()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(0.025f, 359.9f, 0.1f, 0.5f);
            Assert.AreEqual(0.5f, normalized);
        }

        [Test]
        public void CalculateNormalizeRpy_Degrees359f95_Min359f9_Max0f1_Scale1()
        {
            float normalized = GripablePlugin.Player.CalculateNormalizedWristRpy(359.95f, 359.9f, 0.1f, 1);
            Assert.That(-0.5f, Is.EqualTo(normalized).Within(.0005));
        }
    }
}