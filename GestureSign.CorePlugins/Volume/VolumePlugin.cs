﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using GestureSign.Common.Localization;
using GestureSign.Common.Plugins;

namespace GestureSign.CorePlugins.Volume
{
    public class VolumePlugin : IPlugin
    {
        #region Private Variables

        private Volume _GUI = null;
        private VolumeSettings _settings = null;

        private enum Method
        {
            VolumeUp = 0,
            VolumeDown = 1,
            Mute = 2
        }

        #endregion

        #region Public Properties

        public string Name
        {
            get { return LocalizationProvider.Instance.GetTextValue("CorePlugins.Volume.Name"); }
        }

        public string Description
        {
            get { return GetDescription(_settings); }
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

        public Volume TypedGUI
        {
            get { return (Volume)GUI; }
        }

        public string Category
        {
            get { return LocalizationProvider.Instance.GetTextValue("CorePlugins.Volume.Category"); }
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
            return AdjustVolume(_settings);
        }

        public bool Deserialize(string SerializedData)
        {
            return PluginHelper.DeserializeSettings(SerializedData, out _settings);
        }

        public string Serialize()
        {
            if (_GUI != null)
                _settings = _GUI.Settings;

            if (_settings == null)
                _settings = new VolumeSettings();

            return PluginHelper.SerializeSettings(_settings);
        }

        #endregion

        #region Private Methods

        private Volume CreateGUI()
        {
            Volume newGUI = new Volume();

            newGUI.Loaded += (o, e) =>
            {
                TypedGUI.Settings = _settings;
                TypedGUI.HostControl = HostControl;
            };

            return newGUI;
        }

        private string GetDescription(VolumeSettings Settings)
        {
            if (Settings == null)
                return LocalizationProvider.Instance.GetTextValue("CorePlugins.Volume.Name");

            // Create string to store final output description
            string strOutput = "";

            // Build output string
            switch (Settings.Method)
            {
                case 0:
                    strOutput = LocalizationProvider.Instance.GetTextValue("CorePlugins.Volume.Increase") + " by " + Settings.Percent;
                    break;
                case 1:
                    strOutput = LocalizationProvider.Instance.GetTextValue("CorePlugins.Volume.Decrease") + " by " + Settings.Percent;
                    break;
                case 2:
                    strOutput = LocalizationProvider.Instance.GetTextValue("CorePlugins.Volume.Mute");
                    break;
            }

            return strOutput;
        }

        private bool AdjustVolume(VolumeSettings Settings)
        {
            if (Settings == null)
                return false;


            try
            {
                switch ((Method)_settings.Method)
                {
                    case Method.VolumeUp:
                        ChangeVolume(Method.VolumeUp);
                        break;
                    case Method.VolumeDown:
                        ChangeVolume(Method.VolumeDown);
                        break;
                    case Method.Mute:
                        SetMute();
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
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
        const uint WM_APPCOMMAND = 0x319;
        const uint APPCOMMAND_VOLUME_UP = 0x0a;
        const uint APPCOMMAND_VOLUME_DOWN = 0x09;
        const uint APPCOMMAND_VOLUME_MUTE = 0x08;
        private void ChangeVolume(Method ChangeMethod)
        {
            Process p = Process.GetCurrentProcess();
            int t = _settings.Percent / 2;
            if (ChangeMethod == Method.VolumeUp)
            {
                for (int i = 0; i < t; i++)
                {
                    //加音量               
                    SendMessage(HostControl.TouchCapture.MessageWindowHandle, WM_APPCOMMAND, 0x30292, APPCOMMAND_VOLUME_UP * 0x10000);
                }

            }
            else
            {
                for (int i = 0; i < t; i++)
                {
                    SendMessage(HostControl.TouchCapture.MessageWindowHandle, WM_APPCOMMAND, 0x30292, APPCOMMAND_VOLUME_DOWN * 0x10000);
                }
            }
        }

        private void SetMute()
        {
            // System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
            //  if (p.MainWindowHandle == IntPtr.Zero)
            SendMessage(HostControl.TouchCapture.MessageWindowHandle, WM_APPCOMMAND, 0x200eb0, APPCOMMAND_VOLUME_MUTE * 0x10000);
            //else SendMessage(p.MainWindowHandle, WM_APPCOMMAND, 0x200eb0, APPCOMMAND_VOLUME_MUTE * 0x10000);
        }

        #endregion

        #region Host Control

        public IHostControl HostControl { get; set; }

        #endregion
    }
}
