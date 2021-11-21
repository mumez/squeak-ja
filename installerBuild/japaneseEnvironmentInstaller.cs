"Extracting"
translationFile := 'ja-20211111.translation'.
formTranslator := 'formTranslator_ja.bin'.
fontFile := 'uJapaneseFont.out'.
CurrentJEISarInstaller extractMember: translationFile.
CurrentJEISarInstaller extractMember: formTranslator.
CurrentJEISarInstaller extractMember: fontFile.

jpLocale := Locale isoLanguage: 'ja'.

"Install Translations"
(NaturalLanguageTranslator localeID: jpLocale localeID) loadFromFileNamed: translationFile.
(Smalltalk at: #TransJa ifAbsentPut: [OrderedCollection new]) add: translationFile.

stream := FileStream oldFileNamed: formTranslator.
[NaturalLanguageFormTranslator loadFormsFrom: stream]
    ensure: [stream close].

CurrentStVersion >= 4.4 ifTrue: [InternalTranslator mergeLegacyTranslators].

"Install bitmap font"
StrikeFontSet installExternalFontFileName6: fontFile encoding: JapaneseEnvironment leadingChar encodingName: #Japanese textStyleName: #DefaultMultiStyle.

"Set defaults"
Locale currentPlatform: jpLocale.
Locale switchToID: jpLocale localeID.
StrikeFont setupDefaultFallbackFont.
Project current updateLocaleDependents.
Flaps disableGlobalFlaps: false.

"Clean up"
targetDir := FileDirectory default.
targetDir deleteFileNamed: translationFile.
targetDir deleteFileNamed: fontFile.
targetDir deleteFileNamed: formTranslator.

!
