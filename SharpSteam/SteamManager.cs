using Microsoft.Win32;
using System;
using System.IO;
using VDFParser;
using VDFParser.Models;

namespace SharpSteam
{
    public static class SteamManager
    {
        /// <summary>
        /// Returns Steam's current installed path
        /// </summary>
        /// <returns></returns>
        public static string GetSteamFolder()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Valve\\Steam");
            if (key == null)
                if ((key = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Valve\\Steam")) == null)
                    return null;
            return key.GetValue("InstallPath").ToString();
        }

        /// <summary>
        /// Returns all the users on userdata
        /// </summary>
        /// <param name="steamInstallPath">Steam's current installed path</param>
        /// <returns>ListString of users path</returns>
        public static String[] GetUsers(string steamInstallPath)
        {
            return Directory.GetDirectories(steamInstallPath + "\\userdata");
        }

        /// <summary>
        /// Reads the shortcuts present in the shortcuts.vdf file for a given user path
        /// </summary>
        /// <param name="userPath">The path to which search for shortcuts</param>
        /// <returns>An array of VDFEntries</returns>
        public static VDFEntry[] ReadShortcuts(string userPath)
        {
            string shortcutFile = userPath + "\\config\\shortcuts.vdf";
            VDFEntry[] shortcuts = new VDFEntry[0];

            //Some users don't seem to have the config directory at all or the shortcut file, return a empty entry for those
            if (!Directory.Exists(userPath + "\\config\\") || !File.Exists(shortcutFile))
            {
                return shortcuts;
            }

            shortcuts = VDFParser.VDFParser.Parse(shortcutFile);

            return shortcuts;
        }

        /// <summary>
        /// Writes the received list of shortcuts to the specified path
        /// </summary>
        /// <param name="vdf">The array of shortcuts to write</param>
        /// <param name="vdfPath">The full path to which to write the shortcuts, including filename</param>
        public static void WriteShortcuts(VDFEntry[] vdf, string vdfPath)
        {
            try
            {
                File.WriteAllBytes(vdfPath, VDFToBytes(vdf));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Converts a VDFEntry array to a byte array
        /// </summary>
        /// <param name="vdf_array">The array of VDFEntry to convert to byte</param>
        /// <returns>byte[]</returns>
        public static byte[] VDFToBytes(VDFEntry[] vdf_array)
        {
            return VDFSerializer.Serialize(vdf_array);
        }
    }
}