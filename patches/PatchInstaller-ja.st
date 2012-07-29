base := 'http://squeak-ja.sourceforge.jp/patches'.
version := #('3.8' '3.9' '3.10' '4.1' '4.2' '4.3') detect: [:each | '*',each,'*' match: Smalltalk version] ifNone: ['3.8'].
patchListFileName := 'PatchList{1}-ja.txt' format: {version}.
patches := Array readFrom: ((base, '/', patchListFileName) asUrl retrieveContents content).
installed := (Smalltalk at: #PatchesJa ifAbsentPut: [OrderedCollection new]) asArray.
patches := patches difference: installed.
patches do: [:each |
	SARInstaller new fileInFrom: (base, '/', each) asUrl retrieveContents content asByteArray readStream.
	(PatchesJa includes: each) ifFalse: [PatchesJa add: each].
].
!