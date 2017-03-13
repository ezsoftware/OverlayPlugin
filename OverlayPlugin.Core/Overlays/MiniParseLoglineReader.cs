﻿using Advanced_Combat_Tracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainbowMage.OverlayPlugin.Overlays
{
    partial class MiniParseOverlay
    {
        // Part of ACTWebSocket
        // Copyright (c) 2016 ZCube; Licensed under MIT license.
        public enum MessageType
        {
            LogLine = 0,
            ChangeZone = 1,
            ChangePrimaryPlayer = 2,
            AddCombatant = 3,
            RemoveCombatant = 4,
            AddBuff = 5,
            RemoveBuff = 6,
            FlyingText = 7,
            OutgoingAbility = 8,
            IncomingAbility = 10,
            PartyList = 11,
            PlayerStats = 12,
            CombatantHP = 13,
            NetworkStartsCasting = 20,
            NetworkAbility = 21,
            NetworkAOEAbility = 22,
            NetworkCancelAbility = 23,
            NetworkDoT = 24,
            NetworkDeath = 25,
            NetworkBuff = 26,
            NetworkTargetIcon = 27,
            NetworkRaidMarker = 28,
            NetworkTargetMarker = 29,
            NetworkBuffRemove = 30,
            Debug = 251,
            PacketDump = 252,
            Version = 253,
            Error = 254,
            Timer = 255
        }
        
        private void LogLineReader(bool isImported, LogLineEventArgs e)
        {
            string[] d = e.logLine.Split('|');

            if (d == null || d.Length < 2) // DataErr0r: null or 1-section
            {
                return;
            }

            MessageType type = (MessageType)Convert.ToInt32(d[0]);

            switch(type)
            {
                case MessageType.LogLine:
                    if (d.Length < 5) // Invalid
                    {
                        break;
                    }

                    if (Convert.ToInt32(d[2], 16) == 56) // type:echo
                    {
                        sendEchoEvent(d[4]);
                    }
                    break;
            }
        }

        private void sendEchoEvent(string text)
        {
            if (this.Overlay != null &&
                this.Overlay.Renderer != null &&
                this.Overlay.Renderer.Browser != null)
            {
                this.Overlay.Renderer.ExecuteScript(
                    "document.dispatchEvent(new CustomEvent('onEcho', { detail: { message: \"" + Util.CreateJsonSafeString(text) + "\" } } ));"
                );
            }
        }
    }
}