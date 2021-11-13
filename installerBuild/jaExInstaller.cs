"JaEx utilities"
matched := #('4.1' '4.2' '4.3' '4.4' '5.3') detect: [:each | '*',each,'*' match: Smalltalk version] ifNone: [].
matched isNil ifTrue: [Transcript cr; show: ('[JaEx] No version match: ', Smalltalk version). ^self].

CurrentJEISarInstaller fileInMemberNamed: 'ttfInstaller.cs'.

jaEx := 'JaEx-Squeak-MU.24.mcz'.
(matched notNil) ifTrue:[CurrentJEISarInstaller fileInMonticelloZipVersionNamed: jaEx].

2 timesRepeat: [ActiveWorld project toggleShowWorldMainDockingBar].

StringHolder new
		acceptContents: ((CurrentJEISarInstaller memberNamed: 'ReadMe-Fonts.txt') contents convertFromWithConverter: (UTF8TextConverter new));
		openLabel: 'ReadMe-Fonts'.
			
StringHolder new
		acceptContents: ((CurrentJEISarInstaller memberNamed: 'ReadMe-Packages.md') contents convertFromWithConverter: (UTF8TextConverter new));
		openLabel: 'ReadMe-Packages'.
			
!