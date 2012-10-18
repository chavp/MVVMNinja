using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Billing.Test
{
    [TestClass]
    public class UnitTest1
    {
        MockRepository mocks;
        [TestInitialize]
        public void SetUp()
        {
            mocks = new MockRepository();
        }

        [TestMethod]
        public void TestMethod1()
        {
            AnimalStb animal = MockRepository.GenerateStub<AnimalStb>();
            //AnimalStb animal = mocks.StrictMock<AnimalStb>();

            Expect.Call(animal.Eyes).Return(2);

            //animal.Hello("ding");
            //LastCall.On(animal).Return("555 ding");

            //animal.Hello("teb");
            //LastCall.On(animal).Return("555 teb");

            //animal.Eyes = 2;

            mocks.ReplayAll();


            Assert.AreEqual(2, animal.Eyes);
            //Assert.AreEqual("555 teb", animal.Hello("teb"));

            //mocks.ReplayAll();

            //mocks.VerifyAll();
        }
    }

    public interface IAnimal
    {
        int Legs { get; set; }
        int Eyes { get; set; }
        string Name { get; set; }
        string Species { get; set; }

        event EventHandler Hungry;
        string GetMood();
        string Hello(string name);
    }

    public class AnimalStb
    {
        int Legs { get; set; }
        public virtual int Eyes { get; private set; }
        string Name { get; set; }
        string Species { get; set; }

        event EventHandler Hungry;
    }
}
