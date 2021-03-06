﻿using System;
using BSG.CameraEffects;
using Citadel.Data;
using Citadel.Options;
using Citadel.Utils;
using EFT;
using EFT.Animations;
using EFT.Ballistics;
using EFT.Interactive;
using EFT.InventoryLogic;
using EFT.Weather;
using UnityEngine;

namespace Citadel.Features
{
    class Misc : MonoBehaviour
    {
        private string _hud = string.Empty;
        public static bool NotHooked = true;
        public static TestHook BulletPenetrationHook;
        private Vector3 _tempPosition;

        private void FixedUpdate()
        {
            try
            {
                if (Main.CanUpdate)
                {
                    DoThermalVision();
                    DoNightVison();
                    NoRecoil();
                    NoSway();
                    MaxSkills();
                    NoVisor();
                    InfiniteStamina();
                    SpeedHack();
                    UnlockDoors();
                    FlyHack();
                    AlwaysAutomatic();
                    PrepareHud();
                    DontMoveWeaponCloser();
                    BulletPenetration();
                    FullBrightUpdate();
                    FullBrightCreateObject();
                    AlwaysRunning();
                    SuperJump();
                    HotKeys();
                    ChangeWeather();
                    Test();
                }
            }
            catch
            {

            }
        }

        private void Test()
        {
        }

        private void ChangeWeather()
        {
            if (Input.GetKeyDown(KeyCode.Home))
            {
                TOD_Sky.Instance.Components.Time.GameDateTime = null;
                TOD_Sky Sky_Obj = (TOD_Sky)FindObjectOfType(typeof(TOD_Sky));
                WeatherController.Instance.RainController.SetIntensity(0f);
                WeatherController.Instance.GlobalFogOvercast = 0f;
                WeatherController.Instance.LightningSummonBandWidth = 0f;
                WeatherController.Instance.RainController.enabled = false;
                Sky_Obj.Cycle.Hour = 12f;
            }

        }

        private void HotKeys()
        {
            if (Input.GetKeyDown(PlayerOptions.TogglePlayerEsp))
                PlayerOptions.DrawPlayers = !PlayerOptions.DrawPlayers;

            if (Input.GetKeyDown(PlayerOptions.ToggleScavEsp))
                PlayerOptions.DrawScavs = !PlayerOptions.DrawScavs;

            if (Input.GetKeyDown(MiscVisualsOptions.ToggleItemEsp))
                MiscVisualsOptions.DrawItems = !MiscVisualsOptions.DrawItems;

            if (Input.GetKeyDown(MiscVisualsOptions.ToggleContainerEsp))
                MiscVisualsOptions.DrawLootableContainers = !MiscVisualsOptions.DrawLootableContainers;

            if (Input.GetKeyDown(MiscOptions.ThermalVisonKey))
                MiscOptions.ThermalVison = !MiscOptions.ThermalVison;

            if (Input.GetKeyDown(MiscOptions.NightVisonKey))
                MiscOptions.NightVison = !MiscOptions.NightVison;
        }

        private void AlwaysRunning()
        {
            if (MiscOptions.AlwaysSprint)
            {
                foreach (Player.ESpeedLimit speedlimit in Enum.GetValues(typeof(EFT.Player.ESpeedLimit)))
                {
                    if (speedlimit != Player.ESpeedLimit.SurfaceNormal)
                    {
                        Main.LocalPlayer.RemoveStateSpeedLimit(speedlimit);
                    }
                }
            }
        }

        private void SuperJump()
        {
            if (MiscOptions.SuperJump)
            {
                Main.LocalPlayer.Skills.StrengthBuffJumpHeightInc.Value = MiscOptions.SuperJumpValue;
            }
        }

        private void FullBrightCreateObject()
        {
            if (!GameFullBright.LightCalled && GameFullBright.Enabled)
            {
                GameFullBright.LightGameObject = new GameObject("Fullbright");
                GameFullBright.FullBrightLight = GameFullBright.LightGameObject.AddComponent<Light>();
                GameFullBright.FullBrightLight.color = new Color(1f, 0.96f, 0.91f);
                GameFullBright.FullBrightLight.range = 2000f;
                GameFullBright.FullBrightLight.intensity = 0.6f;
                GameFullBright.LightCalled = true;
            }
        }

