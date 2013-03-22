"JaEx utilities"
matched := #('4.1' '4.2' '4.3' '4.4') detect: [:each | '*',each,'*' match: Smalltalk version] ifNone: [].
matched isNil ifTrue: [^self].

CurrentJEISarInstaller fileInMemberNamed: 'ttfInstaller.cs'.

jaEx := 'JaEx-Squeak-mu.19.mcz'.
(matched notNil) ifTrue:[CurrentJEISarInstaller fileInMonticelloZipVersionNamed: jaEx].

2 timesRepeat: [ActiveWorld project toggleShowWorldMainDockingBar].

StringHolder new
		acceptContents: ((CurrentJEISarInstaller memberNamed: 'ReadMe-Fonts.txt') contents convertFromWithConverter: (UTF8TextConverter new));
		openLabel: 'Read Me Fonts'.
!