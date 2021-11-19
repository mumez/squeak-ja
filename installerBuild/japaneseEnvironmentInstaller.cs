"Extracting"
translationFile := 'ja-20211111.translation'.
formTranslator := 'formTranslator_ja.bin'.
fontFile := 'uJapaneseFont.out'.
CurrentJEISarInstaller extractMember: translationFile.
CurrentJEISarInstaller extractMember: formTranslator.
CurrentJEISarInstaller extractMember: fontFile.

"Install Translations"
(NaturalLanguageTranslator isoLanguage: 'ja') loadFromFileNamed: translationFile.
(Smalltalk at: #TransJa ifAbsentPut: [OrderedCollection new]) add: translationFile.

stream := FileStream oldFileNamed: formTranslator.
[NaturalLanguageFormTranslator loadFormsFrom: stream]
    ensure: [stream close].

CurrentStVersion >= 4.4 ifTrue: [InternalTranslator mergeLegacyTranslators].

"Install bitmap font"
StrikeFontSet installExternalFontFileName6: fontFile encoding: JapaneseEnvironment leadingChar encodingName: #Japanese textStyleName: #DefaultMultiStyle.

"Set defaults"
jpLocale := Locale isoLanguage: 'ja' isoCountry: 'JP'.
Locale currentPlatform: jpLocale.
Locale switchToID: jpLocale.
StrikeFont setupDefaultFallbackFont. 
Project current updateLocaleDependents.

"Clean up"
targetDir := FileDirectory default.
targetDir deleteFileNamed: translationFile.
targetDir deleteFileNamed: fontFile.
targetDir deleteFileNamed: formTranslator.

!
