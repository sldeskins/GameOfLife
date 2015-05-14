using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using ClassLibraryFull;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryFullTest
{
    [TestClass]
    public class OtherTest
    {
        [TestMethod]
        public void Test_GameIsSerializable ()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(GameGrid));
            Type[] types = assembly.GetTypes();

            StringBuilder notSerializedTypes = new StringBuilder();
            int notSerializedTypesCount = 0;
            foreach (var type in types)
            {
                if (!type.IsSerializable)
                {
                    notSerializedTypes.Append(type.Name + ", ");
                    notSerializedTypesCount++;
                }
            }

            Assert.AreEqual(0, notSerializedTypesCount, string.Format("The following {0} types are not serializable: {1}", notSerializedTypesCount, notSerializedTypes.ToString()));
        }
    }
}
