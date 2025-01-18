# UES Addresser
 
UES Addresser extends [Smart Addresser](https://github.com/CyberAgentGameEntertainment/SmartAddresser) with new tools specific to Risk of Rain 2 modding. Smart Addresser must be installed alongside this package.

## Key Features

- A Smart Addresser implementation of the RoR2 labels system
- An Addressables Group Template tuned for RoR2 mods
- A label provider for Content Packs
- A RoR2-style address provider (omits the Assets/ folder)
- A new component-based asset filter (used internally)

## RoR2 Labels

RoR2 organizes scriptable objects and prefabs with addressables labels. You can find a comprehensive list of these labels at `RoR2.ContentManagement.AddressablesLabels`. If your assets are properly labeled, you can use the same content loading systems as RoR2 (See `RoR2.ContentManagement.AddressablesLoadHelper.AddContentPackLoadOperationWithYields`).

There are two ways to add the RoR2 label rules to a Smart Addresser LayoutRuleData:

- Set a Primary LayoutRuleData in the Smart Addresser project settings, then select `Tools/UES Addresser/Create RoR2 Label Rules/Primary Layout Rule Data` from the main menu.
- Inspect any LayoutRuleData and select `Create RoR2 Label Rules` from its context menu.

If you set a Primary LayoutRuleData, the new label rules will be applied automatically. Otherwise, you must apply them manually through Smart Addresser.

## Addressables Group Template

To install the included Addressables Group Template, select `Tools/UES Addresser/Install Addressables Group Template` from the main menu. This "Mod Assets" template will replace the default "Packed Assets" template. After installation, you may freely create new Addressables Groups based on the "Mod Assets" template.