﻿using SivaEftCheat.Options;
using UnityEngine;

namespace SivaEftCheat
{
    public class Menu : MonoBehaviour
    {
        private Rect _mainWindow;
        private Rect _playerVisualWindow;
        private Rect _itemVisuals;
        private Rect _aimbotVisualWindow;
        private Rect _miscFeatureslVisualWindow;
        private bool _visible = true;

        private bool _playerEspVisualVisible;
        private bool _miscVisualVisible;
        private bool _aimbotVisualVisible;
        private bool _miscFeatureslVisible;

        private string watermark = "TESTING";

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 150f);
            _playerVisualWindow = new Rect(275f, 60f, 350f, 150f);
            _itemVisuals = new Rect(530f, 60f, 350f, 150f);
            _aimbotVisualWindow = new Rect(785f, 60f, 350f, 150f);
            _miscFeatureslVisualWindow = new Rect(1040f, 60f, 350f, 150f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _visible = !_visible;
        }

        private void OnGUI()
        {
            if (!_visible)
                return;

            _mainWindow = GUILayout.Window(0, _mainWindow, RenderUi, watermark);

            if (_playerEspVisualVisible)
                _playerVisualWindow = GUILayout.Window(1, _playerVisualWindow, RenderUi, "Player Visual");
            if (_miscVisualVisible)
                _itemVisuals = GUILayout.Window(2, _itemVisuals, RenderUi, "Miscellaneous Visual");
            if (_aimbotVisualVisible)
                _aimbotVisualWindow = GUILayout.Window(3, _aimbotVisualWindow, RenderUi, "Aimbot");
            if (_miscFeatureslVisible)
                _miscFeatureslVisualWindow = GUILayout.Window(4, _miscFeatureslVisualWindow, RenderUi, "Miscellaneous");
        }

