base := 'https://raw.githubusercontent.com/mumez/squeak-ja/master/translations'.
version := #('3.8' '3.9' '3.10' '4.1' '4.2' '4.3' '4.4' '5.3') detect: [:each | '*',each,'*' match: Smalltalk version] ifNone: ['4.4'].
listFileName := 'TransList{1}-ja.txt' format: {version}.
trans := Array readFrom: ((base, '/', listFileName) asUrl retrieveContents content).
installs := (Smalltalk at: #TransJa ifAbsentPut: [OrderedCollection new]) asArray.
trans := trans difference: installs.
trans do: [:each | | conts |
	conts := (base, '/', each) asUrl retrieveContents contents contents.
	(version asNumber < 5) ifTrue: [conts := conts convertFromWithConverter: (UTF8TextConverter new)].
	(NaturalLanguageTranslator localeID: (LocaleID isoString: 'ja')) loadFromStream: conts readStream.
	TransJa add: each.
].
!