        private void FullBrightUpdate()
        {

            if (MiscOptions.ForceLight)
            {
                GameFullBright.Enabled = true;

                if (GameFullBright.FullBrightLight == null)
                    return;

                _tempPosition = Main.LocalPlayer.Transform.position;
                _tempPosition.y += .2f;
                GameFullBright.LightGameObject.transform.position = _tempPosition;
            }
            else
            {
                if (GameFullBright.FullBrightLight != null)
                    Destroy(GameFullBright.FullBrightLight);
                GameFullBright.LightCalled = false;
            }

            //if (MiscOptions.ForceLight)
            //{
            //    RenderSettings.fog = false;
            //    RenderSettings.ambientLight = Color.white;
            //}
            //else
            //{
            //    RenderSettings.fog = true;
            //    RenderSettings.ambientLight = _ambientColor;
            //}

        }

        private void BulletPenetration()
        {
            if (MiscOptions.BulletPenetration && NotHooked && Main.LocalPlayer.HandsController.Item is Weapon)
            {
                BulletPenetrationHook = new TestHook();
                BulletPenetrationHook.Init(typeof(BallisticsCalculator).GetMethod("GetAmmoPenetrationPower"), typeof(HookObject).GetMethod("BulletPenetration"));
                BulletPenetrationHook.Hook();
                NotHooked = false;
            }
        }

        private void DontMoveWeaponCloser()
        {
            if (MiscOptions.DontMoveWeaponCloser)
            {
                if (Main.LocalPlayer.HandsController.Item is Weapon)
                {
                    Main.LocalPlayer.ProceduralWeaponAnimation.Mask = EProceduralAnimationMask.ForceReaction;
                }
            }
        }

        private void PrepareHud()
        {
            if (MiscOptions.DrawHud)
            {
                string tempMag = string.Empty;

                if (Main.LocalPlayer.HandsController.Item is Weapon)
                {
                    var weapon = Main.LocalPlayer.Weapon;
                    var mag = weapon.GetCurrentMagazine();
                    if (mag != null)
                    {
                        tempMag = $"{mag.Count}+{weapon.ChamberAmmoCount}/{mag.MaxCount}";
                    }
                }
                else
                {
                    tempMag = "unkown";
                }

                string tempHealth = $"{Main.LocalPlayer.HealthController.GetBodyPartHealth(EBodyPart.Common, true).Current} / {Main.LocalPlayer.HealthController.GetBodyPartHealth(EBodyPart.Common, true).Maximum}";
                _hud = $"HP: {tempHealth} Ammo: {tempMag}";
            }

        }

        private void AlwaysAutomatic()
        {
            if (MiscOptions.AlwaysAutomatic && Main.LocalPlayer.HandsController.Item is Weapon)
            {
                Main.LocalPlayer.Weapon.GetItemComponent<FireModeComponent>().FireMode = Weapon.EFireMode.fullauto;
                Main.LocalPlayer.GetComponent<Player.FirearmController>().Item.Template.BoltAction = false;
                Main.LocalPlayer.GetComponent<Player.FirearmController>().Item.Template.bFirerate = 1000;
            }
        }

        private void FlyHack()
        {
            if (Input.GetKey(MiscOptions.FlyHackKey) && MiscOptions.FlyHack)
            {
                Main.LocalPlayer.MovementContext.IsGrounded = true;
                Main.LocalPlayer.MovementContext.FreefallTime = -0.2f;
            }
        }

        private void UnlockDoors()
        {
            if (MiscOptions.DoorUnlocker)
            {
                if (Input.GetKeyDown(MiscOptions.DoorUnlockerKey))
                {
                    foreach (var door in LocationScene.GetAllObjects<WorldInteractiveObject>())
                    {
                        if (door.DoorState == EDoorState.Open || door.DoorState == EDoorState.Shut || Vector3.Distance(door.transform.position, Main.LocalPlayer.Position) > 10f)
                            continue;

                        door.DoorState = EDoorState.Shut;
                        break;
                    }
                }
            }
        }

        private void SpeedHack()
        {
            if (MiscOptions.SpeedHack)
            {
                if (Input.GetKey(KeyCode.W))
                    Main.LocalPlayer.Transform.position += Main.LocalPlayer.Transform.forward / 5f * MiscOptions.SpeedHackValue;
                if (Input.GetKey(KeyCode.S))
                    Main.LocalPlayer.Transform.position -= Main.LocalPlayer.Transform.forward / 5f * MiscOptions.SpeedHackValue;
                if (Input.GetKey(KeyCode.A))
                    Main.LocalPlayer.Transform.position -= Main.LocalPlayer.Transform.right / 5f * MiscOptions.SpeedHackValue;
                if (Input.GetKey(KeyCode.D))
                    Main.LocalPlayer.Transform.position += Main.LocalPlayer.Transform.right / 5f * MiscOptions.SpeedHackValue;
            }
        }

