﻿using BarboraTimeCheck.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BarboraTimeCheck.Services
{
    public class SettingsService
    {
        private Settings settings;
        private readonly string SettingsFileName = "settings.json";
        public SettingsService()
        {
            CheckSettingsFile();
        }

        public Settings GetSettings()
        {
            return settings;
        }

        public void UpdateCommonInformation(int checkInterval, bool emailNotifications, bool pushNotifications)
        {
            settings.CheckInterval = checkInterval;
            settings.EmailNotifications = emailNotifications;
            settings.PushNotifications = pushNotifications;
            WriteSettingsToFile();
        }

        public void UpdateEmailInformation(string emailUsername, string emailPassword, string emailSmtp, string deliveryEmail)
        {
            settings.EmailUsername = emailUsername;
            settings.EmailPassword = emailPassword;
            settings.EmailSmtpServer = emailSmtp;
            settings.DeliveryEmail = deliveryEmail;
        }

        public void UpdateAuthInformation(string username, string password, string authCookie)
        {
            settings.Username = username;
            settings.Password = password;
            settings.AuthCookie = authCookie;
            WriteSettingsToFile();
        }

        public bool AuthenticationExists()
        {
            return !string.IsNullOrEmpty(settings.Username) && !string.IsNullOrEmpty(settings.Password) && !string.IsNullOrEmpty(settings.AuthCookie);
        }

        public string GetAuthCookie()
        {
            return settings.AuthCookie;
        }

        private void CheckSettingsFile()
        {
            if (File.Exists(SettingsFileName))
            {
                var jsonText = File.ReadAllText(SettingsFileName);
                settings = JsonConvert.DeserializeObject<Settings>(jsonText);
            }
            else
            {
                settings = CreateDefaultSettings();
                WriteSettingsToFile();
            }
        }

        private void WriteSettingsToFile()
        {
            var jsonText = JsonConvert.SerializeObject(settings);
            File.WriteAllText(SettingsFileName, jsonText);
        }

        private Settings CreateDefaultSettings()
        {
            return new Settings
            {
                CheckInterval = 30,
                EmailNotifications = false,
                PushNotifications = true,
                EmailUsername = "barboratest123@gmail.com",
                EmailPassword = "naujas11",
                EmailSmtpServer = "smtp.gmail.com"
            };
        }
    }
}