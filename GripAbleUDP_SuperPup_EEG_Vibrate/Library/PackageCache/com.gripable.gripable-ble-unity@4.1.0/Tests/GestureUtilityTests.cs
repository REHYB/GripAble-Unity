using NUnit.Framework;
using Protos;

namespace GestureUtilityTests
{
	public class GestureUtilityTests
    {
        [Test]
        public void GetTranslation_returnsCorrectForSqueeze()
        {
            float translation = GestureUtility.GetTranslation(0, 5, GestureType.Squeeze, 0.5f);
            Assert.AreEqual(translation, 2.5f);
        }

        [Test]
        public void GetTranslation_returnsCorrectForRelease()
        {
            float translation = GestureUtility.GetTranslation(0, 5, GestureType.Release, 0.5f);
            Assert.AreEqual(translation, -2.5f);
        }

        [Test]
        public void GetTranslation_returnsCorrectForSupination()
        {
            float translation = GestureUtility.GetTranslation(340, 15, GestureType.Supination, 0.3f);
            Assert.AreEqual(translation, 10.5f);
        }

        [Test]
        public void GetTranslation_returnsCorrectForPronation()
        {
            float translation = GestureUtility.GetTranslation(340, 15, GestureType.Pronation, 0.3f);
            Assert.AreEqual(translation, -10.5f);
        }

        [Test]
        public void GetTranslation_returnsCorrectForUlnar()
        {
            float translation = GestureUtility.GetTranslation(340, 30, GestureType.Ulnar, 0.5f);
            Assert.AreEqual(translation, 25f);
        }

        [Test]
        public void GetTranslation_returnsCorrectForRadial()
        {
            float translation = GestureUtility.GetTranslation(340, 30, GestureType.Radial, 0.5f);
            Assert.AreEqual(translation, -25f);
        }

        [Test]
        public void GetTranslation_returnsCorrectForFlexion()
        {
            float translation = GestureUtility.GetTranslation(340, 30, GestureType.Flexion, 0.5f);
            Assert.AreEqual(translation, 25f);
        }

        [Test]
        public void GetTranslation_returnsCorrectForExtension()
        {
            float translation = GestureUtility.GetTranslation(340, 30, GestureType.Extension, 0.5f);
            Assert.AreEqual(translation, -25f);
        }
    }
}
