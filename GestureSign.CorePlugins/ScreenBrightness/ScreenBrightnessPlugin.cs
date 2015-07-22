﻿///<summary>
///
///Tiny tool for quickly adjusting screen brightness of laptops and tablets
///Tested on win7 (x86)
///needs .net 4 (but should compile on lower versions, too...probably You have to rebuild the form - dunno)
///Does not work on normal pcs...as far as I know...
///free to use and whatever...but not for sale...^^
///code for wmi queries stolen from Samuel Lai http://edgylogic.com/projects/display-brightness-vista-gadget/ :)
///
///</summary>

using System;
using System.Management;
using System.Windows.Controls;
using GestureSign.Common.Localization;
using GestureSign.Common.Plugins;

namespace GestureSign.CorePlugins.ScreenBrightness
{
    class ScreenBrightnessPlugin : IPlugin
    {
        #region Private Variables
        private ScreenBrightnessUI _GUI = null;
        private BrightnessSettings _Settings = null;

        private enum Method
        {
            BrightnessUp = 0,
            BrightnessDown = 1
        }
        #endregion

        #region Public Properties
        public string Name
        {
            get { return LocalizationProvider.Instance.GetTextValue("CorePlugins.ScreenBrightness.Name"); }
        }

        public string Description
        {
            get { return GetDescription(_Settings); }
        }

        public UserControl GUI
        {
            get
            {
                if (_GUI == null)
                    _GUI = CreateGUI();

                return _GUI;
            }
        }

        public ScreenBrightnessUI TypedGUI
        {
            get { return (ScreenBrightnessUI)GUI; }
        }

        public string Category
        {
            get { return LocalizationProvider.Instance.GetTextValue("CorePlugins.ScreenBrightness.Category"); }
        }

        public bool IsAction
        {
            get { return true; }
        }

        #endregion

        #region Public Methods


        public void Initialize()
        {

        }
        public bool Gestured(PointInfo ActionPoint)
        {
            return AdjustBrightness(_Settings);
        }

        public bool Deserialize(string SerializedData)
        {
            return PluginHelper.DeserializeSettings(SerializedData, out _Settings);
        }

        public string Serialize()
        {
            if (_GUI != null)
                _Settings = _GUI.Settings;

            if (_Settings == null)
                _Settings = new BrightnessSettings();

            return PluginHelper.SerializeSettings(_Settings);
        }

        #endregion

        #region Private Methods

        private ScreenBrightnessUI CreateGUI()
        {
            ScreenBrightnessUI newGUI = new ScreenBrightnessUI();

            newGUI.Loaded += (o, e) =>
            {
                TypedGUI.Settings = _Settings;
                TypedGUI.HostControl = HostControl;
            };

            return newGUI;
        }

        private string GetDescription(BrightnessSettings Settings)
        {
            if (Settings == null)
                return LocalizationProvider.Instance.GetTextValue("CorePlugins.ScreenBrightness.Name");

            // Create string to store final output description
            string strOutput = "";

            // Build output string
            switch (Settings.Method)
            {
                case 0:
                    strOutput = LocalizationProvider.Instance.GetTextValue("CorePlugins.ScreenBrightness.IncreaseBrightness") + Settings.Percent;
                    break;
                case 1:
                    strOutput = LocalizationProvider.Instance.GetTextValue("CorePlugins.ScreenBrightness.DecreaseBrightness") + Settings.Percent;
                    break;
            }

            return strOutput;
        }

        private bool AdjustBrightness(BrightnessSettings Settings)
        {
            if (Settings == null)
                return false;


            try
            {
                int currentBrightness = GetBrightness();
                switch ((Method)_Settings.Method)
                {
                    case Method.BrightnessUp:
                        SetBrightness((byte)((currentBrightness + Settings.Percent) > 100 ? 100 : currentBrightness + Settings.Percent));
                        break;
                    case Method.BrightnessDown:
                        SetBrightness((byte)((currentBrightness - Settings.Percent) < 0 ? 0 : currentBrightness - Settings.Percent));
                        break;
                }

                return true;
            }
            catch
            {
                //MessageBox.Show("Could not change volume settings.", "Volume Change Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        //get the actual percentage of brightness
        static int GetBrightness()
        {
            //define scope (namespace)
            ManagementScope s = new ManagementScope("root\\WMI");

            //define query
            SelectQuery q = new SelectQuery("WmiMonitorBrightness");

            //output current brightness
            ManagementObjectSearcher mos = new ManagementObjectSearcher(s, q);

            ManagementObjectCollection moc = mos.Get();

            //store result
            byte curBrightness = 0;
            foreach (ManagementObject o in moc)
            {
                curBrightness = (byte)o.GetPropertyValue("CurrentBrightness");
                break; //only work on the first object
            }

            moc.Dispose();
            mos.Dispose();

            return (int)curBrightness;
        }

        //array of valid brightness values in percent
        static byte[] GetBrightnessLevels()
        {
            //define scope (namespace)
            ManagementScope s = new ManagementScope("root\\WMI");

            //define query
            SelectQuery q = new SelectQuery("WmiMonitorBrightness");

            //output current brightness
            ManagementObjectSearcher mos = new ManagementObjectSearcher(s, q);
            byte[] BrightnessLevels = new byte[0];

            try
            {
                ManagementObjectCollection moc = mos.Get();

                //store result


                foreach (ManagementObject o in moc)
                {
                    BrightnessLevels = (byte[])o.GetPropertyValue("Level");
                    break; //only work on the first object
                }

                moc.Dispose();
                mos.Dispose();

            }
            catch (Exception)
            {
                // MessageBox.Show("Sorry, Your System does not support this brightness control...");

            }

            return BrightnessLevels;
        }

        static void SetBrightness(byte targetBrightness)
        {
            //define scope (namespace)
            ManagementScope s = new ManagementScope("root\\WMI");

            //define query
            SelectQuery q = new SelectQuery("WmiMonitorBrightnessMethods");

            //output current brightness
            ManagementObjectSearcher mos = new ManagementObjectSearcher(s, q);

            ManagementObjectCollection moc = mos.Get();

            foreach (ManagementObject o in moc)
            {
                o.InvokeMethod("WmiSetBrightness", new Object[] { UInt32.MaxValue, targetBrightness }); //note the reversed order - won't work otherwise!
                break; //only work on the first object
            }

            moc.Dispose();
            mos.Dispose();
        }
        #endregion

        #region Host Control

        public IHostControl HostControl { get; set; }

        #endregion
    }
}
