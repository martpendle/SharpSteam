using NUnit.Framework;
using SharpSteam;
using System;
using System.IO;
using VDFParser;
using VDFParser.Models;

namespace SharpSteamTests
{
    [TestFixture]
    public class SteamManagerTests
    {
        [Test]
        public void TestGetSteamFolder()
        {
            // Arrange
            // No arrangement necessary for this test

            // Act
            var result = SteamManager.GetSteamFolder();

            // Assert
            // Replace "expectedResult" with your expected result
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void TestGetUsers()
        {
            // Arrange
            string steamInstallPath = SteamManager.GetSteamFolder();

            // Act
            var result = SteamManager.GetUsers(steamInstallPath);

            // Assert
            // Steam generates folders with user ids, so we can't predict the exact result. But 0 should always be there.
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Contains.Item("C:\\Program Files\\Steam\\userdata\\0"));
        }

        [Test]
        public void TestReadShortcuts()
        {
            // Arrange
            string userPath = "C:\\Program Files\\Steam\\userdata\\0"; // Replace with a valid user path

            // Act
            var result = SteamManager.ReadShortcuts(userPath);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void TestWriteShortcuts()
        {
            //TODO: This test should not test the file system
        }

        [Test]
        public void VDFToBytes_ReturnsByteArray()
        {
            // Arrange
            VDFEntry[] vdfArray = new VDFEntry[1];
            vdfArray[0] = new VDFEntry()
            {
                appid = 875770417,
                AppName = "Guitar Hero World Tour",
                Exe = "\"D:\\Program Files\\GH\\GHWT.exe\"",
                StartDir = "\"D:\\Program Files\\GH\\\"",
                AllowDesktopConfig = 1,
                AllowOverlay = 1,
                IsHidden = 0,
                OpenVR = 0,
                Devkit = 0,
                DevkitGameID = "",
                Icon = "",
                LastPlayTime = 1590640610,
                LaunchOptions = "",
                ShortcutPath = "",
                Tags = new string[] { "Music" },
                Index = 0
            };

            // Act
            byte[] result = SteamManager.VDFToBytes(vdfArray);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<byte[]>(result);
        }
    }
}
