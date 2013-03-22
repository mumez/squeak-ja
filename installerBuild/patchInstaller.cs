"Extra Patches"
base := 'patches'.
version := #('3.8' '3.9' '3.10' '4.1' '4.2' '4.3' '4.4') detect: [:each | '*',each,'*' match: Smalltalk version] ifNone: ['3.8'].
(version beginsWith: '3.') ifTrue: [(Smalltalk confirm: 'Install localization patches?') ifFalse: [^nil]].

patchListFileName := 'PatchList{1}-ja.txt' format: {version}.
patches := Array readFrom: ((CurrentJEISarInstaller memberNamed:(base, '/', patchListFileName) contents) contents).
installs := (Smalltalk at: #PatchesJa ifAbsentPut: [OrderedCollection new]) asArray.
patches := patches difference: installs.
patches do: [:each |
	SARInstaller new fileInFrom: (CurrentJEISarInstaller memberNamed: (base, '/', each)) contentStream.
	(PatchesJa includes: each) ifFalse: [PatchesJa add: each].
].
!