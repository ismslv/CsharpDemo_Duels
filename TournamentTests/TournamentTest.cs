using NUnit.Framework;

namespace Tournament.Tests
{
    [TestFixture]
    public class TournamentTest
    {
        static readonly string equipmentfile = "../../../../DuelApp/bin/Debug/Equipment.cfg";

        [SetUp]
        public void Init()
        {
            ReadData.ReadFile(equipmentfile);
        }

        [Test]
        public void SimpleTestCase()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void SwordsmanVsViking()
        {
            Warrior swordsman = new Swordsman();

            Viking viking = new Viking();

            swordsman.Engage(viking);

            Assert.AreEqual(0, swordsman.HitPoints());
            Assert.AreEqual(35, viking.HitPoints());
        }

        [Test]
        public void SwordsmanWithBucklerVsVikingWithBuckler()
        {
            Swordsman swordsman = new Swordsman()
                    .Equip("buckler");

            Viking viking = new Viking()
                    .Equip("buckler");

            swordsman.Engage(viking);

            Assert.AreEqual(0, swordsman.HitPoints());
            Assert.AreEqual(70, viking.HitPoints());
        }

        [Test]
        public void ArmoredSwordsmanVsHighlander()
        {
            Highlander highlander = new Highlander();

            Swordsman swordsman = new Swordsman()
                    .Equip("buckler")
                    .Equip("armor");

            swordsman.Engage(highlander);

            Assert.AreEqual(0, swordsman.HitPoints());
            Assert.AreEqual(10, highlander.HitPoints());
        }
        
        [Test]
        public void ViciousSwordsmanVsVeteranHighlander()
        {
            Swordsman swordsman = new Swordsman("Vicious")
                    .Equip("axe")
                    .Equip("buckler")
                    .Equip("armor");

            Highlander highlander = new Highlander("Veteran");

            swordsman.Engage(highlander);

            Assert.AreEqual(1, swordsman.HitPoints());
            Assert.AreEqual(0, highlander.HitPoints());
        }
    }
}