﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using UnityEngine;

namespace SivaEftCheat.Utils
{
    class GameUtils
    {
        private static readonly LayerMask LayerMask = 1 << 12 | 1 << 16 | 1 << 18;

        public static float GetColorAlpha(float distance, float distanceMax)
        {
            float colorAlpha = 1f;

            if (distance > 0)
            {
                float alphaCalc = (distance / distanceMax) * 0.5f;

                colorAlpha = colorAlpha - alphaCalc;
            }

            return colorAlpha;
        }

        public static bool IsPlayerAlive(Player player)
        {
            if (!IsPlayerValid(player))
                return false;

            if (player.HealthController == null)
                return false;

            return player.HealthController.IsAlive;
        }

        public static bool IsPlayerValid(Player player)
        {
            return player != null && player.Transform != null && player.PlayerBones != null && player.PlayerBones.transform != null;
        }

        public static Vector3 WorldPointToScreenPoint(Vector3 worldPoint)
        {
            Vector3 screenPoint = Main.Camera.WorldToScreenPoint(worldPoint);

            screenPoint.y = Screen.height - screenPoint.y;

            return screenPoint;
        }
        public static float InPoint(Vector3 c1, Vector3 c2)
        {
            return (c1 - c2).sqrMagnitude;
        }
        public static bool IsScreenPointVisible(Vector3 screenPoint)
        {
            return screenPoint.z > 0.01f && screenPoint.x > -5f && screenPoint.y > -5f && screenPoint.x < Screen.width && screenPoint.y < Screen.height;
        }

        public static bool IsVisible(Vector3 Position)
        {
            return Physics.Linecast(Main.LocalPlayer.PlayerBones.Fireport.position, Position, out var raycastHit, LayerMask, QueryTriggerInteraction.UseGlobal) && raycastHit.transform.name.Contains("Human");
        }
    }
}
