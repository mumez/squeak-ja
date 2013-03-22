
"Install font"
translationFile := 'ja-20121008.translation'.
fontFile := 'uJapaneseFont.out'.
formTranslator := 'formTranslator_ja.bin'.

CurrentJEISarInstaller extractMember: translationFile.
CurrentJEISarInstaller extractMember: fontFile.
CurrentJEISarInstaller extractMember: formTranslator.

(NaturalLanguageTranslator isoLanguage: 'ja') loadFromFileNamed: translationFile.
(Smalltalk at: #TransJa ifAbsentPut: [OrderedCollection new]) add: translationFile.

StrikeFontSet installExternalFontFileName6: fontFile encoding: JapaneseEnvironment leadingChar encodingName: #Japanese textStyleName: #DefaultMultiStyle.

stream := FileStream oldFileNamed: formTranslator.
[NaturalLanguageFormTranslator loadFormsFrom: stream]
    ensure: [stream close].

CurrentStVersion >= 4.4 ifTrue: [InternalTranslator mergeLegacyTranslators].

Locale currentPlatform: (Locale isoLanguage: 'ja').
StrikeFont setupDefaultFallbackFont.

"Set defaults"
Locale switchToID: (LocaleID isoLanguage: 'ja').
Preferences restoreDefaultFonts.
StrikeFont setupDefaultFallbackFont.
Project current updateLocaleDependents.

"Clean up"
targetDir := FileDirectory default.
targetDir deleteFileNamed: translationFile.
targetDir deleteFileNamed: fontFile.
targetDir deleteFileNamed: formTranslator.

!
