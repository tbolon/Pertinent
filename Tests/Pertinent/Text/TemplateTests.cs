using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Pertinent.Text
{
    [TestFixture]
    public class TemplateTests
    {
        [Test]
        public void Empty()
        {
            var t = new TextTemplate("");
            Assert.AreEqual("", t.Execute());
        }

        [Test]
        public void Single()
        {
            var t = new TextTemplate("a");
            Assert.AreEqual("a", t.Execute());
        }

        [Test]
        public void Multi()
        {
            var t = new TextTemplate("foo bar");
            Assert.AreEqual("foo bar", t.Execute());
        }

        [Test]
        public void Single_Parameter()
        {
            var t = new TextTemplate("[foo]");
            Assert.AreEqual("", t.Execute());
        }

        [Test]
        public void Single_Parameter_With_Default_Value()
        {
            var t = new TextTemplate("[foo:bar]");
            Assert.AreEqual("bar", t.Execute());
        }

        [Test]
        public void Single_Parameter_With_Format()
        {
            var t = new TextTemplate("[foo:bar:Hello {0}!]");
            Assert.AreEqual("Hello bar!", t.Execute());
        }

        [Test]
        public void Single_Parameter_Empty_With_Format()
        {
            var t = new TextTemplate("[foo::Hello {0}!]");
            Assert.AreEqual("Hello !", t.Execute());
        }
    }
}
