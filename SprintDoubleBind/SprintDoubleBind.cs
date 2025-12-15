using BepInEx;
using BepInEx.Configuration;
using On.RoR2;
using R2API.Utils;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using RoR2;
using UnityEngine;

namespace SprintDoubleBind
{
    // Dependency to risk of options
    [BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    // This attribute is required, and lists metadata for your plugin.
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class SprintDoubleBind : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "kenculator";
        public const string PluginName = "SprintDoubleBind";
        public const string PluginVersion = "1.0.0";

        // Config Declarations
        public ConfigEntry<bool> SprintScrollUpEnabled {  get; set; }
        public ConfigEntry<bool> SprintScrollDownEnabled { get; set; }
        public ConfigEntry<bool> SprintKeyEnabled { get; set; }
        public ConfigEntry<KeyboardShortcut> SprintBindKey { get; set; }

        // Class Var
        private bool _keyPressedThisFrame = false;

        public void Awake()
        {
            // Init config
            SprintScrollUpEnabled = Config.Bind(
                "General",
                "SprintScrollUpEnabled",
                false,
                "Enables scrolling up as a secondary keybind for sprinting"
            );

            SprintScrollDownEnabled = Config.Bind(
                "General",
                "SprintScrollDownEnabled",
                true,
                "Enable scrolling down as a secondary keybind for sprinting"
            );

            SprintKeyEnabled = Config.Bind(
                "General",
                "SprintKeyEnabled",
                true,
                "Enables using a custom keybind as a secondary keybind for sprinting"
            );

            SprintBindKey = Config.Bind(
                "General",
                "SprintBindKey",
                new KeyboardShortcut(KeyCode.Mouse3), // Default value
                "The key/button that will toggle sprint if sprint key is enabled. Supports keyboard keys and mouse buttons"
            );

            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.rune580.riskofoptions"))
            {
                AddConfigOptions();
            }

            Logger.LogInfo("Configurable Sprint Mod initialized. Applying FixedUpdate Hook.");
            On.RoR2.PlayerCharacterMasterController.FixedUpdate += PlayerCharacterMasterController_FixedUpdate;
        }

        private void AddConfigOptions()
        {
            ModSettingsManager.AddOption(new CheckBoxOption(SprintScrollUpEnabled));
            ModSettingsManager.AddOption(new CheckBoxOption(SprintScrollDownEnabled));
            ModSettingsManager.AddOption(new CheckBoxOption(SprintKeyEnabled));
            ModSettingsManager.AddOption(new KeyBindOption(SprintBindKey, new KeyBindConfig() { checkIfDisabled = Check }));
        }

        private bool Check()
        {
            return !SprintKeyEnabled.Value;
        }

        public void Update()
        {
            // Only check input if a previous press hasn't been processed yet
            if (_keyPressedThisFrame)
            {
                return;
            }

            // input check

            //// Check for scroll
            Vector2 scrollDelta = Input.mouseScrollDelta;
            // Check for scroll down
            if (SprintScrollDownEnabled.Value && scrollDelta.y < 0f)
            {
                _keyPressedThisFrame = true;
            }
            // Check for scroll up
            if (SprintScrollUpEnabled.Value && scrollDelta.y > 0f)
            {
                _keyPressedThisFrame = true;
            }
            // Check for keyboard/mouse buttons
            if (SprintKeyEnabled.Value && UnityInput.Current.GetKeyDown(SprintBindKey.Value.MainKey))
            {
                _keyPressedThisFrame = true;
            }
        }

        private void PlayerCharacterMasterController_FixedUpdate(
            On.RoR2.PlayerCharacterMasterController.orig_FixedUpdate orig,
            RoR2.PlayerCharacterMasterController self)
        {
            orig(self);

            if (self.networkUser?.localUser != null && !self.networkUser.localUser.isUIFocused)
            {
                RoR2.CharacterBody body = self.body;
                RoR2.InputBankTest bodyInputs = self.bodyInputs;

                if (body != null && bodyInputs != null)
                {
                    // check flag for key input from Update()
                    if (_keyPressedThisFrame)
                    {
                        //// fetch if player is sprinting
                        //bool isSprinting = body.isSprinting;

                        //// toggle sprint
                        //RoR2.InputBankTest.ButtonState currentSprintState = bodyInputs.sprint;
                        //currentSprintState.down = !isSprinting;
                        //bodyInputs.sprint = currentSprintState;

                        self.sprintInputPressReceived = true;

                        // Reset the flag after processing the press.
                        _keyPressedThisFrame = false;
                    }
                }
            }
        }

        public void OnDestroy()
        {
            On.RoR2.PlayerCharacterMasterController.FixedUpdate -= PlayerCharacterMasterController_FixedUpdate;
        }
    }
}