        private void InfiniteStamina()
        {
            if (MiscOptions.InfiniteStamina)
            {
                Main.LocalPlayer.Physical.StaminaParameters.AimDrainRate = 0f;
                Main.LocalPlayer.Physical.StaminaParameters.SprintDrainRate = 0f;
                Main.LocalPlayer.Physical.StaminaParameters.JumpConsumption = 0f;
                Main.LocalPlayer.Physical.StaminaParameters.ExhaustedMeleeSpeed = 10000f;
            }
        }

        private void NoVisor()
        {
            Main.Camera.GetComponent<VisorEffect>().Intensity = MiscOptions.NoVisor ? 0f : 1f;
        }

        private void MaxSkills()
        {
            if (MiscOptions.MaxSkills)
            {
                foreach (var skill in Main.LocalPlayer.Skills.Skills)
                {
                    if (!skill.IsEliteLevel)
                        skill.SetLevel(51);
                }
            }
        }

        private void NoSway()
        {
            try
            {
                if (Main.LocalPlayer.Weapon != null)
                {
                    if (MiscOptions.NoSway)
                    {
                        Main.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity = 0f;
                        Main.LocalPlayer.ProceduralWeaponAnimation.WalkEffectorEnabled = false;
                        Main.LocalPlayer.ProceduralWeaponAnimation.Walk.Intensity = 0f;
                        Main.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = 0f;
                        Main.LocalPlayer.ProceduralWeaponAnimation.ForceReact.Intensity = 0f;
                    }
                    else
                    {
                        Main.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity = 1f;
                        Main.LocalPlayer.ProceduralWeaponAnimation.WalkEffectorEnabled = true;
                        Main.LocalPlayer.ProceduralWeaponAnimation.Walk.Intensity = 1f;
                        Main.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = 1f;
                        Main.LocalPlayer.ProceduralWeaponAnimation.ForceReact.Intensity = 1f;
                    }
                }
            }
            catch { }
        }

        private void NoRecoil()
        {
            try
            {
                if (MiscOptions.NoRecoil)
                {
                    Main.LocalPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthXy = Vector2.zero;
                    Main.LocalPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthZ = Vector2.zero;
                }
            }
            catch { }
        }

        private void DoNightVison()
        {
            Main.Camera.GetComponent<NightVision>().SetPrivateField("_on", MiscOptions.NightVison);
        }

        private void DoThermalVision()
        {
            Main.Camera.GetComponent<ThermalVision>().On = MiscOptions.ThermalVison;
        }

        private void OnGUI()
        {
            try
            {
                if (Main.CanUpdate)
                {
                    DrawHud();
                    DrawCrossHair();
                }

            }
            catch { }
        }

        private void DrawHud()
        {
            if (MiscOptions.DrawHud && Main.CanUpdate)
            {
                Render.DrawString(new Vector2(512, Screen.height - 78), $"{Main.ClosePlayers} Players closer than 50m", Color.white, false, 20);
                Render.DrawString(new Vector2(512, Screen.height - 56), _hud, Color.white, false, 20);
            }
        }

        private static void DrawCrossHair()
        {
            if (MiscVisualsOptions.DrawCrossHair)
            {
                Render.DrawLine(new Vector2(Screen.width / 2, Screen.height / 2 - 9), new Vector2(Screen.width / 2, Screen.height / 2 + 9), MiscVisualsOptions.CrossHairColor, 0.5f,
                    true);
                Render.DrawLine(new Vector2(Screen.width / 2 - 9, Screen.height / 2), new Vector2(Screen.width / 2 + 9, Screen.height / 2), MiscVisualsOptions.CrossHairColor, 0.5f,
                    true);
            }
            //else
            //{
            //    Vector3 crosshair = RayCast.BarrelRayCast(Main.LocalPlayer);
            //    Vector3 screenPos = Main.Camera.WorldToScreenPoint(crosshair);
            //    Render.DrawTextOutline(screenPos, "X", Color.black, Color.red );
            //}
        }
    }
}