        private void RenderUi(int id)
        {

            switch (id)
            {
                case 0:
                    if (GUILayout.Button("Player Visuals"))
                        _playerEspVisualVisible = !_playerEspVisualVisible;
                    if (GUILayout.Button("Miscellaneous Visuals"))
                        _miscVisualVisible = !_miscVisualVisible;
                    if (GUILayout.Button("Aimbot"))
                        _aimbotVisualVisible = !_aimbotVisualVisible;
                    if (GUILayout.Button("Miscellaneous"))
                        _miscFeatureslVisible = !_miscFeatureslVisible;
                    break;

                case 1:
                    GUILayout.Label("Players");
                    PlayerOptions.DrawPlayers = GUILayout.Toggle(PlayerOptions.DrawPlayers, "Draw Players");
                    PlayerOptions.DrawPlayerHealth = GUILayout.Toggle(PlayerOptions.DrawPlayerHealth, "Draw Player Health Bar");
                    PlayerOptions.DrawPlayerHealthBar = GUILayout.Toggle(PlayerOptions.DrawPlayerHealthBar, "Draw Player Health");
                    PlayerOptions.DrawPlayerName = GUILayout.Toggle(PlayerOptions.DrawPlayerName, "Draw Player Name");
                    PlayerOptions.DrawPlayerLevel = GUILayout.Toggle(PlayerOptions.DrawPlayerLevel, "Draw Player Level");
                    PlayerOptions.DrawPlayerWeapon = GUILayout.Toggle(PlayerOptions.DrawPlayerWeapon, "Draw Player Weapon");
                    PlayerOptions.DrawPlayerSnapLine = GUILayout.Toggle(PlayerOptions.DrawPlayerSnapLine, "Draw Player SnapLine");
                    PlayerOptions.DrawPlayerCornerBox = GUILayout.Toggle(PlayerOptions.DrawPlayerCornerBox, "Draw Player DrawBox");
                    PlayerOptions.DrawPlayerSkeleton = GUILayout.Toggle(PlayerOptions.DrawPlayerSkeleton, "Draw Player Skeleton");
                    PlayerOptions.DrawPlayerAimPos = GUILayout.Toggle(PlayerOptions.DrawPlayerAimPos, "Draw Player Aim Pos");
                    PlayerOptions.DrawPlayerDistance = GUILayout.Toggle(PlayerOptions.DrawPlayerDistance, "Draw Player Distance");
                    PlayerOptions.DrawPlayerValue = GUILayout.Toggle(PlayerOptions.DrawPlayerValue, "Draw Player Value");
                    PlayerOptions.DrawCorpses = GUILayout.Toggle(PlayerOptions.DrawCorpses, "Draw Player Corpses");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Player Distance {(int)PlayerOptions.DrawPlayerRange} m");
                    PlayerOptions.DrawPlayerRange = GUILayout.HorizontalSlider(PlayerOptions.DrawPlayerRange, 0f, 1000f);
                    GUILayout.EndHorizontal();

                    GUILayout.Space(20);
                    GUILayout.Label("Scavs");
                    PlayerOptions.DrawScavs = GUILayout.Toggle(PlayerOptions.DrawScavs, "Draw Scavs");
                    PlayerOptions.DrawScavHealthBar = GUILayout.Toggle(PlayerOptions.DrawScavHealthBar, "Draw ScavColor Health Bar");
                    PlayerOptions.DrawScavHealth = GUILayout.Toggle(PlayerOptions.DrawScavHealth, "Draw ScavColor Health");
                    PlayerOptions.DrawScavName = GUILayout.Toggle(PlayerOptions.DrawScavName, "Draw ScavColor Name");
                    PlayerOptions.DrawScavWeapon = GUILayout.Toggle(PlayerOptions.DrawScavWeapon, "Draw ScavColor Weapon");
                    PlayerOptions.DrawScavSnapLine = GUILayout.Toggle(PlayerOptions.DrawScavSnapLine, "Draw ScavColor SnapLine");
                    PlayerOptions.DrawScavCornerBox = GUILayout.Toggle(PlayerOptions.DrawScavCornerBox, "Draw ScavColor DrawBox");
                    PlayerOptions.DrawScavSkeleton = GUILayout.Toggle(PlayerOptions.DrawScavSkeleton, "Draw ScavColor Skeleton");
                    PlayerOptions.DrawScavAimPos = GUILayout.Toggle(PlayerOptions.DrawScavAimPos, "Draw ScavColor Aim Pos");
                    PlayerOptions.DrawScavValue = GUILayout.Toggle(PlayerOptions.DrawScavValue, "Draw ScavColor Value");
                    PlayerOptions.DrawScavDistance = GUILayout.Toggle(PlayerOptions.DrawScavDistance, "Draw ScavColor Distance");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"ScavColor Distance {(int)PlayerOptions.DrawScavsRange} m");
                    PlayerOptions.DrawScavsRange = GUILayout.HorizontalSlider(PlayerOptions.DrawScavsRange, 0f, 1000f);
                    GUILayout.EndHorizontal();
                    break;

                case 2:
                    MiscVisualsOptions.DrawItems = GUILayout.Toggle(MiscVisualsOptions.DrawItems, "Draw Items");
                    MiscVisualsOptions.DrawCommonItems = GUILayout.Toggle(MiscVisualsOptions.DrawCommonItems, "Draw Common Items");
                    MiscVisualsOptions.DrawRareItems = GUILayout.Toggle(MiscVisualsOptions.DrawRareItems, "Draw Rare Items");
                    MiscVisualsOptions.DrawSuperRareItems = GUILayout.Toggle(MiscVisualsOptions.DrawSuperRareItems, "Draw Super Rare Items");
                    MiscVisualsOptions.DrawMedtems = GUILayout.Toggle(MiscVisualsOptions.DrawMedtems, "Draw Meds Items");
                    MiscVisualsOptions.DrawSpecialItems = GUILayout.Toggle(MiscVisualsOptions.DrawSpecialItems, "Draw Special Items");
                    MiscVisualsOptions.DrawQuestItems = GUILayout.Toggle(MiscVisualsOptions.DrawQuestItems, "Draw Quest Items");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Item Distance {(int)MiscVisualsOptions.DrawItemRange} m");
                    MiscVisualsOptions.DrawItemRange = GUILayout.HorizontalSlider(MiscVisualsOptions.DrawItemRange, 0f, 1000f);
                    GUILayout.EndHorizontal();

                    GUILayout.Space(20);
                    GUILayout.Label("Container ESP ");
                    MiscVisualsOptions.DrawLootableContainers = GUILayout.Toggle(MiscVisualsOptions.DrawLootableContainers, "Draw Lootable Containers");
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Container Distance {(int)MiscVisualsOptions.DrawLootableContainersRange} m");
                    MiscVisualsOptions.DrawLootableContainersRange = GUILayout.HorizontalSlider(MiscVisualsOptions.DrawLootableContainersRange, 0f, 1000f);
                    GUILayout.EndHorizontal();

                    GUILayout.Space(20);
                    GUILayout.Label("Other Visuals");
                    MiscOptions.DrawHud = GUILayout.Toggle(MiscOptions.DrawHud, "Draw Hud");
                    MiscVisualsOptions.DrawExtractEsp = GUILayout.Toggle(MiscVisualsOptions.DrawExtractEsp, "Draw Extracts");
                    MiscVisualsOptions.DrawGrenadeEsp = GUILayout.Toggle(MiscVisualsOptions.DrawGrenadeEsp, "Draw Grenades");
                    MiscVisualsOptions.DrawCrossHair = GUILayout.Toggle(MiscVisualsOptions.DrawCrossHair, "Draw Crosshair");

                    break;

                case 3:
                    AimbotOptions.Aimbot = GUILayout.Toggle(AimbotOptions.Aimbot, "Aimbot");
                    AimbotOptions.DrawAimbotFov = GUILayout.Toggle(AimbotOptions.DrawAimbotFov, "Draw Aimbot Fov");
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Aimbot Fov {(int)AimbotOptions.AimbotFov} m");
                    AimbotOptions.AimbotFov = GUILayout.HorizontalSlider(AimbotOptions.AimbotFov, 0f, 800f);
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Aimbot Key: " + AimbotOptions.AimbotKey))
                        AimbotOptions.AimbotKey = KeyCode.None;

                    AimbotOptions.SilentAim = GUILayout.Toggle(AimbotOptions.SilentAim, "Silent Aim");
                    AimbotOptions.DrawSilentFov = GUILayout.Toggle(AimbotOptions.DrawSilentFov, "Draw Silent Aim Fov");
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Silent Aim Fov {(int)AimbotOptions.SilentAimFov} m");
                    AimbotOptions.SilentAimFov = GUILayout.HorizontalSlider(AimbotOptions.SilentAimFov, 0f, 800f);
                    GUILayout.EndHorizontal();

                    AimbotOptions.TargetSnapLine = GUILayout.Toggle(AimbotOptions.TargetSnapLine, "Target Snapline");
                    break;

                case 4:
                    MiscOptions.MaxSkills = GUILayout.Toggle(MiscOptions.MaxSkills, "Max Skills");
                    MiscOptions.InfiniteStamina = GUILayout.Toggle(MiscOptions.InfiniteStamina, "Infinite Stamina");
                    MiscOptions.SpeedHack = GUILayout.Toggle(MiscOptions.SpeedHack, "SpeedHack");
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Speed Hack: {MiscOptions.SpeedHackValue}");
                    MiscOptions.SpeedHackValue = GUILayout.HorizontalSlider(MiscOptions.SpeedHackValue, 0f, 1f);
                    GUILayout.EndHorizontal();
                    MiscOptions.DoorUnlocker = GUILayout.Toggle(MiscOptions.DoorUnlocker, $"DoorUnlocker: {MiscOptions.DoorUnlockerKey}");
                    MiscOptions.ThermalVison = GUILayout.Toggle(MiscOptions.ThermalVison, $"Thermal Vison: {MiscOptions.ThermalVisonKey}");
                    MiscOptions.NightVison = GUILayout.Toggle(MiscOptions.NightVison, $"Night Vison: {MiscOptions.NightVisonKey}");
                    MiscOptions.NoVisor = GUILayout.Toggle(MiscOptions.NoVisor, $"No Visor");
                    MiscOptions.FlyHack = GUILayout.Toggle(MiscOptions.FlyHack, $"Fly Hack: {MiscOptions.FlyHackKey}");

                    GUILayout.Space(20);
                    GUILayout.Label("Weapon Options");
                    MiscOptions.NoRecoil = GUILayout.Toggle(MiscOptions.NoRecoil, "No Recoil");
                    MiscOptions.NoSway = GUILayout.Toggle(MiscOptions.NoSway, "No Sway");
                    MiscOptions.AlwaysAutomatic = GUILayout.Toggle(MiscOptions.AlwaysAutomatic, "Always Automatic");
                    MiscOptions.BulletPenetration = GUILayout.Toggle(MiscOptions.BulletPenetration, "Bullet Penetration");
                    MiscOptions.InstantHit = GUILayout.Toggle(MiscOptions.InstantHit, "Instant Hit");
                    MiscOptions.DontMoveWeaponCloser = GUILayout.Toggle(MiscOptions.DontMoveWeaponCloser, "Don't move weapon closer.");
                    break;
            }
            GUI.DragWindow();

            if (AimbotOptions.AimbotKey == KeyCode.None)
            {
                Event e = Event.current;
                AimbotOptions.AimbotKey = e.keyCode;
            }
        }
    }
}
